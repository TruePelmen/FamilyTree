namespace FamilyTree.DAL.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Models;

    public interface ISpecialRecordRepository : IGenericRepository<SpecialRecord>
    {
        IEnumerable<SpecialRecord> GetAllSpecialRecordsForEvent(int eventId);
    }
}
