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

        public IEnumerable<SpecialRecordInformation> GetAllSpecialRecords()
        {
            return this.specialRecordRepository.GetAll().Select(specialRecord =>
                                       new SpecialRecordInformation(specialRecord)).ToList();
        }

        public IEnumerable<SpecialRecordInformation> GetAllSpecialRecordsForEvent(int eventId)
        {
            return this.specialRecordRepository.GetAllSpecialRecordsForEvent(eventId).Select(specialRecord =>
                                                  new SpecialRecordInformation(specialRecord)).ToList();
        }

        public SpecialRecordInformation GetSpecialRecordById(int id)
        {
            return new SpecialRecordInformation(this.specialRecordRepository.GetById(id));
        }

        public void AddSpecialRecord(SpecialRecordInformation specialRecord)
        {
            SpecialRecord newSpecialRecord = new SpecialRecord
            {
                RecordType = specialRecord.RecordType,
                HouseNumber = specialRecord.HouseNumber,
                Priest = specialRecord.Priest,
                Record = specialRecord.Record,
                EventId = specialRecord.EventId,
            };

            this.specialRecordRepository.Add(newSpecialRecord);
            this.specialRecordRepository.Save();
        }

        public void UpdateSpecialRecord(SpecialRecordInformation specialRecord)
        {
            SpecialRecord existingSpecialRecord = this.specialRecordRepository.GetById(specialRecord.Id);

            if (existingSpecialRecord != null)
            {
                existingSpecialRecord.RecordType = specialRecord.RecordType;
                existingSpecialRecord.HouseNumber = specialRecord.HouseNumber;
                existingSpecialRecord.Priest = specialRecord.Priest;
                existingSpecialRecord.Record = specialRecord.Record;
                existingSpecialRecord.EventId = specialRecord.EventId;

                this.specialRecordRepository.Update(existingSpecialRecord);
                this.specialRecordRepository.Save();
            }
        }

        public void DeleteSpecialRecord(int id)
        {
            this.specialRecordRepository.Remove(this.specialRecordRepository.GetById(id));
            this.specialRecordRepository.Save();
        }

        //public IEnumerable<SpecialRecord> GetAllSpecialRecordsForEvent(int eventId)
        //{
        //    List<SpecialRecord> specialRecordsForEvent = new List<SpecialRecord>();
        //    List<SpecialRecord> specialRecordsCollection = (List<SpecialRecord>)this.GetAllSpecialRecords();

        //    foreach (SpecialRecord specialRecord in specialRecordsCollection)
        //    {
        //        if (specialRecord.EventId == eventId)
        //        {
        //            specialRecordsForEvent.Add(specialRecord);
        //        }
        //    }

        //    return specialRecordsForEvent; // Явно перетворюємо в List<SpecialRecord>
        //}

        public bool AreSpecialRecordsOfTypeExistForEvent(int eventId, string recordType)
        {
            // Отримайте всі спеціальні записи для заданої події та перевірте, чи є серед них записи заданого типу.
            var specialRecords = this.GetAllSpecialRecordsForEvent(eventId);
            return specialRecords.Any(record => record.RecordType == recordType);
        }
    }
}
