using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models; 

namespace FamilyTree.DAL.Repositories
{
    public class EventRepository: GenericRepository<Event>, IEventRepository
    {
        public IEnumerable<Event> GetEventsByPersonId(int personId)
        {
            return _context.Events
            .Where(e => e.PersonId == personId)
            .ToList();
        }
        public IEnumerable<Event> GetEventsByPersonIdAndEventType(int personId, string eventType)
        {
            return _context.Events
            .Where(e => e.PersonId == personId && e.EventType == eventType)
            .ToList();
        }
    }
}
