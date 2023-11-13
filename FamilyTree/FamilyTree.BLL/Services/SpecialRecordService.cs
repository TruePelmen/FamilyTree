namespace FamilyTree.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class SpecialRecordService : ISpecialRecordService
    {
        private ISpecialRecordRepository specialRecordRepository;

        public SpecialRecordService(ISpecialRecordRepository specialRecordRepository)
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
        public IEnumerable<SpecialRecord> GetAllSpecialRecordsForEvent(int eventId)
        {
            List<SpecialRecord> specialRecordsForEvent = new List<SpecialRecord>();
            List<SpecialRecord> specialRecordsCollection = (List<SpecialRecord>)GetAllSpecialRecords();

            foreach (SpecialRecord specialRecord in specialRecordsCollection)
            {
                if (specialRecord.EventId == eventId)
                {
                    specialRecordsForEvent.Add(specialRecord);
                }
            }

            return specialRecordsForEvent; // Явно перетворюємо в List<SpecialRecord>
        }



        public bool AreSpecialRecordsOfTypeExistForEvent(int eventId, string recordType)
        {
            // Отримайте всі спеціальні записи для заданої події та перевірте, чи є серед них записи заданого типу.
            var specialRecords = this.GetAllSpecialRecordsForEvent(eventId);
            return specialRecords.Any(record => record.RecordType == recordType);
        }

    }
}
