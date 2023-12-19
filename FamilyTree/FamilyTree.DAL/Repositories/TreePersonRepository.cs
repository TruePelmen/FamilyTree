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

        public TreePerson GetTreePerson(int treeId, int personId)
        {
            return this.context.TreePeople
         .Where(tp => tp.TreeId == treeId && tp.PersonId == personId)
         .FirstOrDefault();
        }

        public int GetPersonsNumber(int treeId)
        {
            return this.context.TreePeople
         .Where(tp => tp.TreeId == treeId)
         .Include(tp => tp.Person)
         .Select(tp => tp.Person)
         .Count();
        }

        public int GetEventsNumber(int treeId)
        {
            int totalEvents = this.context.TreePeople
            .Where(tp => tp.TreeId == treeId)
            .Join(this.context.People, tp => tp.PersonId, p => p.Id, (tp, p) => p)
            .Sum(person => person.Events.Count);

            return totalEvents;
        }

        public int GetPhotosNumber(int treeId)
        {
            int personsPhoto = 0;

            var treePeople = this.GetTreePeopleByTreeId(treeId);

            foreach (var person in treePeople)
            {
                var mediaEvents = this.context.MediaEvents
                .Include(me => me.Event)
                .Include(me => me.Media)
                .Where(me => me.Event != null && me.Event.PersonId == person.Id)
                .ToList();

                personsPhoto += mediaEvents
                    .Where(me => me.Media.MediaType == "photo")
                    .Select(me => me.Media)
                    .Count();
            }

            return personsPhoto;
        }
    }
}
