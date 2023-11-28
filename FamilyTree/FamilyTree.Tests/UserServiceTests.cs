namespace FamilyTree.Tests
{
    using BCrypt.Net;
    public class UserServiceTests
    {
        [Fact]
        public void GetAllUsers_ShouldReturnUserList()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var usersFromRepository = new List<User>
            {
                new User { Login = "user1", Password = "password1" },
                new User { Login = "user2", Password = "password2" }
            };
            mockUserRepository.Setup(repo => repo.GetAll()).Returns(usersFromRepository);

            // Act
            var result = userService.GetAllUsers();

            // Assert
            result.Should().BeEquivalentTo(usersFromRepository);
        }

        [Fact]
        public void GetUserByLogin_ShouldReturnUser()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var userLogin = "user1";
            var userFromRepository = new User { Login = userLogin, Password = "password1" };
            mockUserRepository.Setup(repo => repo.GetById(userLogin)).Returns(userFromRepository);

            // Act
            var result = userService.GetUserByLogin(userLogin);

            // Assert
            result.Should().BeEquivalentTo(userFromRepository);
        }

        [Fact]
        public void FindUserByLogin_ShouldReturnTrue()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var userLogin = "user1";
            var userFromRepository = new User { Login = userLogin, Password = "password1" };
            mockUserRepository.Setup(repo => repo.GetById(userLogin)).Returns(userFromRepository);

            // Act
            var result = userService.FindUserByLogin(userLogin);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FindUserByLogin_ShouldReturnFalse()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var userLogin = "user1";
            var userFromRepository = new User { Login = userLogin, Password = "password1" };
            mockUserRepository.Setup(repo => repo.GetById(userLogin)).Returns(userFromRepository);

            // Act
            var result = userService.FindUserByLogin("user2");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void AddUser_ShouldAddUserToRepository()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var userToAdd = new User
            {
                Login = "user1",
                Password = "password1",
            };

            // Act
            userService.AddUser(userToAdd.Login, userToAdd.Password);

            // Assert
            mockUserRepository.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
            mockUserRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void AddUser_ShouldThrowException()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var userToAdd = new User
            {
                Login = "user1",
                Password = "password1",
            };

            mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>())).Throws(new Exception());

            // Act
            Action act = () => userService.AddUser(userToAdd.Login, userToAdd.Password);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося додати користувача");
            mockUserRepository.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUserInRepository()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var existingUserLogin = "user1";
            var existingUser = new User { Login = existingUserLogin, Password = "password1" };

            mockUserRepository.Setup(repo => repo.GetById(existingUserLogin)).Returns(existingUser);

            var updatedUser = new User
            {
                Login = existingUserLogin,
                Password = "password2",
            };

            // Act
            userService.UpdateUser(updatedUser.Login, updatedUser.Password);

            // Assert
            mockUserRepository.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
            mockUserRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteUser_ShouldDeleteUserFromRepository()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var existingUserLogin = "user1";
            var existingUser = new User { Login = existingUserLogin, Password = "password1" };

            mockUserRepository.Setup(repo => repo.GetById(existingUserLogin)).Returns(existingUser);

            // Act
            userService.DeleteUser(existingUserLogin);

            // Assert
            mockUserRepository.Verify(repo => repo.Remove(It.IsAny<User>()), Times.Once);
            mockUserRepository.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteUser_ShouldThrowException()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var existingUserLogin = "user1";
            var existingUser = new User { Login = existingUserLogin, Password = "password1" };

            mockUserRepository.Setup(repo => repo.GetById(existingUserLogin)).Returns(existingUser);
            mockUserRepository.Setup(repo => repo.Remove(It.IsAny<User>())).Throws(new Exception());

            // Act
            Action act = () => userService.DeleteUser(existingUserLogin);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося видалити користувача");
            mockUserRepository.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public void AuthenticateUser_ShouldReturnTrueForValidCredentials()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var login = "validuser";
            var password = "validpassword";
            var hashedPassword = BCrypt.HashPassword(password);
            var validUser = new User { Login = login, Password = hashedPassword };

            mockUserRepository.Setup(repo => repo.GetById(login)).Returns(validUser);

            // Act
            var result = userService.AuthenticateUser(login, password);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void AuthenticateUser_ShouldReturnFalseForInvalidCredentials()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var login = "validuser";
            var validPassword = "validpassword";
            var invalidPassword = "invalidpassword";
            var hashedPassword = BCrypt.HashPassword(validPassword);
            var validUser = new User { Login = login, Password = hashedPassword };

            mockUserRepository.Setup(repo => repo.GetById(login)).Returns(validUser);

            // Act
            var result = userService.AuthenticateUser(login, invalidPassword);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void AuthenticateUser_ShouldReturnFalseForNonExistingUser()
        {
            // Arrange
            var mockUserRepository = new Mock<IGenericRepository<User>>();
            var userService = new UserService(mockUserRepository.Object);

            var nonExistingUserLogin = "nonexistentuser";

            mockUserRepository.Setup(repo => repo.GetById(nonExistingUserLogin)).Returns((User)null);

            // Act
            var result = userService.AuthenticateUser(nonExistingUserLogin, "anypassword");

            // Assert
            result.Should().BeFalse();
        }
    }
}
