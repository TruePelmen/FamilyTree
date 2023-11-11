namespace FamilyTree.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public IEnumerable<Event> GetEventsByPersonId(int personId)
        {
            return this.context.Events
            .Where(e => e.PersonId == personId)
            .ToList();
        }

        public IEnumerable<Event> GetEventsByPersonIdAndEventType(int personId, string eventType)
        {
            return this.context.Events
            .Where(e => e.PersonId == personId && e.EventType == eventType)
            .ToList();
        }
    }
}
