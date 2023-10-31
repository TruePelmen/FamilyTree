using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface IMediaEventService
    {
        IEnumerable<MediaEvent> GetAllMediaEvents();
        MediaEvent GetMediaEventById(int id);
        void AddMediaEvent(int? eventId, int mediaId);
        void UpdateMediaEvent(int id, int? eventId, int mediaId);
        void DeleteMediaEvent(int id);
    }
}
