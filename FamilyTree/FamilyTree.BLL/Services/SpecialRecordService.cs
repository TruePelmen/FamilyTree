namespace FamilyTree.BLL.Services
{
    using System;
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
            try
            {
                this.specialRecordRepository.Add(newSpecialRecord);
                this.specialRecordRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Невдалося додати спеціальний запис");
            }
        }

        public string UpdateSpecialRecord(SpecialRecordInformation specialRecord)
        {
            SpecialRecord existingSpecialRecord = this.specialRecordRepository.GetById(specialRecord.Id);
            string message = string.Empty;
            if (existingSpecialRecord != null)
            {
                existingSpecialRecord.RecordType = specialRecord.RecordType;
                existingSpecialRecord.HouseNumber = specialRecord.HouseNumber;
                existingSpecialRecord.Priest = specialRecord.Priest;
                existingSpecialRecord.Record = specialRecord.Record;
                existingSpecialRecord.EventId = specialRecord.EventId;

                this.specialRecordRepository.Update(existingSpecialRecord);
                this.specialRecordRepository.Save();
                message = "Спеціальний запис успішно оновлено";
            }
            else
            {
                message = "Спеціальний запис з таким id не знайдено";
            }

            return message;
        }

        public void DeleteSpecialRecord(int id)
        {
            try
            {
                this.specialRecordRepository.Remove(this.specialRecordRepository.GetById(id));
                this.specialRecordRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Невдалося видалити спеціальний запис з таким id");
            }
        }

        public bool AreSpecialRecordsOfTypeExistForEvent(int eventId, string recordType)
        {
            var specialRecords = this.GetAllSpecialRecordsForEvent(eventId);
            return specialRecords.Any(record => record.RecordType == recordType);
        }
    }
}
