namespace FamilyTree.Tests
{
    public class PersonServiseTests
    {
        [Fact]
        public void GetAllPeople_ShouldReturnPersonInformationList()
        {
            // Arrange
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockEventService = new Mock<IEventService>();
            var mockMediaPersonService = new Mock<IMediaPersonService>();

            var personService = new PersonService(mockPersonRepository.Object, mockEventService.Object, mockMediaPersonService.Object);

            var peopleFromRepository = new List<Person>
            {
                 new Person { Id = 1, FirstName = "Іван", LastName = "Франко", Gender = "male", BirthDate = new DateOnly(2000, 01, 01) },
                 new Person { Id = 2, FirstName = "Ольга", LastName = "Хранко", Gender = "female", MaidenName = "Хоружинська", BirthDate = new DateOnly(1864, 01, 01), DeathDate = new DateOnly(1944, 01, 01)},
            };

            mockPersonRepository.Setup(repo => repo.GetAll()).Returns(peopleFromRepository);

            // Act
            var result = personService.GetAllPeople();

            // Assert
            result.Should().BeEquivalentTo(peopleFromRepository.Select(person => new PersonInformation(person)));
        }

        [Fact]
        public void GetPersonById_ShouldReturnPersonInformationWithBirthAndDeathPlace()
        {
            // Arrange
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockEventService = new Mock<IEventService>();
            var mockMediaPersonService = new Mock<IMediaPersonService>();

            var personService = new PersonService(mockPersonRepository.Object, mockEventService.Object, mockMediaPersonService.Object);

            var personId = 1;
            var personFromRepository = new Person { Id = personId, FirstName = "Іван", LastName = "Франко", Gender = "male", BirthDate = new DateOnly(2000, 01, 01) };

            mockPersonRepository.Setup(repo => repo.GetById(personId)).Returns(personFromRepository);

            var eventsFromService = new List<EventInformation>
            {
                new EventInformation { Id = 1, EventType = "birth", EventPlace = "Нагуєвичі" },
                new EventInformation { Id = 2, EventType = "death", EventPlace = "Львів" },
            };

            mockEventService.Setup(service => service.GetAllEventsByPersonIdAndEventType(personId, "birth")).Returns(eventsFromService.Where(e => e.EventType == "birth"));
            mockEventService.Setup(service => service.GetAllEventsByPersonIdAndEventType(personId, "death")).Returns(eventsFromService.Where(e => e.EventType == "death"));

            // Act
            var result = personService.GetPersonById(personId);

            var person = new PersonInformation(personFromRepository);
            person.BirthPlace = "Нагуєвичі";
            person.DeathPlace = "Львів";

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(person);
        }

        [Fact]
        public void GetShortInformationAboutPerson_ShouldReturnPersonInformationWithMainPhoto()
        {
            // Arrange
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockEventService = new Mock<IEventService>();
            var mockMediaPersonService = new Mock<IMediaPersonService>();

            var personService = new PersonService(mockPersonRepository.Object, mockEventService.Object, mockMediaPersonService.Object);

            var personId = 1;
            var personFromRepository = new Person { Id = personId, FirstName = "Іван", LastName = "Франко", Gender = "male", BirthDate = new DateOnly(2000, 01, 01) };

            mockPersonRepository.Setup(repo => repo.GetById(personId)).Returns(personFromRepository);

            var mainPhotoFromService = new Photo { Id = 1, FilePath = "path/to/photo.jpg" };

            mockMediaPersonService.Setup(service => service.GetMainPhotoByPersonId(personId)).Returns(mainPhotoFromService);

            // Act
            var result = personService.GetShortInformationAboutPerson(personId);

            var person = new PersonInformation(personFromRepository);
            person.MainPhoto = "path/to/photo.jpg";

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(person);
        }

        [Fact]
        public void AddPerson_ShouldAddPersonToRepositoryAndReturnId()
        {
            // Arrange
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockEventService = new Mock<IEventService>();
            var mockMediaPersonService = new Mock<IMediaPersonService>();

            var personService = new PersonService(mockPersonRepository.Object, mockEventService.Object, mockMediaPersonService.Object);

            var personInformation = new PersonInformation
            {
                FirstName = "Іван",
                LastName = "Франко",
                Gender = "male",
                BirthDate = new DateOnly(1980, 1, 1),
            };

            // Act
            var result = personService.AddPerson(personInformation);

            // Assert
            mockPersonRepository.Verify(repo => repo.Add(It.IsAny<Person>()), Times.Once);
            mockPersonRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void UpdatePerson_ShouldUpdatePersonInRepository()
        {
            // Arrange
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockEventService = new Mock<IEventService>();
            var mockMediaPersonService = new Mock<IMediaPersonService>();

            var personService = new PersonService(mockPersonRepository.Object, mockEventService.Object, mockMediaPersonService.Object);

            var personInformation = new PersonInformation
            {
                Id = 1,
                FirstName = "Іван",
                LastName = "Франко",
                Gender = "male",
                BirthDate = new DateOnly(1980, 1, 1),
            };

            var existingPerson = new Person { Id = 1, FirstName = "Ольга", LastName = "Франко", Gender = "female" };

            mockPersonRepository.Setup(repo => repo.GetById(personInformation.Id)).Returns(existingPerson);

            // Act
            personService.UpdatePerson(personInformation);

            // Assert
            mockPersonRepository.Verify(repo => repo.Update(It.IsAny<Person>()), Times.Once);
            mockPersonRepository.Verify(repo => repo.Save(), Times.Once);
            existingPerson.FirstName.Should().Be("Іван");
            existingPerson.LastName.Should().Be("Франко");
            existingPerson.Gender.Should().Be("male");
        }

        [Fact]
        public void DeletePerson_ShouldRemovePersonFromRepository()
        {
            // Arrange
            var mockPersonRepository = new Mock<IPersonRepository>();
            var mockEventService = new Mock<IEventService>();
            var mockMediaPersonService = new Mock<IMediaPersonService>();

            var personService = new PersonService(mockPersonRepository.Object, mockEventService.Object, mockMediaPersonService.Object);

            var personId = 1;
            var existingPerson = new Person { Id = personId, FirstName = "Іван", LastName = "Франко", Gender = "male" };

            mockPersonRepository.Setup(repo => repo.GetById(personId)).Returns(existingPerson);

            // Act
            personService.DeletePerson(personId);

            // Assert
            mockPersonRepository.Verify(repo => repo.Remove(It.IsAny<Person>()), Times.Once);
            mockPersonRepository.Verify(repo => repo.Save(), Times.Once);
        }
    }
}
