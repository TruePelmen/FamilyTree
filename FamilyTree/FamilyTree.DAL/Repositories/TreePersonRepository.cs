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

    public class TreePersonRepository : GenericRepository<TreePerson>, ITreePersonRepository
    {
        public IEnumerable<Person> GetTreePeopleByTreeId(int treeId)
        {
            return this.context.TreePeople
         .Where(tp => tp.TreeId == treeId)
         .Include(tp => tp.Person)
         .Select(tp => tp.Person)
         .ToList();
        }
    }
}
