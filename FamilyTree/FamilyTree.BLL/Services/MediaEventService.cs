namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class MediaEventService : IMediaEventService
    {
        private IMediaEventRepository mediaEventRepository;

        public MediaEventService(IMediaEventRepository mediaEventRepository)
        {
            this.mediaEventRepository = mediaEventRepository;
        }

        public IEnumerable<MediaEvent> GetAllMediaEvents()
        {
            return this.mediaEventRepository.GetAll();
        }

        public IEnumerable<Photo> GetAllPhotosForPerson(int personId)
        {
            return this.mediaEventRepository.GetAllPhotosForPerson(personId).Select(photo =>
                           new Photo(photo)).ToList();
        }

        public IEnumerable<int> GetAllPersonsIdForPhotos(int mediaId)
        {
            return this.mediaEventRepository.GetAllPersonForPhotos(mediaId);
        }

        public IEnumerable<Photo> GetAllPhotosForEvent(int eventId)
        {
            return this.mediaEventRepository.GetAllPhotosForEvent(eventId).Select(photo =>
                                      new Photo(photo)).ToList();
        }

        public MediaEvent GetMediaEventById(int id)
        {
            return this.mediaEventRepository.GetById(id);
        }

        public void AddMediaEvent(int? eventId, int mediaId)
        {
            MediaEvent newMediaEvent = new MediaEvent
            {
                EventId = eventId,
                MediaId = mediaId,
            };
            try
            {
                this.mediaEventRepository.Add(newMediaEvent);
                this.mediaEventRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося додати зв'язок між медіа та подією");
            }
        }

        public void UpdateMediaEvent(int id, int? eventId, int mediaId)
        {
            MediaEvent existingMediaEvent = this.mediaEventRepository.GetById(id);

            if (existingMediaEvent != null)
            {
                existingMediaEvent.EventId = eventId;
                existingMediaEvent.MediaId = mediaId;

                this.mediaEventRepository.Update(existingMediaEvent);
                this.mediaEventRepository.Save();
            }
        }

        public void DeleteMediaEvent(int id)
        {
            try
            {
                this.mediaEventRepository.Remove(this.GetMediaEventById(id));
                this.mediaEventRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося видалити зв'язок між медіа та подією");
            }
        }
    }
}
