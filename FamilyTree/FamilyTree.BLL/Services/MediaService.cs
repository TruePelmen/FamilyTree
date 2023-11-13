namespace FamilyTree.BLL.Services
{
#nullable enable

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class MediaService : IMediaService
    {
        private readonly IGenericRepository<Media> mediaRepository;

        public MediaService(IGenericRepository<Media> mediaRepository)
        {
            this.mediaRepository = mediaRepository;
        }

        public IEnumerable<Photo> GetAllMedia()
        {
            return this.mediaRepository.GetAll().Select(media =>
                           new Photo(media)).ToList();
        }

        public Photo GetMediaById(int id)
        {
            return new Photo(this.mediaRepository.GetById(id));
        }

        public void AddMedia(Photo photo)
        {
            Media media = new Media
            {
                MediaType = photo.MediaType,
                FilePath = photo.FilePath,
                TaggedPerson = photo.TaggedPerson,
                Description = photo.Description,
                Date = photo.Date,
                Place = photo.Place,
                MainPhoto = photo.MainPhoto,
            };

            this.mediaRepository.Add(media);
            this.mediaRepository.Save();
        }

        public void UpdateMedia(Photo photo)
        {
            Media media = this.mediaRepository.GetById(photo.Id);

            if (media != null)
            {
                media.MediaType = photo.MediaType;
                media.FilePath = photo.FilePath;
                media.TaggedPerson = photo.TaggedPerson;
                media.Description = photo.Description;
                media.Date = photo.Date;
                media.Place = photo.Place;
                media.MainPhoto = photo.MainPhoto;

                this.mediaRepository.Update(media);
                this.mediaRepository.Save();
            }
        }

        public void DeleteMedia(int id)
        {
            this.mediaRepository.Remove(this.mediaRepository.GetById(id));
            this.mediaRepository.Save();
        }
    }
}
