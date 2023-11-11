namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

#nullable enable

    public interface IEventService
    {
        IEnumerable<Event> GetAllEvents();

        IEnumerable<Event> GetEventsByPersonId(int id);

        IEnumerable<Event> GetAllEventsByPersonIdAndEventType(int personId, string eventType);

        Event GetEventById(int id);

        void AddEvent(string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age);

        void UpdateEvent(int id, string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age);

        void DeleteEvent(int id);

        bool IsEventUnique(int personId, string eventType);
    }
}