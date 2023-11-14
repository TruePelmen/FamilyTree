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
        IEnumerable<SpecialRecordInformation> GetAllSpecialRecords();

        IEnumerable<SpecialRecordInformation> GetAllSpecialRecordsForEvent(int eventId);

        /// <summary>
        /// Retrieves a special record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the special record.</param>
        /// <returns>The SpecialRecord object with the specified identifier, or null if not found.</returns>
        SpecialRecordInformation GetSpecialRecordById(int id);

        void AddSpecialRecord(SpecialRecordInformation specialRecord);

        void UpdateSpecialRecord(SpecialRecordInformation specialRecord);

        /// <summary>
        /// Deletes a special record from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the special record to delete.</param>
        void DeleteSpecialRecord(int id);

        public bool AreSpecialRecordsOfTypeExistForEvent(int eventId, string recordType);
    }
}