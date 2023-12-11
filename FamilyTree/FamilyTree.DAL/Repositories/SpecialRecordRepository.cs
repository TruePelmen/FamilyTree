namespace FamilyTree.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;
    using Microsoft.EntityFrameworkCore;

    public class SpecialRecordRepository : GenericRepository<SpecialRecord>, ISpecialRecordRepository
    {
        public IEnumerable<SpecialRecord> GetAllSpecialRecordsForEvent(int eventId)
        {
            var specialRecords = this.context.SpecialRecords
                .Where(sr => sr.EventId == eventId)
                .ToList();

            return specialRecords;
        }
    }
}
