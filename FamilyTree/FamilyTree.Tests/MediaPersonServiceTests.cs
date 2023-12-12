namespace FamilyTree.Tests
{
    public class MediaPersonServiceTests
    {
        private Mock<IMediaPersonRepository> mediaPersonRepositoryMock;
        private IMediaPersonService mediaPersonService;

        public MediaPersonServiceTests()
        {
            mediaPersonRepositoryMock = new Mock<IMediaPersonRepository>();
            mediaPersonService = new MediaPersonService(mediaPersonRepositoryMock.Object);
        }

        [Fact]
        public void GetAllMediaPeople_ShouldReturnMediaPersonList()
        {
            // Arrange
            var mediaPeopleFromRepository = new List<MediaPerson>
            {
                new MediaPerson { Id = 1, PersonId = 1, MediaId = 1 },
                new MediaPerson { Id = 2, PersonId = 2, MediaId = 2 },
            };

            mediaPersonRepositoryMock.Setup(repo => repo.GetAll()).Returns(mediaPeopleFromRepository);

            // Act
            var result = mediaPersonService.GetAllMediaPeople();

            // Assert
            result.Should().BeEquivalentTo(mediaPeopleFromRepository);
        }

        [Fact]
        public void GetMediaPersonById_ShouldReturnMediaPerson()
        {
            // Arrange
            int mediaPersonId = 1;
            var mediaPersonFromRepository = new MediaPerson { Id = mediaPersonId, PersonId = 1, MediaId = 1 };

            mediaPersonRepositoryMock.Setup(repo => repo.GetById(mediaPersonId)).Returns(mediaPersonFromRepository);

            // Act
            var result = mediaPersonService.GetMediaPersonById(mediaPersonId);

            // Assert
            result.Should().BeEquivalentTo(mediaPersonFromRepository);
        }

        [Fact]
        public void GetAllMediaByPersonId_ShouldReturnMediaList()
        {
            // Arrange
            int personId = 1;
            var mediaListFromRepository = new List<Media>
            {
                new Media { Id = 1, FilePath = "photo1.jpg", MediaType = "photo" },
                new Media { Id = 2, FilePath = "photo2.jpg", MediaType = "photo" },
            };

            mediaPersonRepositoryMock.Setup(repo => repo.GetAllMediaByPersonId(personId)).Returns(mediaListFromRepository);

            // Act
            var result = mediaPersonService.GetAllMediaByPersonId(personId);

            // Assert
            result.Should().BeEquivalentTo(mediaListFromRepository, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void GetAllPersonsIdByMediaId_ShouldReturnPersonsIds()
        {
            // Arrange
            int mediaId = 1;
            var personsIdsFromRepository = new List<int> { 1, 2 };

            mediaPersonRepositoryMock.Setup(repo => repo.GetAllPersonsIdByMediaId(mediaId)).Returns(personsIdsFromRepository);

            // Act
            var result = mediaPersonService.GetAllPersonsIdByMediaId(mediaId);

            // Assert
            result.Should().BeEquivalentTo(personsIdsFromRepository);
        }

        [Fact]
        public void GetMainPhotoByPersonId_ShouldReturnMainPhoto()
        {
            // Arrange
            int personId = 1;
            var mainPhotoFromRepository = new Media { Id = 1, FilePath = "mainPhoto.jpg", MediaType = "photo" };

            mediaPersonRepositoryMock.Setup(repo => repo.GetMainPhotoByPersonId(personId)).Returns(mainPhotoFromRepository);

            // Act
            var result = mediaPersonService.GetMainPhotoByPersonId(personId);

            // Assert
            result.Should().BeEquivalentTo(new Photo(mainPhotoFromRepository), options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void AddMediaPerson_ShouldAddMediaPersonToRepository()
        {
            // Arrange
            int personId = 1;
            int mediaId = 1;

            // Act
            mediaPersonService.AddMediaPerson(personId, mediaId);

            // Assert
            mediaPersonRepositoryMock.Verify(repo => repo.Add(It.IsAny<MediaPerson>()), Times.Once);
            mediaPersonRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void AddMediaPerson_ShouldThrowException()
        {
            // Arrange
            int personId = 1;
            int mediaId = 1;

            mediaPersonRepositoryMock.Setup(repo => repo.Add(It.IsAny<MediaPerson>())).Throws(new Exception());

            // Act
            Action act = () => mediaPersonService.AddMediaPerson(personId, mediaId);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося додати зв'язок між медіа та особою");
            mediaPersonRepositoryMock.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public void UpdateMediaPerson_ShouldUpdateMediaPersonInRepository()
        {
            // Arrange
            int mediaPersonId = 1;
            int personId = 1;
            int mediaId = 2;

            var existingMediaPerson = new MediaPerson { Id = mediaPersonId, PersonId = personId, MediaId = mediaId };

            mediaPersonRepositoryMock.Setup(repo => repo.GetById(mediaPersonId)).Returns(existingMediaPerson);

            // Act
            mediaPersonService.UpdateMediaPerson(mediaPersonId, personId, mediaId);

            // Assert
            mediaPersonRepositoryMock.Verify(repo => repo.Update(It.IsAny<MediaPerson>()), Times.Once);
            mediaPersonRepositoryMock.Verify(repo => repo.Save(), Times.Once);
            existingMediaPerson.PersonId.Should().Be(personId);
            existingMediaPerson.MediaId.Should().Be(mediaId);
        }

        [Fact]
        public void DeleteMediaPerson_ShouldDeleteMediaPersonFromRepository()
        {
            // Arrange
            int mediaPersonId = 1;
            var mediaPersonFromRepository = new MediaPerson { Id = mediaPersonId, PersonId = 1, MediaId = 1 };

            mediaPersonRepositoryMock.Setup(repo => repo.GetById(mediaPersonId)).Returns(mediaPersonFromRepository);

            // Act
            mediaPersonService.DeleteMediaPerson(mediaPersonId);

            // Assert
            mediaPersonRepositoryMock.Verify(repo => repo.Remove(It.IsAny<MediaPerson>()), Times.Once);
            mediaPersonRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteMediaPerson_ShouldThrowException()
        {
            // Arrange
            int mediaPersonId = 1;

            mediaPersonRepositoryMock.Setup(repo => repo.GetById(mediaPersonId)).Throws(new Exception());

            // Act
            Action act = () => mediaPersonService.DeleteMediaPerson(mediaPersonId);

            // Assert
            act.Should().Throw<Exception>();
        }
    }
}

