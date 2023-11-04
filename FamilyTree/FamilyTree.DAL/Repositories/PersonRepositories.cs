using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyTree.DAL.Context;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;

namespace FamilyTree.DAL.Repositories
{
    public class PersonRepositories : GenericRepository<Person>, IPersonRepository
    {
        public IEnumerable<Person> GetPeopleByTreeId(int treeId)
        {
            return _context.Set<Person>()
           .Where(e => e.TreePeople.Any(tp => tp.TreeId == treeId))
           .ToList();
        }
    }
}
