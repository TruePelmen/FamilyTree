using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using FamilyTree.BLL.Interfaces;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class MediaEventService : IMediaEventService
    {
        private IGenericRepository<MediaEvent> _mediaEventRepository;

        public MediaEventService(IGenericRepository<MediaEvent> mediaEventRepository)
        {
            _mediaEventRepository = mediaEventRepository;
        }

        public IEnumerable<MediaEvent> GetAllMediaEvents()
        {
            return _mediaEventRepository.GetAll();
        }

        public MediaEvent GetMediaEventById(int id)
        {
            return _mediaEventRepository.GetById(id);
        }

        public void AddMediaEvent(int? eventId, int mediaId)
        {
            MediaEvent newMediaEvent = new MediaEvent
            {
                EventId = eventId,
                MediaId = mediaId
            };

            _mediaEventRepository.Add(newMediaEvent);
            _mediaEventRepository.Save();
        }

        public void UpdateMediaEvent(int id, int? eventId, int mediaId)
        {
            MediaEvent existingMediaEvent = _mediaEventRepository.GetById(id);

            if (existingMediaEvent != null)
            {
                existingMediaEvent.EventId = eventId;
                existingMediaEvent.MediaId = mediaId;

                _mediaEventRepository.Update(existingMediaEvent);
                _mediaEventRepository.Save();
            }
        }

        public void DeleteMediaEvent(int id)
        {
            _mediaEventRepository.Remove(GetMediaEventById(id));
            _mediaEventRepository.Save();
        }
    }
}
