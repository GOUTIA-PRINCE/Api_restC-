using System;
using API_Rest.models;

namespace API_Rest.services
{
    /// <summary>
    ///definition de l'interface UserService qui représente le contrat pour les opérations de gestion des utilisateurs dans l'application, elle définit les méthodes pour récupérer tous les utilisateurs,
    ///récupérer un utilisateur spécifique par son id, ajouter un nouvel utilisateur et mettre à jour les informations d'un utilisateur existant. Cette interface est utilisée pour abstraire la logique métier de la gestion des utilisateurs et permettre une séparation claire entre la couche de service et la couche de données représentée par le UserRepository.
    /// </summary>
    internal interface UserService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
    }
}
