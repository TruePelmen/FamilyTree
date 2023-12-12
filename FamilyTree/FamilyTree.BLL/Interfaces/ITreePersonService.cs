namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing the association of persons with family trees.
    /// </summary>
    public interface ITreePersonService
    {
        /// <summary>
        /// Retrieves all associations of persons with family trees.
        /// </summary>
        /// <returns>An IEnumerable of TreePerson objects representing all associations.</returns>
        IEnumerable<TreePerson> GetAllTreePeople();

        /// <summary>
        /// Retrieves an association of a person with a family tree by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the association.</param>
        /// <returns>The TreePerson object with the specified identifier, or null if not found.</returns>
        TreePerson GetTreePersonById(int id);

        /// <summary>
        /// Retrieves all persons associated with a specific family tree.
        /// </summary>
        /// <param name="treeId">The unique identifier of the family tree.</param>
        /// <returns>An IEnumerable of Person objects associated with the family tree.</returns>
        IEnumerable<PersonInformation> GetTreePeopleByTreeId(int treeId);

        TreePerson GetTreePerson(int treeId, int personId);

        int GetPersonsNumber(int treeId);

        int GetEventsNumber(int treeId);

        int GetPhotosNumber(int treeId);

        /// <summary>
        /// Adds a new association of a person with a family tree.
        /// </summary>
        /// <param name="treeId">The unique identifier of the family tree.</param>
        /// <param name="personId">The unique identifier of the person to associate with the tree.</param>
        void AddTreePerson(int treeId, int personId);

        /// <summary>
        /// Updates an existing association of a person with a family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the association to update.</param>
        /// <param name="treeId">The updated unique identifier of the family tree.</param>
        /// <param name="personId">The updated unique identifier of the person.</param>
        string UpdateTreePerson(int id, int treeId, int personId);

        void DeleteTreePerson(int treeId, int personId);
    }
}
