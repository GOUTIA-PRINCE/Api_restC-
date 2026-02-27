using System;
using MySql.Data.MySqlClient;


namespace API_Rest.data
{
    /// <summary>
    /// represente la classe de contexte de données pour l'application, elle est utilisée pour gérer les connexions à la base de données .
    /// </summary>
    internal class DbContext
    {
        //definition de la chaine de connexion à la base de données en mode lecture seule
        private readonly string connectionString = "Database=gestionenseignement;server =localhost;user=root;password=;";

        //definition d'une méthode pour créer une nouvelle connexion à la base de données en utilisant la chaine de connexion définie précédement
        public MySqlConnection CreerConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
