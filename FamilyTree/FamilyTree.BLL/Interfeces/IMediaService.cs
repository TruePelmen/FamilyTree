namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public interface IMediaService
    {
        IEnumerable<Media> GetAllMedia();

        Media GetMediaById(int id);

        void AddMedia(string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto);

        void UpdateMedia(int id, string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto);

        void DeleteMedia(int id);
    }
}
