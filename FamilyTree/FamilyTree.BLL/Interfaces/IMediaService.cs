namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

#nullable enable
    /// <summary>
    /// Interface for managing media content in the family tree.
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Retrieves all media content in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of Media objects representing all media content.</returns>
        IEnumerable<Photo> GetAllMedia();

        /// <summary>
        /// Retrieves media content by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the media content.</param>
        /// <returns>The Media object with the specified identifier, or null if not found.</returns>
        Photo GetMediaById(int id);

        void AddMedia(Photo photo);

        void UpdateMedia(Photo photo);

        /// <summary>
        /// Deletes media content from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the media content to delete.</param>
        void DeleteMedia(int id);
    }
}
