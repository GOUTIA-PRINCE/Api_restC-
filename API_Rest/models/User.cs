using System;

namespace API_Rest.models
{
    /// <summary>
    /// definition du model de données d'un utilisateur
    /// </summary>
    internal class User
    {
        //definition des propriétés de l'utilisateur avec  get et set pour chaque propriété
        public int id { get; set; }
        public string name { get; set; }

        public int tel { get; set; }
        public string email { get; set; }

        //definition du constructeur de la classe User
        public User(int id, string name, int tel, string email)
        {
            this.id = id;
            this.name = name;
            this.tel = tel;
            this.email = email;
        }

        //definition du constructeur par défaut de la classe User
        public User()
        {
        }

    }
}
