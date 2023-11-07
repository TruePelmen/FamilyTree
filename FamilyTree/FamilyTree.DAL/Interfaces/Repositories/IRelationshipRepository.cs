namespace FamilyTree.DAL.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Models;

    public interface IRelationshipRepository : IGenericRepository<Relationship>
    {
        int GetParentIdByPersonId(int id, string type);

        int GetSpouseIdByPersonId(int id);

        IEnumerable<int> GetChildrenIdByPersonId(int id);
    }
}
