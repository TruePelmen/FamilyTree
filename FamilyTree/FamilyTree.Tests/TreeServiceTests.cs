namespace FamilyTree.Tests
{
    public class TreeServiceTests
    {
        [Fact]
        public void GetAllTrees_ShouldReturnTreeList()
        {
            // Arrange
            var mockTreeRepository = new Mock<IGenericRepository<Tree>>();
            var mockTreePersonService = new Mock<ITreePersonService>();
            var mockPersonService = new Mock<IPersonService>();
            var treeService = new TreeService(mockTreeRepository.Object, mockTreePersonService.Object, mockPersonService.Object);

            var treesFromRepository = new List<Tree>
            {
            new Tree { Id = 1, Name = "Дерево Симеренків",  PrimaryPerson = 1 },
            new Tree { Id = 2, Name = "Дерево Франків", PrimaryPerson = 2 },
        };

            mockTreeRepository.Setup(repo => repo.GetAll()).Returns(treesFromRepository);

            // Act
            var result = treeService.GetAllTrees();

            // Assert
            result.Should().BeEquivalentTo(treesFromRepository);
        }

        [Fact]
        public void GetTreeById_ShouldReturnTree()
        {
            // Arrange
            var mockTreeRepository = new Mock<IGenericRepository<Tree>>();
            var mockTreePersonService = new Mock<ITreePersonService>();
            var mockPersonService = new Mock<IPersonService>();
            var treeService = new TreeService(mockTreeRepository.Object, mockTreePersonService.Object, mockPersonService.Object);

            var treeId = 1;
            var treeFromRepository = new Tree { Id = treeId, Name = "Дерево Симеренків", PrimaryPerson = 1 };

            mockTreeRepository.Setup(repo => repo.GetById(treeId)).Returns(treeFromRepository);

            // Act
            var result = treeService.GetTreeById(treeId);

            // Assert
            result.Should().BeEquivalentTo(treeFromRepository);
        }

        [Fact]
        public void GetPrimaryPersonId_ShouldReturnPrimaryPersonId()
        {
            // Arrange
            var mockTreeRepository = new Mock<IGenericRepository<Tree>>();
            var mockTreePersonService = new Mock<ITreePersonService>();
            var mockPersonService = new Mock<IPersonService>();
            var treeService = new TreeService(mockTreeRepository.Object, mockTreePersonService.Object, mockPersonService.Object);

            var treeId = 1;
            var treeFromRepository = new Tree { Id = treeId, Name = "Дерево Симеренків", PrimaryPerson = 1 };

            mockTreeRepository.Setup(repo => repo.GetById(treeId)).Returns(treeFromRepository);

            // Act
            var result = treeService.GetPrimaryPersonId(treeId);

            // Assert
            result.Should().Be(treeFromRepository.PrimaryPerson);
        }

        [Fact] 
        public void AddTree_ShouldAddTreeToRepository()
        {
            // Arrange
            var mockTreeRepository = new Mock<IGenericRepository<Tree>>();
            var mockTreePersonService = new Mock<ITreePersonService>();
            var mockPersonService = new Mock<IPersonService>();
            var treeService = new TreeService(mockTreeRepository.Object, mockTreePersonService.Object, mockPersonService.Object);

            var treeToAdd = new Tree
            {
                Name = "Дерево Симеренків",
                PrimaryPerson = 1,
            };

            // Act
            var result = treeService.AddTree(treeToAdd.Name, treeToAdd.PrimaryPerson);

            // Assert
            mockTreeRepository.Verify(repo => repo.Add(It.IsAny<Tree>()), Times.Once);
            mockTreeRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void AddTree_ShouldThrowException()
        {
            // Arrange
            var mockTreeRepository = new Mock<IGenericRepository<Tree>>();
            var mockTreePersonService = new Mock<ITreePersonService>();
            var mockPersonService = new Mock<IPersonService>();
            var treeService = new TreeService(mockTreeRepository.Object, mockTreePersonService.Object, mockPersonService.Object);

            var treeToAdd = new Tree
            {
                Name = "Дерево Симеренків",
                PrimaryPerson = 1,
            };

            mockTreeRepository.Setup(repo => repo.Add(It.IsAny<Tree>())).Throws(new Exception());

            // Act
            Action act = () => treeService.AddTree(treeToAdd.Name, treeToAdd.PrimaryPerson);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося додати дерево");
            mockTreeRepository.Verify(repo => repo.Save(), Times.Never);
        }


        [Fact]
        public void UpdateTree_ShouldUpdateTreeInRepository()
        {
            // Arrange
            var mockTreeRepository = new Mock<IGenericRepository<Tree>>();
            var mockTreePersonService = new Mock<ITreePersonService>();
            var mockPersonService = new Mock<IPersonService>();
            var treeService = new TreeService(mockTreeRepository.Object, mockTreePersonService.Object, mockPersonService.Object);

            var existingTreeId = 1;
            var existingTree = new Tree { Id = existingTreeId, Name = "Дерево Симеренків", PrimaryPerson = 1 };

            mockTreeRepository.Setup(repo => repo.GetById(existingTreeId)).Returns(existingTree);

            // Act
            treeService.UpdateTree(existingTreeId, "Дерево Франків");

            // Assert
            mockTreeRepository.Verify(repo => repo.Update(It.IsAny<Tree>()), Times.Once);
            mockTreeRepository.Verify(repo => repo.Save(), Times.Once);
            existingTree.Name.Should().Be("Дерево Франків");
        }

        [Fact]
        public void DeleteTree_ShouldDeleteTreeFromRepository()
        {
            // Arrange
            var mockTreeRepository = new Mock<IGenericRepository<Tree>>();
            var mockTreePersonService = new Mock<ITreePersonService>();
            var mockPersonService = new Mock<IPersonService>();
            var treeService = new TreeService(mockTreeRepository.Object, mockTreePersonService.Object, mockPersonService.Object);

            var treeId = 1;
            var treeFromRepository = new Tree { Id = treeId, Name = "Дерево Симеренків", PrimaryPerson = 1 };

            mockTreeRepository.Setup(repo => repo.GetById(treeId)).Returns(treeFromRepository);

            // Act
            treeService.DeleteTree(treeId);

            // Assert
            mockTreeRepository.Verify(repo => repo.Remove(It.IsAny<Tree>()), Times.Once);
            mockTreeRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteTree_ShouldThrowException()
        {
            // Arrange
            var mockTreeRepository = new Mock<IGenericRepository<Tree>>();
            var mockTreePersonService = new Mock<ITreePersonService>();
            var mockPersonService = new Mock<IPersonService>();
            var treeService = new TreeService(mockTreeRepository.Object, mockTreePersonService.Object, mockPersonService.Object);
            var treeId = 1;
            mockTreeRepository.Setup(repo => repo.GetById(treeId)).Throws(new Exception());

            // Act
            Action act = () => treeService.DeleteTree(treeId);

            // Assert
            act.Should().Throw<Exception>();
        }
    }
}
