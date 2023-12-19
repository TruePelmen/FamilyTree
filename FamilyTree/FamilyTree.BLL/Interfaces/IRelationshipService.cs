namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing relationships between persons in the family tree.
    /// </summary>
    public interface IRelationshipService
    {
        /// <summary>
        /// Retrieves all relationships between persons in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of Relationship objects representing all relationships.</returns>
        IEnumerable<Relationship> GetAllRelationships();

        /// <summary>
        /// Retrieves a relationship by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the relationship.</param>
        /// <returns>The Relationship object with the specified identifier, or null if not found.</returns>
        Relationship GetRelationshipById(int id);

        /// <summary>
        /// Retrieves the unique identifier of the mother of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The unique identifier of the mother of the person, or -1 if not found.</returns>
        int GetMotherIdByPersonId(int id);

        /// <summary>
        /// Retrieves the unique identifier of the father of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The unique identifier of the father of the person, or -1 if not found.</returns>
        int GetFatherIdByPersonId(int id);

        /// <summary>
        /// Retrieves the unique identifier of the spouse of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The unique identifier of the spouse of the person, or -1 if not found.</returns>
        int GetSpouseIdByPersonId(int id);

        /// <summary>
        /// Retrieves the unique identifiers of children of a person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>An IEnumerable of unique identifiers representing the children of the person.</returns>
        IEnumerable<int> GetChildrenIdByPersonId(int id);

        /// <summary>
        /// Adds a new relationship between two persons in the family tree.
        /// </summary>
        /// <param name="personId1">The unique identifier of the first person in the relationship.</param>
        /// <param name="personId2">The unique identifier of the second person in the relationship.</param>
        /// <param name="relationshipType">The type of the relationship (e.g., parent, child, spouse).</param>
        void AddRelationship(int personId1, int personId2, string relationshipType);

        /// <summary>
        /// Updates an existing relationship between two persons in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the relationship to update.</param>
        /// <param name="personId1">The updated unique identifier of the first person in the relationship.</param>
        /// <param name="personId2">The updated unique identifier of the second person in the relationship.</param>
        /// <param name="relationshipType">The updated type of the relationship.</param>
        void UpdateRelationship(int id, int personId1, int personId2, string relationshipType);

        /// <summary>
        /// Deletes a relationship from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the relationship to delete.</param>
        void DeleteRelationship(int id);
    }
}
