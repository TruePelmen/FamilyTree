namespace FamilyTree.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.DAL.Models;

    /// <summary>
    /// Interface for managing media events related to the family tree.
    /// </summary>
    public interface IMediaEventService
    {
        /// <summary>
        /// Retrieves all media events in the family tree.
        /// </summary>
        /// <returns>An IEnumerable of MediaEvent objects representing all media events.</returns>
        IEnumerable<MediaEvent> GetAllMediaEvents();

        /// <summary>
        /// Retrieves a media event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the media event.</param>
        /// <returns>The MediaEvent object with the specified identifier, or null if not found.</returns>
        MediaEvent GetMediaEventById(int id);

        IEnumerable<Photo> GetAllPhotosForPerson(int personId);

        IEnumerable<int> GetAllPersonsIdForPhotos(int mediaId);

        /// <summary>
        /// Adds a new media event to the family tree.
        /// </summary>
        /// <param name="eventId">The unique identifier of the associated event.</param>
        /// <param name="mediaId">The unique identifier of the associated media (e.g., photo, video).</param>
        void AddMediaEvent(int? eventId, int mediaId);

        /// <summary>
        /// Updates an existing media event in the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the media event to update.</param>
        /// <param name="eventId">The updated unique identifier of the associated event.</param>
        /// <param name="mediaId">The updated unique identifier of the associated media.</param>
        void UpdateMediaEvent(int id, int? eventId, int mediaId);

        /// <summary>
        /// Deletes a media event from the family tree.
        /// </summary>
        /// <param name="id">The unique identifier of the media event to delete.</param>
        void DeleteMediaEvent(int id);
    }
}
