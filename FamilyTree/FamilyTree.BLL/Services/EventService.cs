namespace FamilyTree.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

#nullable enable
    public class EventService : IEventService
    {
        private IEventRepository eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public IEnumerable<EventInformation> GetAllEvents()
        {
            return this.eventRepository.GetAll().Select(eventInfo => new EventInformation(eventInfo)).ToList();
        }

        public IEnumerable<EventInformation> GetEventsByPersonId(int personId)
        {
            return this.eventRepository.GetEventsByPersonId(personId).Select(eventInfo => new EventInformation(eventInfo)).ToList();
        }

        public IEnumerable<EventInformation> GetImportantEventsByPersonId(int personId)
        {
            return this.eventRepository.GetImportantEventsByPersonId(personId).Select(eventInfo => new EventInformation(eventInfo)).ToList();
        }

        public IEnumerable<EventInformation> GetAllEventsByPersonIdAndEventType(int personId, string eventType)
        {
            return this.eventRepository.GetEventsByPersonIdAndEventType(personId, eventType).Select(eventInfo => new EventInformation(eventInfo)).ToList();
        }

        public EventInformation GetEventById(int id)
        {
            return new EventInformation(this.eventRepository.GetById(id));
        }

        public void AddEvent(EventInformation eventInformation)
        {
            Event newEvent = new Event
            {
                EventType = eventInformation.EventType,
                EventDate = eventInformation.EventDate,
                EventPlace = eventInformation.EventPlace,
                PersonId = eventInformation.PersonId,
                Description = eventInformation.Description,
                Age = eventInformation.Age,
            };

            this.eventRepository.Add(newEvent);
            this.eventRepository.Save();
        }

        public void UpdateEvent(EventInformation eventInformation)
        {
            Event existingEvent = this.eventRepository.GetById(eventInformation.Id);

            if (existingEvent != null)
            {
                existingEvent.EventType = eventInformation.EventType;
                existingEvent.EventDate = eventInformation.EventDate;
                existingEvent.EventPlace = eventInformation.EventPlace;
                existingEvent.PersonId = eventInformation.PersonId;
                existingEvent.Description = eventInformation.Description;
                existingEvent.Age = eventInformation.Age;

                this.eventRepository.Update(existingEvent);
                this.eventRepository.Save();
            }
        }

        public void DeleteEvent(int id)
        {
            this.eventRepository.Remove(this.eventRepository.GetById(id));
            this.eventRepository.Save();
        }

        public bool IsEventUnique(int personId, string eventType)
        {
            if (eventType == "birth" || eventType == "death")
            {
                IEnumerable<EventInformation> existingEvents = this.GetAllEventsByPersonIdAndEventType(personId, eventType);

                if (existingEvents != null && existingEvents.Any())
                {
                    foreach (EventInformation existingEvent in existingEvents)
                    {
                        if (existingEvent.EventType == eventType)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}