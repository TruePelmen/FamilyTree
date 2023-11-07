namespace FamilyTree.BLL.Services
{
    using System.Collections.Generic;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class MediaPersonService : IMediaPersonService
    {
        private IMediaPersonRepository mediaPersonRepository;

        public MediaPersonService(IMediaPersonRepository mediaPersonRepository)
        {
            this.mediaPersonRepository = mediaPersonRepository;
        }

        public IEnumerable<MediaPerson> GetAllMediaPeople()
        {
            return this.mediaPersonRepository.GetAll();
        }

        public MediaPerson GetMediaPersonById(int id)
        {
            return this.mediaPersonRepository.GetById(id);
        }

        public Media GetMainPhotoByPersonId(int id)
        {
            return this.mediaPersonRepository.GetMainPhotoByPersonId(id);
        }

        public void AddMediaPerson(int personId, int mediaId)
        {
            MediaPerson newMediaPerson = new MediaPerson
            {
                PersonId = personId,
                MediaId = mediaId,
            };

            this.mediaPersonRepository.Add(newMediaPerson);
            this.mediaPersonRepository.Save();
        }

        public void UpdateMediaPerson(int id, int personId, int mediaId)
        {
            MediaPerson existingMediaPerson = this.mediaPersonRepository.GetById(id);

            if (existingMediaPerson != null)
            {
                existingMediaPerson.PersonId = personId;
                existingMediaPerson.MediaId = mediaId;

                this.mediaPersonRepository.Update(existingMediaPerson);
                this.mediaPersonRepository.Save();
            }
        }

        public void DeleteMediaPerson(int id)
        {
            this.mediaPersonRepository.Remove(this.GetMediaPersonById(id));
            this.mediaPersonRepository.Save();
        }
    }
}
