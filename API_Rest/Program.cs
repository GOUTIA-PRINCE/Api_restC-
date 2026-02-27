using API_Rest.controllers;
using API_Rest.data;
using API_Rest.repositories;
using API_Rest.Routing;
using API_Rest.services;
using API_Rest.models;

using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:5000/");
listener.Start();

Console.WriteLine("API tourne sur le port 5000 ");

// =====================
// Injection manuelle
// =====================
var db = new DbContext();
var repo = new UserRepositoryImpl(db);
var service = new UserServiceImpl(repo);
var controller = new UserController(service);

// Récupère toutes les méthodes du controller
var methods = controller.GetType().GetMethods();

// Boucle infinie pour écouter les requêtes entrantes
while (true)
{
    // Attendre une requête entrante
    var ctx = await listener.GetContextAsync();
    // Récupérer la requête et la réponse
    var req = ctx.Request;
    var res = ctx.Response;

    
    var path = req.Url.AbsolutePath;
    var httpMethod = req.HttpMethod;

    MethodInfo? methodFound = null;
    object?[]? parameters = null;

    foreach (var method in methods)
    {
        var getAttr = method.GetCustomAttribute<HttpGetAttribute>();
        var postAttr = method.GetCustomAttribute<HttpPostAttribute>();
        var putAttr = method.GetCustomAttribute<HttpPutAttribute>();

        string? route = null;

        if (getAttr != null && httpMethod == "GET")
            route = getAttr.Path;

        if (postAttr != null && httpMethod == "POST")
            route = postAttr.Path;

        if (putAttr != null && httpMethod == "PUT")
            route = putAttr.Path;

        if (route == null)
            continue;

        // =============================
        // Gestion route simple exacte
        // =============================
        if (route == path)
        {
            methodFound = method;
            parameters = null;
            break;
        }

        // =============================
        // Gestion route avec {id}
        // =============================
        if (route.Contains("{id}"))
        {
            var baseRoute = route.Replace("/{id}", "");
            if (path.StartsWith(baseRoute))
            {
                var idPart = path.Substring(baseRoute.Length).Trim('/');
                if (int.TryParse(idPart, out int id))
                {
                    methodFound = method;

                    if (httpMethod == "PUT")
                    {
                        using var reader = new StreamReader(req.InputStream);
                        var body = await reader.ReadToEndAsync();
                        var user = JsonSerializer.Deserialize<User>(body);

                        parameters = new object?[] { id, user };
                    }
                    else
                    {
                        parameters = new object?[] { id };
                    }

                    break;
                }
            }
        }
    }

    if (methodFound != null)
    {
        object? result;

        if (httpMethod == "POST")
        {
            using var reader = new StreamReader(req.InputStream);
            var body = await reader.ReadToEndAsync();
            var user = JsonSerializer.Deserialize<User>(body);

            result = methodFound.Invoke(controller, new object?[] { user });
        }
        else
        {
            result = methodFound.Invoke(controller, parameters);
        }

        var json = result?.ToString() ?? "";

        var buffer = Encoding.UTF8.GetBytes(json);
        res.ContentType = "application/json";
        res.StatusCode = 200;
        res.OutputStream.Write(buffer, 0, buffer.Length);
    }
    else
    {
        res.StatusCode = 404;
        var buffer = Encoding.UTF8.GetBytes("{\"message\":\"Route not found\"}");
        res.OutputStream.Write(buffer, 0, buffer.Length);
    }

    res.Close();
}