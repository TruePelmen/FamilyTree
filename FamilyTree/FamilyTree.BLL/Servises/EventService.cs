namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class EventService : IEventService
    {
        private IEventRepository eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return this.eventRepository.GetAll();
        }

        public IEnumerable<Event> GetEventsByPersonId(int personId)
        {
            return this.eventRepository.GetEventsByPersonId(personId);
        }

        public IEnumerable<Event> GetAllEventsByPersonIdAndEventType(int personId, string eventType)
        {
            return this.eventRepository.GetEventsByPersonIdAndEventType(personId, eventType);
        }

        public Event GetEventById(int id)
        {
            return this.eventRepository.GetById(id);
        }

        public void AddEvent(string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age)
        {
            Event newEvent = new Event
            {
                EventType = eventType,
                EventDate = eventDate,
                EventPlace = eventPlace,
                PersonId = personId,
                Description = description,
                Age = age,
            };

            this.eventRepository.Add(newEvent);
            this.eventRepository.Save();
        }

        public void UpdateEvent(int id, string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age)
        {
            Event existingEvent = this.eventRepository.GetById(id);

            if (existingEvent != null)
            {
                existingEvent.EventType = eventType;
                existingEvent.EventDate = eventDate;
                existingEvent.EventPlace = eventPlace;
                existingEvent.PersonId = personId;
                existingEvent.Description = description;
                existingEvent.Age = age;

                this.eventRepository.Update(existingEvent);
                this.eventRepository.Save();
            }
        }

        public void DeleteEvent(int id)
        {
            this.eventRepository.Remove(this.GetEventById(id));
            this.eventRepository.Save();
        }

        public bool IsEventUnique(int personId, string eventType)
        {
            if (eventType == "birth" || eventType == "death")
            {
                IEnumerable<Event> existingEvents = this.GetAllEventsByPersonIdAndEventType(personId, eventType);

                if (existingEvents != null && existingEvents.Any())
                {
                    foreach (Event existingEvent in existingEvents)
                    {
                        if (existingEvent.EventType == eventType)
                        {
                            return false;
                        }
                    }

                    // Вже існує подія такого типу, не додавати нову
                }
            }

            return true;
        }
    }
}
