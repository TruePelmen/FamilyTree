namespace FamilyTree.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class PersonRepositories : GenericRepository<Person>, IPersonRepository
    {
        public IEnumerable<Person> GetPeopleByTreeId(int treeId)
        {
            return this.context.Set<Person>()
           .Where(e => e.TreePeople.Any(tp => tp.TreeId == treeId))
           .ToList();
        }
    }
}
