// <copyright file="EventService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Service class for managing events in the family tree.
    /// </summary>
    public class EventService : IEventService
    {
        private IEventRepository eventRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository for events.></param>
        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        /// <summary>
        /// Retrieves all events in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of Event objects representing all events.</returns>
        public IEnumerable<Event> GetAllEvents()
        {
            return this.eventRepository.GetAll();
        }

        /// <summary>
        /// Retrieves events associated with a specific person.
        /// </summary>
        /// <param name="personId">The unique identifier of the person.</param>
        /// <returns>An IEnumerable of Event objects associated with the person.</returns>
        public IEnumerable<Event> GetEventsByPersonId(int personId)
        {
            return this.eventRepository.GetEventsByPersonId(personId);
        }

        /// <summary>
        /// Retrieves events associated with a specific person and event type.
        /// </summary>
        /// <param name="personId">The unique identifier of the person.</param>
        /// <param name="eventType">The type of the event (e.g., birth, marriage).</param>
        /// <returns>An IEnumerable of Event objects matching the person and event type.</returns>
        public IEnumerable<Event> GetAllEventsByPersonIdAndEventType(int personId, string eventType)
        {
            return this.eventRepository.GetEventsByPersonIdAndEventType(personId, eventType);
        }

        /// <summary>
        /// Retrieves an event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <returns>The Event object with the specified identifier, or null if not found.</returns>
        public Event GetEventById(int id)
        {
            return this.eventRepository.GetById(id);
        }

        /// <summary>
        /// Adds a new event to the family tree.
        /// </summary>
        /// <param name="eventType">The type of the event (e.g., birth, marriage).</param>
        /// <param name="eventDate">The date of the event (null if not applicable).</param>
        /// <param name="eventPlace">The place of the event (null if not applicable).</param>
        /// <param name="personId">The unique identifier of the person associated with the event.</param>
        /// <param name="description">Additional description or details about the event (null if not applicable).</param>
        /// <param name="age">The age of the person at the time of the event (null if not applicable).</param>
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

        /// <summary>
        /// Updates an existing event in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the event to update.</param>
        /// <param name="eventType">The updated type of the event.</param>
        /// <param name="eventDate">The updated date of the event (null if unchanged).</param>
        /// <param name="eventPlace">The updated place of the event (null if unchanged).</param>
        /// <param name="personId">The updated unique identifier of the person associated with the event.</param>
        /// <param name="description">The updated additional description or details about the event (null if unchanged).</param>
        /// <param name="age">The updated age of the person at the time of the event (null if unchanged).</param>
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

        /// <summary>
        /// Deletes an event from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the event to delete.</param>
        public void DeleteEvent(int id)
        {
            this.eventRepository.Remove(this.GetEventById(id));
            this.eventRepository.Save();
        }
    }
}
