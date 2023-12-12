namespace FamilyTree.Tests
{
    public class UserTreeServiceTests
    {
        [Fact]
        public void GetAllUserTrees_ShouldReturnUserTreeList()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var userTreesFromRepository = new List<UserTree>
            {
                new UserTree { Id = 1, UserLogin = "user1", TreeId = 1, AccessType = "edit" },
                new UserTree { Id = 2, UserLogin = "user2", TreeId = 2, AccessType = "edit"}
            };

            mockUserTreeRepository.Setup(repo => repo.GetAll()).Returns(userTreesFromRepository);

            // Act
            var result = userTreeService.GetAllUserTrees();

            // Assert
            result.Should().BeEquivalentTo(userTreesFromRepository);
        }

        [Fact]
        public void GetUserTreeById_ShouldReturnUserTree()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var userTreeId = 1;
            var userTreeFromRepository = new UserTree { Id = userTreeId, UserLogin = "user1", TreeId = 1, AccessType = "edit"};

            mockUserTreeRepository.Setup(repo => repo.GetById(userTreeId)).Returns(userTreeFromRepository);

            // Act
            var result = userTreeService.GetUserTreeById(userTreeId);

            // Assert
            result.Should().BeEquivalentTo(userTreeFromRepository);
        }

        [Fact]
        public void GetAllTreeByUserLogin_ShouldReturnTreeList()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var userLogin = "user1";
            var userTreesFromRepository = new List<Tree>
            {
                new Tree { Id = 1, Name = "Дерево Симеренків", PrimaryPerson = 1 },
                new Tree { Id = 2, Name = "Дерево Франків", PrimaryPerson = 2 }
            };

            mockUserTreeRepository.Setup(repo => repo.GetAllTreeByUserLogin(userLogin)).Returns(userTreesFromRepository);

            // Act
            var result = userTreeService.GetAllTreeByUserLogin(userLogin);

            // Assert
            result.Should().BeEquivalentTo(userTreesFromRepository);
        }

        [Fact]
        public void GetFreeUsersLoginByTreeId_ShouldReturnUserLoginList()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var treeId = 1;
            var usersLoginFromRepository = new List<string>
            {
                "user1",
                "user2"
            };

            mockUserTreeRepository.Setup(repo => repo.GetFreeUsersLoginByTreeId(treeId)).Returns(usersLoginFromRepository);

            // Act
            var result = userTreeService.GetFreeUsersLoginByTreeId(treeId);

            // Assert
            result.Should().BeEquivalentTo(usersLoginFromRepository);
        }

        [Fact]
        public void AddUserTree_ShouldAddUserTreeToRepository()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var userTreeToAdd = new UserTree
            {
                UserLogin = "user1",
                TreeId = 1,
                AccessType = "edit",
            };

            // Act
            userTreeService.AddUserTree(userTreeToAdd.UserLogin, userTreeToAdd.TreeId, userTreeToAdd.AccessType);

            // Assert
            mockUserTreeRepository.Verify(repo => repo.Add(It.IsAny<UserTree>()), Times.Once);
            mockUserTreeRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void AddUser_ShouldThrowException()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var userTreeToAdd = new UserTree
            {
                UserLogin = "user1",
                TreeId = 1,
                AccessType = "edit",
            };

            mockUserTreeRepository.Setup(repo => repo.Add(It.IsAny<UserTree>())).Throws(new Exception());

            // Act
            Action act = () => userTreeService.AddUserTree(userTreeToAdd.UserLogin, userTreeToAdd.TreeId, userTreeToAdd.AccessType);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося додати дерево для користувача");
            mockUserTreeRepository.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public void UpdateUserTree_ShouldUpdateUserTreeInRepository()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var userTreeId = 1;
            var userTreeFromRepository = new UserTree { Id = userTreeId, UserLogin = "user1", TreeId = 1, AccessType = "edit" };

            mockUserTreeRepository.Setup(repo => repo.GetById(userTreeId)).Returns(userTreeFromRepository);

            var userTreeToUpdate = new UserTree
            {
                Id = userTreeId,
                UserLogin = "user1",
                TreeId = 1,
                AccessType = "view",
            };

            // Act
            userTreeService.UpdateUserTree(userTreeToUpdate.Id, userTreeToUpdate.UserLogin, userTreeToUpdate.TreeId, userTreeToUpdate.AccessType);

            // Assert
            mockUserTreeRepository.Verify(repo => repo.Update(userTreeFromRepository), Times.Once);
        }

        [Fact]
        public void DeleteUserTree_ShouldDeleteUserTreeFromRepository()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var userTreeId = 1;
            var userTreeFromRepository = new UserTree { Id = userTreeId, UserLogin = "user1", TreeId = 1, AccessType = "edit" };

            mockUserTreeRepository.Setup(repo => repo.GetById(userTreeId)).Returns(userTreeFromRepository);

            // Act
            userTreeService.DeleteUserTree(userTreeId);

            // Assert
            mockUserTreeRepository.Verify(repo => repo.Remove(userTreeFromRepository), Times.Once);
            mockUserTreeRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteUserTree_ShouldThrowException()
        {
            // Arrange
            var mockUserTreeRepository = new Mock<IUserTreeRepository>();
            var userTreeService = new UserTreeService(mockUserTreeRepository.Object);

            var userTreeId = 1;
            var userTreeFromRepository = new UserTree { Id = userTreeId, UserLogin = "user1", TreeId = 1, AccessType = "edit" };

            mockUserTreeRepository.Setup(repo => repo.GetById(userTreeId)).Returns(userTreeFromRepository);
            mockUserTreeRepository.Setup(repo => repo.Remove(It.IsAny<UserTree>())).Throws(new Exception());

            // Act
            Action act = () => userTreeService.DeleteUserTree(userTreeId);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося видалити дерево для користувача");
            mockUserTreeRepository.Verify(repo => repo.Save(), Times.Never);
        }
    }
}
