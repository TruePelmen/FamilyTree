using FamilyTree.BLL.Interfaces;
using FamilyTree.BLL.Services;

namespace FamilyTree.Tests
{
    public class TreePersonServiceTest
    {
        [Fact]
        public void GetAllTreePeople_ShouldReturnTreePersonList()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treePeopleFromRepository = new List<TreePerson>
            {
                new TreePerson { Id = 1, TreeId = 1, PersonId = 1 },
                new TreePerson { Id = 2, TreeId = 1, PersonId = 2 }
            };
            mockTreePersonRepository.Setup(repo => repo.GetAll()).Returns(treePeopleFromRepository);

            // Act
            var result = treePersonService.GetAllTreePeople();

            // Assert
            result.Should().BeEquivalentTo(treePeopleFromRepository);
        }

        [Fact]
        public void GetTreePersonById_ShouldReturnTreePerson()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treePersonId = 1;
            var treePersonFromRepository = new TreePerson { Id = treePersonId, TreeId = 1, PersonId = 1 };
            mockTreePersonRepository.Setup(repo => repo.GetById(treePersonId)).Returns(treePersonFromRepository);

            // Act
            var result = treePersonService.GetTreePersonById(treePersonId);

            // Assert
            result.Should().BeEquivalentTo(treePersonFromRepository);
        }

        [Fact]
        public void GetPersonsNumber_ShouldReturnPersonsNumber()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treeId = 1;
            mockTreePersonRepository.Setup(repo => repo.GetPersonsNumber(treeId)).Returns(2);

            // Act
            var result = treePersonService.GetPersonsNumber(treeId);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void GetEventsNumber_ShouldReturnPersonsNumber()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treeId = 1;
            mockTreePersonRepository.Setup(repo => repo.GetEventsNumber(treeId)).Returns(2);

            // Act
            var result = treePersonService.GetEventsNumber(treeId);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void GetPhotosNumber_ShouldReturnPersonsNumber()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treeId = 1;
            mockTreePersonRepository.Setup(repo => repo.GetPhotosNumber(treeId)).Returns(2);

            // Act
            var result = treePersonService.GetPhotosNumber(treeId);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void GetTreePersonById_ShouldReturnTreePersonList()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treeId = 1;
            var treePeopleFromRepository = new TreePerson { Id = 1, TreeId = treeId, PersonId = 1 };

            mockTreePersonRepository.Setup(repo => repo.GetById(treeId)).Returns(treePeopleFromRepository);

            // Act
            var result = treePersonService.GetTreePersonById(treeId);

            // Assert
            result.Should().BeEquivalentTo(treePeopleFromRepository);
        }

        [Fact]
        public void GetTreePerson_ShouldReturnTreePerson()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treeId = 1;
            var personId = 1;
            var treePersonFromRepository = new TreePerson { Id = 1, TreeId = treeId, PersonId = personId };

            mockTreePersonRepository.Setup(repo => repo.GetTreePerson(treeId, personId)).Returns(treePersonFromRepository);

            // Act
            var result = treePersonService.GetTreePerson(treeId, personId);

            // Assert
            result.Should().BeEquivalentTo(treePersonFromRepository);
        }

        [Fact]
        public void AddTreePerson_ShouldAddTreePersonToRepository()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treePerson = new TreePerson { Id = 1, TreeId = 1, PersonId = 1 };

            // Act
            treePersonService.AddTreePerson(treePerson.TreeId, treePerson.PersonId);

            // Assert
            mockTreePersonRepository.Verify(repo => repo.Add(It.IsAny<TreePerson>()), Times.Once);
            mockTreePersonRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void AddTreePerson_ShouldThrowException()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treePerson = new TreePerson { Id = 1, TreeId = 1, PersonId = 1 };

            mockTreePersonRepository.Setup(repo => repo.Add(It.IsAny<TreePerson>())).Throws(new Exception());

            // Act
            Action act = () => treePersonService.AddTreePerson(treePerson.TreeId, treePerson.PersonId);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося додати особу до дерева");
            mockTreePersonRepository.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public void UpdateTreePerson_ShouldUpdateTreePersonInRepository()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treePerson = new TreePerson { Id = 1, TreeId = 1, PersonId = 1 };
            mockTreePersonRepository.Setup(repo => repo.GetById(1)).Returns(treePerson);

            // Act
            treePersonService.UpdateTreePerson(treePerson.Id, treePerson.TreeId, treePerson.PersonId);

            // Assert
            mockTreePersonRepository.Verify(repo => repo.Update(It.IsAny<TreePerson>()), Times.Once);
            mockTreePersonRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteTreePerson_ShouldRemoveTreePersonFromRepository()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            var treePersonId = 1;
            var treePersonFromRepository = new TreePerson { Id = treePersonId, TreeId = 1, PersonId = 1 };

            mockTreePersonRepository.Setup(repo => repo.GetById(treePersonId)).Returns(treePersonFromRepository);

            // Act
            treePersonService.DeleteTreePerson(1, 1);

            // Assert
            mockTreePersonRepository.Verify(repo => repo.Remove(It.IsAny<TreePerson>()), Times.Once);
            mockTreePersonRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteTreePerson_ShouldThrowException()
        {
            // Arrange
            var mockTreePersonRepository = new Mock<ITreePersonRepository>();
            var treePersonService = new TreePersonService(mockTreePersonRepository.Object);

            mockTreePersonRepository.Setup(repo => repo.GetTreePerson(2,1)).Throws(new Exception());

            Action act = () => treePersonService.DeleteTreePerson(2, 1);

            // Assert
            act.Should().Throw<Exception>();
        }

    }
}
