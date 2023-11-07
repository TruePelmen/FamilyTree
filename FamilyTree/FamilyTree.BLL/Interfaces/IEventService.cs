namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing events related to persons in a family tree.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Retrieves all events in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of Event objects representing all events.</returns>
        IEnumerable<Event> GetAllEvents();

        /// <summary>
        /// Retrieves events associated with a specific person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>An IEnumerable of Event objects associated with the person.</returns>
        IEnumerable<Event> GetEventsByPersonId(int id);

        /// <summary>
        /// Retrieves events associated with a specific person and a specific event type.
        /// </summary>
        /// <param name="personId">The unique identifier of the person.</param>
        /// <param name="eventType">The type of the event (e.g., birth, marriage).</param>
        /// <returns>An IEnumerable of Event objects matching the specified criteria.</returns>
        IEnumerable<Event> GetAllEventsByPersonIdAndEventType(int personId, string eventType);

        /// <summary>
        /// Retrieves an event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <returns>The Event object with the specified identifier, or null if not found.</returns>
        Event GetEventById(int id);

        /// <summary>
        /// Adds a new event to the family tree.
        /// </summary>
        /// <param name="eventType">The type of the event (e.g., birth, marriage).</param>
        /// <param name="eventDate">The date of the event (null if unknown).</param>
        /// <param name="eventPlace">The location of the event (null if unknown).</param>
        /// <param name="personId">The unique identifier of the person associated with the event.</param>
        /// <param name="description">Additional description of the event (null if not provided).</param>
        /// <param name="age">The age of the person at the time of the event (null if unknown).</param>
        void AddEvent(string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age);

        /// <summary>
        /// Updates an existing event in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the event to update.</param>
        /// <param name="eventType">The type of the event (e.g., birth, marriage).</param>
        /// <param name="eventDate">The updated date of the event (null if unchanged).</param>
        /// <param name="eventPlace">The updated location of the event (null if unchanged).</param>
        /// <param name="personId">The unique identifier of the person associated with the event (null if unchanged).</param>
        /// <param name="description">The updated description of the event (null if unchanged).</param>
        /// <param name="age">The updated age of the person at the time of the event (null if unchanged).</param>
        void UpdateEvent(int id, string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age);

        /// <summary>
        /// Deletes an event from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the event to delete.</param>
        void DeleteEvent(int id);
    }
}
