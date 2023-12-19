namespace FamilyTree.Tests
{
    public class MediaEventServiceTests
    {
        private Mock<IMediaEventRepository> mediaEventRepositoryMock;
        private IMediaEventService mediaEventService;

        public MediaEventServiceTests()
        {
            mediaEventRepositoryMock = new Mock<IMediaEventRepository>();
            mediaEventService = new MediaEventService(mediaEventRepositoryMock.Object);
        }

        [Fact]
        public void GetAllMediaEvents_ShouldReturnMediaEventList()
        {
            // Arrange
            var mediaEventsFromRepository = new List<MediaEvent>
            {
                new MediaEvent { Id = 1, EventId = 1, MediaId = 1 },
                new MediaEvent { Id = 2, EventId = 2, MediaId = 2 },
            };

            mediaEventRepositoryMock.Setup(repo => repo.GetAll()).Returns(mediaEventsFromRepository);

            // Act
            var result = mediaEventService.GetAllMediaEvents();

            // Assert
            result.Should().BeEquivalentTo(mediaEventsFromRepository);
        }

        [Fact]
        public void GetAllPhotosForPerson_ShouldReturnPhotoList()
        {
            // Arrange
            int personId = 1;
            var photoListFromRepository = new List<Media>
            {
                new Media { Id = 1, FilePath = "photo1.jpg", MediaType = "photo" },
                new Media { Id = 2, FilePath = "photo2.jpg", MediaType = "photo" },
            };

            mediaEventRepositoryMock.Setup(repo => repo.GetAllPhotosForPerson(personId)).Returns(photoListFromRepository);

            // Act
            var result = mediaEventService.GetAllPhotosForPerson(personId);

            // Assert
            result.Should().BeEquivalentTo(photoListFromRepository, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetAllPersonsIdForPhotos_ShouldReturnPersonsIds()
        {
            // Arrange
            int mediaId = 1;
            var personsIdsFromRepository = new List<int> { 1, 2 };

            mediaEventRepositoryMock.Setup(repo => repo.GetAllPersonForPhotos(mediaId)).Returns(personsIdsFromRepository);

            // Act
            var result = mediaEventService.GetAllPersonsIdForPhotos(mediaId);

            // Assert
            result.Should().BeEquivalentTo(personsIdsFromRepository);
        }

        [Fact]
        public void GetAllPhotosForEvent_ShouldReturnPhotoList()
        {
            // Arrange
            int eventId = 1;
            var photoListFromRepository = new List<Media>
            {
                new Media { Id = 1, FilePath = "photo1.jpg", MediaType = "photo" },
                new Media { Id = 2, FilePath = "photo2.jpg", MediaType = "photo" },
            };

            mediaEventRepositoryMock.Setup(repo => repo.GetAllPhotosForEvent(eventId)).Returns(photoListFromRepository);

            // Act
            var result = mediaEventService.GetAllPhotosForEvent(eventId);

            // Assert
            result.Should().BeEquivalentTo(photoListFromRepository, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetMediaEventById_ShouldReturnMediaEvent()
        {
            // Arrange
            int mediaEventId = 1;
            var mediaEventFromRepository = new MediaEvent { Id = mediaEventId, EventId = 1, MediaId = 1 };

            mediaEventRepositoryMock.Setup(repo => repo.GetById(mediaEventId)).Returns(mediaEventFromRepository);

            // Act
            var result = mediaEventService.GetMediaEventById(mediaEventId);

            // Assert
            result.Should().BeEquivalentTo(mediaEventFromRepository);
        }

        [Fact]
        public void AddMediaEvent_ShouldAddMediaEventToRepository()
        {
            // Arrange
            int? eventId = 1;
            int mediaId = 1;

            // Act
            mediaEventService.AddMediaEvent(eventId, mediaId);

            // Assert
            mediaEventRepositoryMock.Verify(repo => repo.Add(It.IsAny<MediaEvent>()), Times.Once);
            mediaEventRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void AddMediaEvent_ShouldThrowException()
        {
            // Arrange
            int? eventId = 1;
            int mediaId = 1;

            mediaEventRepositoryMock.Setup(repo => repo.Add(It.IsAny<MediaEvent>())).Throws(new Exception());

            // Act
            Action act = () => mediaEventService.AddMediaEvent(eventId, mediaId);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося додати зв'язок між медіа та подією");
            mediaEventRepositoryMock.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public void UpdateMediaEvent_ShouldUpdateMediaEventInRepository()
        {
            // Arrange
            int mediaEventId = 1;
            int? eventId = 2;
            int mediaId = 3;

            var existingMediaEvent = new MediaEvent { Id = mediaEventId, EventId = eventId, MediaId = mediaId };

            mediaEventRepositoryMock.Setup(repo => repo.GetById(mediaEventId)).Returns(existingMediaEvent);

            // Act
            mediaEventService.UpdateMediaEvent(mediaEventId, eventId, mediaId);

            // Assert
            mediaEventRepositoryMock.Verify(repo => repo.Update(It.IsAny<MediaEvent>()), Times.Once);
            mediaEventRepositoryMock.Verify(repo => repo.Save(), Times.Once);
            existingMediaEvent.EventId.Should().Be(eventId);
            existingMediaEvent.MediaId.Should().Be(mediaId);
        }

        [Fact]
        public void DeleteMediaEvent_ShouldDeleteMediaEventFromRepository()
        {
            // Arrange
            int mediaEventId = 1;
            var mediaEventFromRepository = new MediaEvent { Id = mediaEventId, EventId = 1, MediaId = 1 };

            mediaEventRepositoryMock.Setup(repo => repo.GetById(mediaEventId)).Returns(mediaEventFromRepository);

            // Act
            mediaEventService.DeleteMediaEvent(mediaEventId);

            // Assert
            mediaEventRepositoryMock.Verify(repo => repo.Remove(It.IsAny<MediaEvent>()), Times.Once);
            mediaEventRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteMediaEvent_ShouldThrowException()
        {
            // Arrange
            int mediaEventId = 1;

            mediaEventRepositoryMock.Setup(repo => repo.GetById(mediaEventId)).Throws(new Exception());

            // Act
            Action act = () => mediaEventService.DeleteMediaEvent(mediaEventId);

            // Assert
            act.Should().Throw<Exception>();
        }
    }
}

