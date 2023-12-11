namespace FamilyTree.Tests
{
    public class SpecialRecordServiceTest
    {
        [Fact]
        public void GetAllSpecialRecords_ShouldReturnSpecialRecordInformationList()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var specialRecordsFromRepository = new List<SpecialRecord>
            {
                new SpecialRecord { Id = 1, RecordType = "metric book", HouseNumber = 12, Priest = "Іван Вербицький", Record = "Деталі запису", EventId = 1 },
                new SpecialRecord { Id = 2, RecordType = "metric book", HouseNumber = 10, Priest = "Василь Лисецький", Record = "Невідомо", EventId = 2 }
            };

            mockSpecialRecordRepository.Setup(repo => repo.GetAll()).Returns(specialRecordsFromRepository);

            // Act
            var result = specialRecordService.GetAllSpecialRecords();

            // Assert
            result.Should().BeEquivalentTo(
                specialRecordsFromRepository.Select(record => new SpecialRecordInformation(record))
            );
        }

        [Fact]
        public void GetAllSpecialRecordsForEvent_ShouldReturnSpecialRecordInformationList()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var eventId = 1;
            var specialRecordsFromRepository = new List<SpecialRecord>
            {
                new SpecialRecord { Id = 1, RecordType = "metric book", HouseNumber = 12, Priest = "Іван Вербицький", Record = "Деталі запису", EventId = eventId },
                new SpecialRecord { Id = 2, RecordType = "metric book", HouseNumber = 10, Priest = "Василь Лисецький", Record = "Невідомо", EventId = eventId }
            };

            mockSpecialRecordRepository.Setup(repo => repo.GetAllSpecialRecordsForEvent(eventId)).Returns(specialRecordsFromRepository);

            // Act
            var result = specialRecordService.GetAllSpecialRecordsForEvent(eventId);

            // Assert
            result.Should().BeEquivalentTo(
                specialRecordsFromRepository.Select(record => new SpecialRecordInformation(record))
            );
        }

        [Fact]
        public void GetSpecialRecordById_ShouldReturnSpecialRecordInformation()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var specialRecordId = 1;
            var specialRecordFromRepository = new SpecialRecord { Id = specialRecordId, RecordType = "metric book", HouseNumber = 12, Priest = "Іван Вербицький", Record = "Деталі запису", EventId = 1 };

            mockSpecialRecordRepository.Setup(repo => repo.GetById(specialRecordId)).Returns(specialRecordFromRepository);

            // Act
            var result = specialRecordService.GetSpecialRecordById(specialRecordId);

            // Assert
            result.Should().BeEquivalentTo(new SpecialRecordInformation(specialRecordFromRepository));
        }

        [Fact]
        public void AddSpecialRecord_ShouldAddSpecialRecordToRepository()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var specialRecordToAdd = new SpecialRecordInformation
            {
                RecordType = "metric book",
                HouseNumber = 12,
                Priest = "Іван Вербицький",
                Record = "Деталі запису",
                EventId = 1
            };

            // Act
            specialRecordService.AddSpecialRecord(specialRecordToAdd);

            // Assert
            mockSpecialRecordRepository.Verify(repo => repo.Add(It.IsAny<SpecialRecord>()), Times.Once);
            mockSpecialRecordRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void AddSpecialRecord_ShouldThrowException()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var specialRecordToAdd = new SpecialRecordInformation
            {
                RecordType = "metric book",
                HouseNumber = 12,
                Priest = "Іван Вербицький",
                Record = "Деталі запису",
                EventId = 1
            };

            mockSpecialRecordRepository.Setup(repo => repo.Add(It.IsAny<SpecialRecord>())).Throws(new Exception());

            // Act
            Action addSpecialRecordAction = () => specialRecordService.AddSpecialRecord(specialRecordToAdd);

            // Assert
            addSpecialRecordAction.Should().Throw<Exception>().WithMessage("Невдалося додати спеціальний запис");
            mockSpecialRecordRepository.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public void UpdateSpecialRecord_ShouldUpdateRecordAndSave()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var specialRecordInformation = new SpecialRecordInformation
            {
                Id = 1,
                RecordType = "metric book",
                HouseNumber = 12,
                Priest = "Іван Вербицький",
                Record = "Деталі запису",
                EventId = 1
            };

            mockSpecialRecordRepository.Setup(repo => repo.GetById(1)).Returns(new SpecialRecord { Id = 1, RecordType = "metric book" });
            mockSpecialRecordRepository.Setup(repo => repo.GetById(0)).Returns((SpecialRecord)null);

            // Act
            var result = specialRecordService.UpdateSpecialRecord(specialRecordInformation);

            // Assert
            result.Should().Be("Спеціальний запис успішно оновлено");
            mockSpecialRecordRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockSpecialRecordRepository.Verify(repo => repo.Update(It.IsAny<SpecialRecord>()), Times.Once);
            mockSpecialRecordRepository.Verify(repo => repo.Save(), Times.Once);

            // Act
            result = specialRecordService.UpdateSpecialRecord(new SpecialRecordInformation { Id = 0, RecordType = "some type" });

            // Assert
            result.Should().Be("Спеціальний запис з таким id не знайдено");
            mockSpecialRecordRepository.Verify(repo => repo.GetById(0), Times.Once);
            mockSpecialRecordRepository.VerifyNoOtherCalls();
        }



        [Fact]
        public void DeleteSpecialRecord_ShouldRemoveRecordAndSave()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var recordId = 1;

            mockSpecialRecordRepository.Setup(repo => repo.GetById(recordId)).Returns(new SpecialRecord { Id = recordId });

            // Act
            Action deleteRecordAction = () => specialRecordService.DeleteSpecialRecord(recordId);

            // Assert
            deleteRecordAction.Should().NotThrow();
            mockSpecialRecordRepository.Verify(repo => repo.Remove(It.IsAny<SpecialRecord>()), Times.Once);
            mockSpecialRecordRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteSpecialRecord_ShouldThrowException()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var recordId = 1;

            mockSpecialRecordRepository.Setup(repo => repo.GetById(recordId)).Throws(new Exception());

            // Act
            Action deleteRecordAction = () => specialRecordService.DeleteSpecialRecord(recordId);

            // Assert
            deleteRecordAction.Should().Throw<Exception>().WithMessage("Невдалося видалити спеціальний запис з таким id");
            mockSpecialRecordRepository.Verify(repo => repo.Remove(It.IsAny<SpecialRecord>()), Times.Never);
            mockSpecialRecordRepository.Verify(repo => repo.Save(), Times.Never);
        }


        [Fact]
        public void AreSpecialRecordsOfTypeExistForEvent_ShouldReturnTrueWhenRecordsExist()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var eventId = 1;
            var recordType = "metric book";

            var specialRecordsFromRepository = new List<SpecialRecord>
            {
                new SpecialRecord { Id = 1, RecordType = recordType, HouseNumber = 12, Priest = "Іван Вербицький", Record = "Деталі запису", EventId = eventId }
            };

            mockSpecialRecordRepository.Setup(repo => repo.GetAllSpecialRecordsForEvent(eventId)).Returns(specialRecordsFromRepository);

            // Act
            var result = specialRecordService.AreSpecialRecordsOfTypeExistForEvent(eventId, recordType);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void AreSpecialRecordsOfTypeExistForEvent_ShouldReturnFalseWhenRecordsDoNotExist()
        {
            // Arrange
            var mockSpecialRecordRepository = new Mock<ISpecialRecordRepository>();
            var specialRecordService = new SpecialRecordService(mockSpecialRecordRepository.Object);

            var eventId = 1;
            var recordType = "metric book";

            var specialRecordsFromRepository = new List<SpecialRecord>
            {
                new SpecialRecord { Id = 1, RecordType = "confessional record", HouseNumber = 12, Priest = "Іван Вербицький", Record = "Деталі запису", EventId = eventId }
            };

            mockSpecialRecordRepository.Setup(repo => repo.GetAllSpecialRecordsForEvent(eventId)).Returns(specialRecordsFromRepository);

            // Act
            var result = specialRecordService.AreSpecialRecordsOfTypeExistForEvent(eventId, recordType);

            // Assert
            result.Should().BeFalse();
        }


    }
}
