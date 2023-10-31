using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class SpecialRecordService : ISpecialRecordService
    {
        private IGenericRepository<SpecialRecord> _specialRecordRepository;

        public SpecialRecordService(IGenericRepository<SpecialRecord> specialRecordRepository)
        {
            _specialRecordRepository = specialRecordRepository;
        }

        public IEnumerable<SpecialRecord> GetAllSpecialRecords()
        {
            return _specialRecordRepository.GetAll();
        }

        public SpecialRecord GetSpecialRecordById(int id)
        {
            return _specialRecordRepository.GetById(id);
        }

        public void AddSpecialRecord(string recordType, int? houseNumber, string priest, string record, int eventId)
        {
            SpecialRecord newSpecialRecord = new SpecialRecord
            {
                RecordType = recordType,
                HouseNumber = houseNumber,
                Priest = priest,
                Record = record,
                EventId = eventId
            };

            _specialRecordRepository.Add(newSpecialRecord);
            _specialRecordRepository.Save();
        }

        public void UpdateSpecialRecord(int id, string recordType, int? houseNumber, string priest, string record, int eventId)
        {
            SpecialRecord existingSpecialRecord = _specialRecordRepository.GetById(id);

            if (existingSpecialRecord != null)
            {
                existingSpecialRecord.RecordType = recordType;
                existingSpecialRecord.HouseNumber = houseNumber;
                existingSpecialRecord.Priest = priest;
                existingSpecialRecord.Record = record;
                existingSpecialRecord.EventId = eventId;

                _specialRecordRepository.Update(existingSpecialRecord);
                _specialRecordRepository.Save();
            }
        }

        public void DeleteSpecialRecord(int id)
        {
            _specialRecordRepository.Remove(id);
            _specialRecordRepository.Save();
        }
    }
}
