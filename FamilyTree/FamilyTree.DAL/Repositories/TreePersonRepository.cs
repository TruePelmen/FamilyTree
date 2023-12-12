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
            int totalPhotos = this.context.TreePeople
            .Where(tp => tp.TreeId == treeId)
            .Join(this.context.People, tp => tp.PersonId, p => p.Id, (tp, p) => new { TreePerson = tp, Person = p })
            .GroupJoin(this.context.MediaPeople, tp => tp.Person.Id, mediaPerson => mediaPerson.PersonId, (tp, mediaPeople) => new { TreePerson = tp.TreePerson, Person = tp.Person, MediaPeople = mediaPeople })
            .SelectMany(tp => tp.MediaPeople.DefaultIfEmpty(), (tp, mediaPerson) => new { TreePerson = tp.TreePerson, Person = tp.Person, MediaPerson = mediaPerson })
            .GroupJoin(this.context.MediaEvents, tp => tp.MediaPerson.MediaId, mediaEvent => mediaEvent.MediaId, (tp, mediaEvents) => new { TreePerson = tp.TreePerson, Person = tp.Person, MediaPerson = tp.MediaPerson, MediaEvents = mediaEvents })
            .SelectMany(tp => tp.MediaEvents.DefaultIfEmpty(), (tp, mediaEvent) => mediaEvent)
            .Count();

            return totalPhotos;
        }
    }
}
