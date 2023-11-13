using FamilyTree.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.DAL.Interfaces.Repositories
{
    public interface ISpecialRecordRepository : IGenericRepository<SpecialRecord>
    {
        public IEnumerable<SpecialRecord> GetSpecialRecordsById(int Id);

        public IEnumerable<SpecialRecord> GetSpecialRecordsByIdAndRecordType(int Id, string recordType);
        
    }
}
