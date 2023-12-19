namespace FamilyTree.BLL.Interfeces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing user accounts in the system.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all user accounts in the system.
        /// </summary>
        /// <returns>An IEnumerable of User objects representing all user accounts.</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Retrieves a user account by their login.
        /// </summary>
        /// <param name="login">The login of the user account.</param>
        /// <returns>The User object with the specified login, or null if not found.</returns>
        User GetUserByLogin(string login);

        /// <summary>
        /// Checks if a user account with the given login exists in the system.
        /// </summary>
        /// <param name="login">The login to check for existence.</param>
        /// <returns>True if a user account with the login exists; otherwise, false.</returns>
        bool FindUserByLogin(string login);

        /// <summary>
        /// Adds a new user account to the system.
        /// </summary>
        /// <param name="login">The login for the new user account.</param>
        /// <param name="password">The password for the new user account.</param>
        void AddUser(string login, string password);

        /// <summary>
        /// Updates an existing user account's password in the system.
        /// </summary>
        /// <param name="login">The login of the user account to update.</param>
        /// <param name="password">The updated password for the user account.</param>
        void UpdateUser(string login, string password);

        /// <summary>
        /// Deletes a user account from the system.
        /// </summary>
        /// <param name="login">The login of the user account to delete.</param>
        void DeleteUser(string login);

        /// <summary>
        /// Authenticates a user account using the provided login and password.
        /// </summary>
        /// <param name="login">The login of the user account to authenticate.</param>
        /// <param name="password">The password to validate for authentication.</param>
        /// <returns>True if the provided credentials are valid for authentication; otherwise, false.</returns>
        bool AuthenticateUser(string login, string password);
    }
}
