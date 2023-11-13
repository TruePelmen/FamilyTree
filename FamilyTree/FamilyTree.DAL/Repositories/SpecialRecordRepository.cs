using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.DAL.Repositories
{
    public class SpecialRecordRepository : GenericRepository<SpecialRecord>, ISpecialRecordRepository
    {
        public IEnumerable<SpecialRecord> GetSpecialRecordsById(int Id)
        {
            return context.SpecialRecords
            .Where(e => e.Id == Id)
            .ToList();
        }
        public IEnumerable<SpecialRecord> GetSpecialRecordsByIdAndRecordType(int Id, string recordType)
        {
            return context.SpecialRecords
            .Where(e => e.Id == Id && e.RecordType == recordType)
            .ToList();
        }
    }
}
