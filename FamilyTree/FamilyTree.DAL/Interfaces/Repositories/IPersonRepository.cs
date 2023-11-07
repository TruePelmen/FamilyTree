namespace FamilyTree.DAL.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Models;

    public interface IPersonRepository : IGenericRepository<Person>
    {
        public IEnumerable<Person> GetPeopleByTreeId(int treeId);
    }
}
