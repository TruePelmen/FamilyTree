using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class MediaService : IMediaService
    {
        private IGenericRepository<Media> _mediaRepository;

        public MediaService(IGenericRepository<Media> mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public IEnumerable<Media> GetAllMedia()
        {
            return _mediaRepository.GetAll();
        }

        public Media GetMediaById(int id)
        {
            return _mediaRepository.GetById(id);
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

            _mediaRepository.Add(media);
            _mediaRepository.Save();
        }

        public void UpdateMedia(int id, string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto)
        {
            Media media = _mediaRepository.GetById(id);

            if (media != null)
            {
                media.MediaType = mediaType;
                media.FilePath = filePath;
                media.TaggedPerson = taggedPerson;
                media.Description = description;
                media.Date = date;
                media.Place = place;
                media.MainPhoto = mainPhoto;

                _mediaRepository.Update(media);
                _mediaRepository.Save();
            }
        }

        public void DeleteMedia(int id)
        {
            _mediaRepository.Remove(id);
            _mediaRepository.Save();
        }
    }
}
