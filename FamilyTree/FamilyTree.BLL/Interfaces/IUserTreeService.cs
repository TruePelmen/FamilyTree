namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing user access to family trees.
    /// </summary>
    public interface IUserTreeService
    {
        /// <summary>
        /// Retrieves all user-tree associations in the system.
        /// </summary>
        /// <returns>An IEnumerable of UserTree objects representing all user-tree associations.</returns>
        IEnumerable<UserTree> GetAllUserTrees();

        /// <summary>
        /// Retrieves a user-tree association by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user-tree association.</param>
        /// <returns>The UserTree object with the specified identifier, or null if not found.</returns>
        UserTree GetUserTreeById(int id);

        /// <summary>
        /// Retrieves all family trees associated with a specific user.
        /// </summary>
        /// <param name="userLogin">The login of the user account.</param>
        /// <returns>An IEnumerable of Tree objects associated with the user.</returns>
        IEnumerable<Tree> GetAllTreeByUserLogin(string userLogin);

        public IEnumerable<UserTree> GetAllUserTreeByTreeId(int treeId);

        public IEnumerable<string> GetFreeUsersLoginByTreeId(int treeId);

        /// <summary>
        /// Retrieves the access type for a user and a specific family tree.
        /// </summary>
        /// <param name="userLogin">The login of the user account.</param>
        /// <param name="treeId">The unique identifier of the family tree.</param>
        /// <returns>The access type (e.g., read, write, admin) for the user and tree association, or an empty string if not found.</returns>
        string GetAccessTypeByUserLoginAndTreeId(string userLogin, int treeId);

        /// <summary>
        /// Adds a new association of a user with a family tree along with an access type.
        /// </summary>
        /// <param name="userLogin">The login of the user account to associate with the tree.</param>
        /// <param name="treeId">The unique identifier of the family tree to associate with the user.</param>
        /// <param name="accessType">The access type for the user-tree association (e.g., read, write, admin).</param>
        void AddUserTree(string userLogin, int treeId, string accessType);

        /// <summary>
        /// Updates an existing association of a user with a family tree, including the access type.
        /// </summary>
        /// <param name="id">The unique identifier of the user-tree association to update.</param>
        /// <param name="userLogin">The login of the user account.</param>
        /// <param name="treeId">The unique identifier of the family tree.</param>
        /// <param name="accessType">The updated access type.</param>
        void UpdateUserTree(int id, string userLogin, int treeId, string accessType);

        /// <summary>
        /// Deletes a user-tree association from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the user-tree association to delete.</param>
        void DeleteUserTree(int id);

        void DeleteUserTree(string login);
    }
}
