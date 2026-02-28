using System;
using System.Text.Json;
using API_Rest.services;
using API_Rest.models;
using API_Rest.Routing;


namespace API_Rest.controllers
{
    /// <summary>
    /// definition de la classe UserController qui est responsable de gérer les requêtes HTTP liées aux utilisateurs dans l'application.
    /// Cette classe agit en tant que point d'entrée pour les opérations de gestion des utilisateurs,
    /// en recevant les requêtes du client, en appelant les services appropriés pour traiter ces requêtes et en retournant les réponses correspondantes.
    /// Le UserController peut inclure des méthodes pour gérer les opérations telles que la récupération de tous les utilisateurs, la récupération d'un utilisateur spécifique par son id,
    /// l'ajout d'un nouvel utilisateur et la mise à jour des informations d'un utilisateur existant.
    /// En utilisant une architecture basée sur les contrôleurs,
    /// cette classe contribue à organiser le code de manière claire et à séparer les responsabilités entre la couche de présentation (contrôleur) et la couche de service (UserService) qui gère la logique métier liée aux utilisateurs.
    /// </summary>
    internal class UserController 
    {
        //injection de dépendance du UserService pour permettre à la classe d'accéder aux services de gestion des utilisateurs
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/api/users")]
       public string GetAllUsers()
        {
            // Appeler le service pour récupérer tous les utilisateurs
            var users = userService.GetAllUsers();
            // Retourner la liste des utilisateurs au format JSON
            return JsonSerializer.Serialize(users);
        }

        [HttpGet("/api/users/{id}")]
        public string GetUserById(int id)
        {
            // Appeler le service pour récupérer l'utilisateur par son id
            var user = userService.GetUserById(id);
            // Retourner l'utilisateur trouvé ou une réponse indiquant que l'utilisateur n'existe pas
            return JsonSerializer.Serialize(user);
        }

        [HttpPost("/api/users")]
        public string AddUser(User user)
        {
            userService.AddUser(user);
            // Retourner l'utilisateur ajouté avec son id généré
            return JsonSerializer.Serialize(user);

        }
        [HttpPut("/api/users/{id}")]
        public string UpdateUser(int id, User user)
        {
            // Assigner l'id de l'utilisateur à partir du paramètre de la route
            user.id = id;
            //Appeler le service pour mettre à jour l'utilisateur
            userService.UpdateUser(user);
            // Retourner l'utilisateur mis à jour
            return JsonSerializer.Serialize(user);
        }
        [HttpDelete("/api/users/{id}")]
    public string DeleteUser(int id)
    {
        userService.DeleteUser(id);
    
        return JsonSerializer.Serialize(new
        {
            message = "utilisateur  supprimé",
            id = id
        });
    }
    }
}
