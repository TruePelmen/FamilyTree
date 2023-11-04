using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface IMediaService
    {
        IEnumerable<Media> GetAllMedia();
        Media GetMediaById(int id);
        Media GetMainPhotoByPersonId(int id);
        void AddMedia(string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto);
        void UpdateMedia(int id, string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto);
        void DeleteMedia(int id);
    }
}
