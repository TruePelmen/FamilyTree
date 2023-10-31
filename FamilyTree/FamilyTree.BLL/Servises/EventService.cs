using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class EventService : IEventService
    {
        private IGenericRepository<Event> _eventRepository;

        public EventService(IGenericRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _eventRepository.GetAll();
        }

        public Event GetEventById(int id)
        {
            return _eventRepository.GetById(id);
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
                Age = age
            };

            _eventRepository.Add(newEvent);
            _eventRepository.Save();
        }

        public void UpdateEvent(int id, string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age)
        {
            Event existingEvent = _eventRepository.GetById(id);

            if (existingEvent != null)
            {
                existingEvent.EventType = eventType;
                existingEvent.EventDate = eventDate;
                existingEvent.EventPlace = eventPlace;
                existingEvent.PersonId = personId;
                existingEvent.Description = description;
                existingEvent.Age = age;

                _eventRepository.Update(existingEvent);
                _eventRepository.Save();
            }
        }

        public void DeleteEvent(int id)
        {
            _eventRepository.Remove(id);
            _eventRepository.Save();
        }
    }
}
