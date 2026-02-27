using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Rest.Routing
{
    /// <summary>
    /// definition de l'attribut HttpDeleteAttribute qui est utilisé pour annoter les méthodes dans les contrôleurs afin d'indiquer que ces méthodes doivent être invoquées en réponse à des requêtes HTTP DELETE.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class HttpDeleteAttribute:Attribute
    {
        public string Path { get; }
        public HttpDeleteAttribute(string path)
        {
            Path = path;
        }
    }
}
