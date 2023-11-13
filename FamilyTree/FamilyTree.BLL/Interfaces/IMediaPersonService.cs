namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing media associated with persons in the family tree.
    /// </summary>
    public interface IMediaPersonService
    {
        /// <summary>
        /// Retrieves all media associated with persons in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of MediaPerson objects representing all media associations with persons.</returns>
        IEnumerable<MediaPerson> GetAllMediaPeople();

        IEnumerable<Photo> GetAllMediaByPersonId(int personId);

        IEnumerable<int> GetAllPersonsIdByMediaId(int mediaId);

        /// <summary>
        /// Retrieves a media association with a person by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the media association with a person.</param>
        /// <returns>The MediaPerson object with the specified identifier, or null if not found.</returns>
        MediaPerson GetMediaPersonById(int id);

        /// <summary>
        /// Retrieves the main photo associated with a specific person.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <returns>The Media object representing the main photo of the person, or null if not found.</returns>
        Photo GetMainPhotoByPersonId(int id);

        /// <summary>
        /// Adds a new media association with a person in the family tree.
        /// </summary>
        /// <param name="personId">The unique identifier of the person associated with the media.</param>
        /// <param name="mediaId">The unique identifier of the media (e.g., photo, video).</param>
        void AddMediaPerson(int personId, int mediaId);

        /// <summary>
        /// Updates an existing media association with a person in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the media association with a person to update.</param>
        /// <param name="personId">The updated unique identifier of the person associated with the media (null if unchanged).</param>
        /// <param name="mediaId">The updated unique identifier of the media (null if unchanged).</param>
        void UpdateMediaPerson(int id, int personId, int mediaId);

        /// <summary>
        /// Deletes a media association with a person from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the media association with a person to delete.</param>
        void DeleteMediaPerson(int id);
    }
}