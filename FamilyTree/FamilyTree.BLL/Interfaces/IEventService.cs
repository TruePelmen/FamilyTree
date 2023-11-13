namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

#nullable enable
    public interface IEventService
    {
        IEnumerable<EventInformation> GetAllEvents();

        IEnumerable<EventInformation> GetEventsByPersonId(int id);

        IEnumerable<EventInformation> GetImportantEventsByPersonId(int personId);

        IEnumerable<EventInformation> GetAllEventsByPersonIdAndEventType(int personId, string eventType);

        EventInformation GetEventById(int id);

        void AddEvent(EventInformation eventInformation);

        void UpdateEvent(EventInformation eventInformation);

        void DeleteEvent(int id);

        bool IsEventUnique(int personId, string eventType);
    }
}