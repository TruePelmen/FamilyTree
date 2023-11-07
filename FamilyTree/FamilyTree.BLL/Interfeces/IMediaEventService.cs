namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public interface IMediaEventService
    {
        IEnumerable<MediaEvent> GetAllMediaEvents();

        MediaEvent GetMediaEventById(int id);

        void AddMediaEvent(int? eventId, int mediaId);

        void UpdateMediaEvent(int id, int? eventId, int mediaId);

        void DeleteMediaEvent(int id);
    }
}
