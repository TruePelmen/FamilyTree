namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing special records related to events in the family tree.
    /// </summary>
    public interface ISpecialRecordService
    {
        /// <summary>
        /// Retrieves all special records in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of SpecialRecord objects representing all special records.</returns>
        IEnumerable<SpecialRecord> GetAllSpecialRecords();

        /// <summary>
        /// Retrieves a special record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the special record.</param>
        /// <returns>The SpecialRecord object with the specified identifier, or null if not found.</returns>
        SpecialRecord GetSpecialRecordById(int id);

        /// <summary>
        /// Adds a new special record to the family tree.
        /// </summary>
        /// <param name="recordType">The type of the special record (e.g., baptism, marriage).</param>
        /// <param name="houseNumber">The house number related to the record (null if not applicable).</param>
        /// <param name="priest">The name of the priest associated with the record.</param>
        /// <param name="record">The details of the special record.</param>
        /// <param name="eventId">The unique identifier of the event associated with the special record.</param>
        void AddSpecialRecord(string recordType, int? houseNumber, string priest, string record, int eventId);

        /// <summary>
        /// Updates an existing special record in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the special record to update.</param>
        /// <param name="recordType">The updated type of the special record.</param>
        /// <param name="houseNumber">The updated house number related to the record (null if unchanged).</param>
        /// <param name="priest">The updated name of the priest associated with the record.</param>
        /// <param name="record">The updated details of the special record.</param>
        /// <param name="eventId">The updated unique identifier of the event associated with the special record.</param>
        void UpdateSpecialRecord(int id, string recordType, int? houseNumber, string priest, string record, int eventId);

        /// <summary>
        /// Deletes a special record from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the special record to delete.</param>
        void DeleteSpecialRecord(int id);

        public bool AreSpecialRecordsOfTypeExistForEvent(int eventId, string recordType);
    }
}