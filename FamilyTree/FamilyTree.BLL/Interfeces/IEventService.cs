using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface IEventService
    {
        IEnumerable<Event> GetAllEvents();
        Event GetEventById(int id);
        void AddEvent(string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age);
        void UpdateEvent(int id, string eventType, DateOnly? eventDate, string? eventPlace, int personId, string? description, int? age);
        void DeleteEvent(int id);
    }
}
