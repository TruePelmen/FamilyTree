namespace FamilyTree.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public void GetAllEvents_ShouldReturnEventInformationList()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var eventsFromRepository = new List<Event>
        {
            new Event { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = 1, Description = "Народження особи", EventPlace = "Львів", Age = 0},
            new Event { Id = 2, EventType = "birth", EventDate = new DateOnly(2010, 01, 01), PersonId = 3},
            new Event { Id = 3, EventType = "other", EventDate = new DateOnly(2003, 01, 01), PersonId = 1, Description = "Хрещення особи", EventPlace = "Рівне", Age = 3},
            new Event { Id = 4, EventType = "marriage", EventDate = new DateOnly(2023, 01, 01), PersonId = 1, Description = "Одруження особи"},
            new Event { Id = 5, EventType = "death", EventDate = new DateOnly(2020, 01, 01), PersonId = 2, Description = "Смерть Особи", EventPlace = "Київ", Age = 70 },
        };

            mockEventRepository.Setup(repo => repo.GetAll()).Returns(eventsFromRepository);

            /// Act
            var result = eventService.GetAllEvents();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(
                eventsFromRepository.Select(eventInfo => new EventInformation(eventInfo))
            );
        }

        [Fact]
        public void GetEventsByPersonId_ShouldReturnEventInformationList()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var personId = 1;
            var eventsFromRepository = new List<Event>
        {
            new Event { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = 1, Description = "Народження особи", EventPlace = "Львів", Age = 0},
            new Event { Id = 3, EventType = "other", EventDate = new DateOnly(2003, 01, 01), PersonId = 1, Description = "Хрещення особи", EventPlace = "Рівне", Age = 3},
            new Event { Id = 4, EventType = "marriage", EventDate = new DateOnly(2023, 01, 01), PersonId = 1, Description = "Одруження особи"},
        };

            mockEventRepository.Setup(repo => repo.GetEventsByPersonId(personId)).Returns(eventsFromRepository);

            // Act
            var result = eventService.GetEventsByPersonId(personId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(
                eventsFromRepository.Select(eventInfo => new EventInformation(eventInfo))
            );
        }

        [Fact]
        public void GetImportantEventsByPersonId_ShouldReturnEventInformationList()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var personId = 1;
            var eventsFromRepository = new List<Event>
        {
            new Event { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = 1, Description = "Народження особи", EventPlace = "Львів", Age = 0},
            new Event { Id = 4, EventType = "marriage", EventDate = new DateOnly(2023, 01, 01), PersonId = 1, Description = "Одруження особи"},
        };

            mockEventRepository.Setup(repo => repo.GetImportantEventsByPersonId(personId)).Returns(eventsFromRepository);

            // Act
            var result = eventService.GetImportantEventsByPersonId(personId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(
                eventsFromRepository.Select(eventInfo => new EventInformation(eventInfo))
            );
        }

        [Fact]
        public void GetAllEventsByPersonIdAndEventType_ShouldReturnEventInformationList()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var personId = 1;
            var eventType = "birth";
            var eventsFromRepository = new List<Event>
        {
           new Event { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = 1, Description = "Народження особи", EventPlace = "Львів", Age = 0},
        };

            mockEventRepository.Setup(repo => repo.GetEventsByPersonIdAndEventType(personId, eventType)).Returns(eventsFromRepository);

            // Act
            var result = eventService.GetAllEventsByPersonIdAndEventType(personId, eventType);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(
                eventsFromRepository.Select(eventInfo => new EventInformation(eventInfo))
            );
        }

        [Fact]
        public void GetEventById_ShouldReturnEventInformation()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var eventId = 1;
            var eventFromRepository = new Event { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = 1, Description = "Народження особи", EventPlace = "Львів", Age = 0 };

            mockEventRepository.Setup(repo => repo.GetById(eventId)).Returns(eventFromRepository);

            // Act
            var result = eventService.GetEventById(eventId);

            // Assert
            result.Should().BeEquivalentTo(new EventInformation(eventFromRepository));
        }

        [Fact]
        public void AddEvent_ShouldAddEventToRepository()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var eventInformation = new EventInformation { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = 1, Description = "Народження особи", EventPlace = "Львів", Age = 0 };

            // Act
            eventService.AddEvent(eventInformation);

            // Assert
            mockEventRepository.Verify(repo => repo.Add(It.IsAny<Event>()), Times.Once);
            mockEventRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void UpdateEvent_ShouldUpdateEventInRepository()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var eventInformation = new EventInformation { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = 1, Description = "Народження особи", EventPlace = "Львів", Age = 0 };

            mockEventRepository.Setup(repo => repo.GetById(1)).Returns(new Event { Id = 1, EventType = "birth", EventDate = new DateOnly(1980, 01, 01), PersonId = 1, Description = "Previous Event" });

            // Act
            eventService.UpdateEvent(eventInformation);

            // Assert
            mockEventRepository.Verify(repo => repo.Update(It.IsAny<Event>()), Times.Once);
            mockEventRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteEvent_ShouldRemoveEventFromRepository()
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var eventId = 1;

            mockEventRepository.Setup(repo => repo.GetById(eventId)).Returns(new Event { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = 1, Description = "Народження особи", EventPlace = "Львів", Age = 0 });

            // Act
            eventService.DeleteEvent(eventId);

            // Assert
            mockEventRepository.Verify(repo => repo.Remove(It.IsAny<Event>()), Times.Once);
            mockEventRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Theory]
        [InlineData("birth")]
        [InlineData("death")]
        public void IsEventUnique_ShouldReturnFalseForExistingEvents(string eventType)
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var personId = 1;

            var existingEvents = new List<Event>
        {
            new Event { Id = 1, EventType = eventType, EventDate = new DateOnly(2000, 01, 01), PersonId = personId, Description = "Existing Event" },
            new Event { Id = 2, EventType = eventType, EventDate = new DateOnly(2000, 01, 01), PersonId = personId, Description = "Another Existing Event" },
        };

            mockEventRepository.Setup(repo => repo.GetEventsByPersonIdAndEventType(personId, eventType)).Returns(existingEvents);

            // Act
            var result = eventService.IsEventUnique(personId, eventType);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("other")]
        public void IsEventUnique_ShouldReturnTrueForNonExistingEvents(string eventType)
        {
            // Arrange
            var mockEventRepository = new Mock<IEventRepository>();
            var eventService = new EventService(mockEventRepository.Object);

            var personId = 1;

            var existingEvents = new List<Event>
        {
            new Event { Id = 1, EventType = "birth", EventDate = new DateOnly(2000, 01, 01), PersonId = personId, Description = "Existing Event" },
            new Event { Id = 2, EventType = "death", EventDate = new DateOnly(2000, 01, 01), PersonId = personId, Description = "Another Existing Event" },
        };

            mockEventRepository.Setup(repo => repo.GetEventsByPersonIdAndEventType(personId, eventType)).Returns(existingEvents);

            // Act
            var result = eventService.IsEventUnique(personId, eventType);

            // Assert
            result.Should().BeTrue();
        }
    }

}
