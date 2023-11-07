namespace FamilyTree.BLL.Services
{
    using System.Collections.Generic;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class MediaEventService : IMediaEventService
    {
        private IGenericRepository<MediaEvent> mediaEventRepository;

        public MediaEventService(IGenericRepository<MediaEvent> mediaEventRepository)
        {
            this.mediaEventRepository = mediaEventRepository;
        }

        public IEnumerable<MediaEvent> GetAllMediaEvents()
        {
            return this.mediaEventRepository.GetAll();
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

            this.mediaEventRepository.Add(newMediaEvent);
            this.mediaEventRepository.Save();
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
            this.mediaEventRepository.Remove(this.GetMediaEventById(id));
            this.mediaEventRepository.Save();
        }
    }
}
