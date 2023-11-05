namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
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

        public IEnumerable<Media> GetAllMedia()
        {
            return this.mediaRepository.GetAll();
        }

        public Media GetMediaById(int id)
        {
            return this.mediaRepository.GetById(id);
        }

        public void AddMedia(string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto)
        {
            Media media = new Media
            {
                MediaType = mediaType,
                FilePath = filePath,
                TaggedPerson = taggedPerson,
                Description = description,
                Date = date,
                Place = place,
                MainPhoto = mainPhoto
            };

            this.mediaRepository.Add(media);
            this.mediaRepository.Save();
        }

        public void UpdateMedia(int id, string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto)
        {
            Media media = this.mediaRepository.GetById(id);

            if (media != null)
            {
                media.MediaType = mediaType;
                media.FilePath = filePath;
                media.TaggedPerson = taggedPerson;
                media.Description = description;
                media.Date = date;
                media.Place = place;
                media.MainPhoto = mainPhoto;

                this.mediaRepository.Update(media);
                this.mediaRepository.Save();
            }
        }

        public void DeleteMedia(int id)
        {
            this.mediaRepository.Remove(this.GetMediaById(id));
            this.mediaRepository.Save();
        }
    }
}
