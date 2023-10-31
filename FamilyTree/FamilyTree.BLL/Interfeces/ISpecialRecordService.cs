using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface ISpecialRecordService
    {
        IEnumerable<SpecialRecord> GetAllSpecialRecords();
        SpecialRecord GetSpecialRecordById(int id);
        void AddSpecialRecord(string recordType, int? houseNumber, string priest, string record, int eventId);
        void UpdateSpecialRecord(int id, string recordType, int? houseNumber, string priest, string record, int eventId);
        void DeleteSpecialRecord(int id);
    }
}
