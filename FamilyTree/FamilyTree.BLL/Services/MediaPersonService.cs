namespace FamilyTree.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
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

        public IEnumerable<Photo> GetAllMediaByPersonId(int personId)
        {
            return this.mediaPersonRepository.GetAllMediaByPersonId(personId).Select(photo =>
                           new Photo(photo)).ToList();
        }

        public IEnumerable<int> GetAllPersonsIdByMediaId(int mediaId)
        {
            return this.mediaPersonRepository.GetAllPersonsIdByMediaId(mediaId);
        }

        public Photo GetMainPhotoByPersonId(int id)
        {
            return new Photo(this.mediaPersonRepository.GetMainPhotoByPersonId(id));
        }

        public void AddMediaPerson(int personId, int mediaId)
        {
            MediaPerson newMediaPerson = new MediaPerson
            {
                PersonId = personId,
                MediaId = mediaId,
            };
            try
            {
                this.mediaPersonRepository.Add(newMediaPerson);
                this.mediaPersonRepository.Save();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Не вдалося додати зв'язок між медіа та особою");
            }
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
            try
            {
                this.mediaPersonRepository.Remove(this.GetMediaPersonById(id));
                this.mediaPersonRepository.Save();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Не вдалося видалити зв'язок між медіа та особою");
            }
        }
    }
}
