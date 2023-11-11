namespace FamilyTree.BLL.Services
{
    using System.Collections.Generic;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class SpecialRecordService : ISpecialRecordService
    {
        private IGenericRepository<SpecialRecord> specialRecordRepository;

        public SpecialRecordService(IGenericRepository<SpecialRecord> specialRecordRepository)
        {
            this.specialRecordRepository = specialRecordRepository;
        }

        public IEnumerable<SpecialRecord> GetAllSpecialRecords()
        {
            return this.specialRecordRepository.GetAll();
        }

        public SpecialRecord GetSpecialRecordById(int id)
        {
            return this.specialRecordRepository.GetById(id);
        }

        public void AddSpecialRecord(string recordType, int? houseNumber, string priest, string record, int eventId)
        {
            SpecialRecord newSpecialRecord = new SpecialRecord
            {
                RecordType = recordType,
                HouseNumber = houseNumber,
                Priest = priest,
                Record = record,
                EventId = eventId,
            };

            this.specialRecordRepository.Add(newSpecialRecord);
            this.specialRecordRepository.Save();
        }

        public void UpdateSpecialRecord(int id, string recordType, int? houseNumber, string priest, string record, int eventId)
        {
            SpecialRecord existingSpecialRecord = this.specialRecordRepository.GetById(id);

            if (existingSpecialRecord != null)
            {
                existingSpecialRecord.RecordType = recordType;
                existingSpecialRecord.HouseNumber = houseNumber;
                existingSpecialRecord.Priest = priest;
                existingSpecialRecord.Record = record;
                existingSpecialRecord.EventId = eventId;

                this.specialRecordRepository.Update(existingSpecialRecord);
                this.specialRecordRepository.Save();
            }
        }

        public void DeleteSpecialRecord(int id)
        {
            this.specialRecordRepository.Remove(this.GetSpecialRecordById(id));
            this.specialRecordRepository.Save();
        }
    }
}
