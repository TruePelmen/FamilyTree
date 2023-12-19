namespace FamilyTree.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;
    using Microsoft.EntityFrameworkCore;

    public class MediaEventRepository : GenericRepository<MediaEvent>, IMediaEventRepository
    {
        public IEnumerable<Media> GetAllPhotosForPerson(int personId)
        {
            var mediaEvents = this.context.MediaEvents
                .Include(me => me.Event)
                .Include(me => me.Media)
                .Where(me => me.Event != null && me.Event.PersonId == personId)
                .ToList();

            var photos = mediaEvents
                .Where(me => me.Media.MediaType == "photo")
                .Select(me => me.Media)
                .ToList();

            return photos;
        }

        public IEnumerable<int> GetAllPersonForPhotos(int mediaId)
        {
            var persons = this.context.MediaEvents
        .Where(me => me.MediaId == mediaId)
        .Select(me => me.Event)
        .Where(e => e != null)
        .Select(e => e.PersonId)
        .ToList();

            return persons;
        }

        public IEnumerable<Media> GetAllPhotosForEvent(int eventId)
        {
            var mediaEvents = this.context.MediaEvents
                .Include(me => me.Event)
                .Include(me => me.Media)
                .Where(me => me.EventId == eventId)
                .ToList();

            var photos = mediaEvents
                .Where(me => me.Media.MediaType == "photo")
                .Select(me => me.Media)
                .ToList();

            return photos;
        }
    }
}
