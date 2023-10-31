using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class MediaPersonService : IMediaPersonService
    {
        private IGenericRepository<MediaPerson> _mediaPersonRepository;

        public MediaPersonService(IGenericRepository<MediaPerson> mediaPersonRepository)
        {
            _mediaPersonRepository = mediaPersonRepository;
        }

        public IEnumerable<MediaPerson> GetAllMediaPeople()
        {
            return _mediaPersonRepository.GetAll();
        }

        public MediaPerson GetMediaPersonById(int id)
        {
            return _mediaPersonRepository.GetById(id);
        }

        public void AddMediaPerson(int personId, int mediaId)
        {
            MediaPerson newMediaPerson = new MediaPerson
            {
                PersonId = personId,
                MediaId = mediaId
            };

            _mediaPersonRepository.Add(newMediaPerson);
            _mediaPersonRepository.Save();
        }

        public void UpdateMediaPerson(int id, int personId, int mediaId)
        {
            MediaPerson existingMediaPerson = _mediaPersonRepository.GetById(id);

            if (existingMediaPerson != null)
            {
                existingMediaPerson.PersonId = personId;
                existingMediaPerson.MediaId = mediaId;

                _mediaPersonRepository.Update(existingMediaPerson);
                _mediaPersonRepository.Save();
            }
        }

        public void DeleteMediaPerson(int id)
        {
            _mediaPersonRepository.Remove(id);
            _mediaPersonRepository.Save();
        }
    }
}
