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

        /// <summary>
        /// Adds a new person to the family tree.
        /// </summary>
        /// <param name="primaryPerson">A flag indicating whether the person is the primary person in the family tree.</param>
        /// <param name="lastname">The last name of the person.</param>
        /// <param name="gender">The gender of the person.</param>
        /// <param name="maidenName">The maiden name of the person (if applicable).</param>
        /// <param name="firstName">The first name of the person.</param>
        /// <param name="otherNameVariants">Other name variants or aliases of the person (if applicable).</param>
        /// <param name="birthDate">The date of birth of the person (null if unknown).</param>
        /// <param name="deathDate">The date of death of the person (null if alive or date unknown).</param>
        /// <returns>The unique identifier of the newly added person.</returns>
        int AddPerson(PersonInformation person);

        /// <summary>
        /// Updates existing person information in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the person to update.</param>
        /// <param name="primaryPerson">The updated flag indicating whether the person is the primary person in the family tree.</param>
        /// <param name="lastname">The updated last name of the person.</param>
        /// <param name="gender">The updated gender of the person.</param>
        /// <param name="maidenName">The updated maiden name of the person.</param>
        /// <param name="firstName">The updated first name of the person.</param>
        /// <param name="otherNameVariants">The updated other name variants or aliases of the person.</param>
        /// <param name="birthDate">The updated date of birth of the person (null if unchanged).</param>
        /// <param name="deathDate">The updated date of death of the person (null if unchanged).</param>
        void UpdatePerson(PersonInformation person);

        /// <summary>
        /// Deletes a person from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the person to delete.</param>
        void DeletePerson(int id);
    }
}