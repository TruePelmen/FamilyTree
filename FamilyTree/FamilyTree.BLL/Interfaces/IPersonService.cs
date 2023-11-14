namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for managing persons and their information in the family tree.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Retrieves all persons in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of Person objects representing all persons.</returns>
        IEnumerable<PersonInformation> GetAllPeople();

        /// <summary>
        /// Retrieves a person by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The Person object with the specified identifier, or null if not found.</returns>
        PersonInformation GetPersonById(int id);

        /// <summary>
        /// Retrieves short information about a person in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>A PersonCardInformation object containing concise information about the person.</returns>
        PersonInformation GetShortInformationAboutPerson(int id);

        int AddPerson(PersonInformation person);

        void UpdatePerson(PersonInformation person);

        /// <summary>
        /// Deletes a person from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the person to delete.</param>
        void DeletePerson(int id);
    }
}