namespace FamilyTree.Tests
{
    public class MediaServiceTests
    {
        [Fact]
        public void GetAllMedia_ShouldReturnPhotoList()
        {
            // Arrange
            var mockMediaRepository = new Mock<IGenericRepository<Media>>();
            var mediaService = new MediaService(mockMediaRepository.Object);

            var mediaFromRepository = new List<Media>
        {
            new Media { Id = 1, MediaType = "photo", FilePath = "path/to/photo.jpg", Date = new DateOnly(2000, 1, 1), Description = "Перше фото" },
            new Media { Id = 2, MediaType = "photo", FilePath = "path/to/photo.png", Date = new DateOnly(2000, 1, 1), Description = "Друге фото" },
        };

            mockMediaRepository.Setup(repo => repo.GetAll()).Returns(mediaFromRepository);

            // Act
            var result = mediaService.GetAllMedia();

            // Assert
            result.Should().BeEquivalentTo(mediaFromRepository.Select(media => new Photo(media)));
        }

        [Fact]
        public void GetMediaById_ShouldReturnPhoto()
        {
            // Arrange
            var mockMediaRepository = new Mock<IGenericRepository<Media>>();
            var mediaService = new MediaService(mockMediaRepository.Object);

            var mediaId = 1;
            var mediaFromRepository = new Media { Id = mediaId, MediaType = "photo", FilePath = "path/to/photo.jpg", Date = new DateOnly(2000, 1, 1), Description = "Фото" };

            mockMediaRepository.Setup(repo => repo.GetById(mediaId)).Returns(mediaFromRepository);

            // Act
            var result = mediaService.GetMediaById(mediaId);

            // Assert
            result.Should().BeEquivalentTo(new Photo(mediaFromRepository));
        }

        [Fact]
        public void AddMedia_ShouldAddMediaToRepository()
        {
            // Arrange
            var mockMediaRepository = new Mock<IGenericRepository<Media>>();
            var mediaService = new MediaService(mockMediaRepository.Object);

            var photoToAdd = new Photo
            {
                MediaType = "photo",
                FilePath = "path/to/new/photo.jpg",
                Date = new DateOnly(2000, 1, 1),
                Description = "Нове фото",
            };

            // Act
            mediaService.AddMedia(photoToAdd);

            // Assert
            mockMediaRepository.Verify(repo => repo.Add(It.IsAny<Media>()), Times.Once);
            mockMediaRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void UpdateMedia_ShouldUpdateMediaInRepository()
        {
            // Arrange
            var mockMediaRepository = new Mock<IGenericRepository<Media>>();
            var mediaService = new MediaService(mockMediaRepository.Object);

            var existingMediaId = 1;
            var existingMedia = new Media { Id = existingMediaId, MediaType = "photo", FilePath = "path/to/photo.jpg", Date = new DateOnly(2000, 1, 1), Description = "Початкове фото" };

            mockMediaRepository.Setup(repo => repo.GetById(existingMediaId)).Returns(existingMedia);

            var updatedPhoto = new Photo
            {
                Id = existingMediaId,
                MediaType = "photo",
                FilePath = "path/to/updated/photo.jpg",
                Date = new DateOnly(2000, 1, 1),
                Description = "Оновлене фото",
            };

            // Act
            mediaService.UpdateMedia(updatedPhoto);

            // Assert
            mockMediaRepository.Verify(repo => repo.Update(It.IsAny<Media>()), Times.Once);
            mockMediaRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteMedia_ShouldRemoveMediaFromRepository()
        {
            // Arrange
            var mockMediaRepository = new Mock<IGenericRepository<Media>>();
            var mediaService = new MediaService(mockMediaRepository.Object);

            var mediaIdToDelete = 1;
            var existingMedia = new Media { Id = mediaIdToDelete, MediaType = "photo", FilePath = "path/to/photo.jpg", Date = new DateOnly(2000, 1, 1), Description = "Фото" };

            mockMediaRepository.Setup(repo => repo.GetById(mediaIdToDelete)).Returns(existingMedia);

            // Act
            mediaService.DeleteMedia(mediaIdToDelete);

            // Assert
            mockMediaRepository.Verify(repo => repo.Remove(It.IsAny<Media>()), Times.Once);
            mockMediaRepository.Verify(repo => repo.Save(), Times.Once);
        }

    }

}
