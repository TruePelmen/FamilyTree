using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyTree.DAL.Models;
using FamilyTree.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.DAL.Repositories
{
    public class TreePersonRepository : GenericRepository<TreePerson>, ITreePersonRepository
    {
        public IEnumerable<Person> GetTreePeopleByTreeId(int treeId)
        {
            return _context.TreePeople
         .Where(tp => tp.TreeId == treeId)
         .Include(tp => tp.Person)
         .Select(tp => tp.Person)
         .ToList();
        }
    }
}
