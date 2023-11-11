namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing media content in the family tree.
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Retrieves all media content in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of Media objects representing all media content.</returns>
        IEnumerable<Media> GetAllMedia();

        /// <summary>
        /// Retrieves media content by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the media content.</param>
        /// <returns>The Media object with the specified identifier, or null if not found.</returns>
        Media GetMediaById(int id);

        /// <summary>
        /// Adds new media content to the family tree.
        /// </summary>
        /// <param name="mediaType">The type of media content (e.g., photo, video).</param>
        /// <param name="filePath">The file path or location of the media content.</param>
        /// <param name="taggedPerson">The unique identifier of the person tagged in the media content (null if not tagged).</param>
        /// <param name="description">Additional description of the media content (null if not provided).</param>
        /// <param name="date">The date associated with the media content (null if unknown).</param>
        /// <param name="place">The location associated with the media content (null if unknown).</param>
        /// <param name="mainPhoto">A flag indicating whether this is the main photo (null if not specified).</param>
        void AddMedia(string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto);

        /// <summary>
        /// Updates existing media content in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the media content to update.</param>
        /// <param name="mediaType">The updated type of media content (e.g., photo, video, null if unchanged).</param>
        /// <param name="filePath">The updated file path or location of the media content (null if unchanged).</param>
        /// <param name="taggedPerson">The updated unique identifier of the person tagged in the media content (null if unchanged).</param>
        /// <param name="description">The updated description of the media content (null if unchanged).</param>
        /// <param name="date">The updated date associated with the media content (null if unchanged).</param>
        /// <param name="place">The updated location associated with the media content (null if unchanged).</param>
        /// <param name="mainPhoto">The updated flag indicating whether this is the main photo (null if unchanged).</param>
        void UpdateMedia(int id, string mediaType, string filePath, int? taggedPerson, string? description, DateOnly? date, string? place, bool? mainPhoto);

        /// <summary>
        /// Deletes media content from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the media content to delete.</param>
        void DeleteMedia(int id);
    }
}
