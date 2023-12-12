namespace FamilyTree.Tests
{
    public class RelationshipServiceTests
    {
        private Mock<IRelationshipRepository> relationshipRepositoryMock;
        private IRelationshipService relationshipService;

        public RelationshipServiceTests()
        {
            relationshipRepositoryMock = new Mock<IRelationshipRepository>();
            relationshipService = new RelationshipService(relationshipRepositoryMock.Object);
        }

        [Fact]
        public void GetAllRelationships_ShouldReturnRelationshipList()
        {
            // Arrange
            var relationshipsFromRepository = new List<Relationship>
            {
                new Relationship { Id = 1, PersonId1 = 1, PersonId2 = 2, RelationshipType = "father-child" },
                new Relationship { Id = 2, PersonId1 = 2, PersonId2 = 3, RelationshipType = "spouse" },
            };

            relationshipRepositoryMock.Setup(repo => repo.GetAll()).Returns(relationshipsFromRepository);

            // Act
            var result = relationshipService.GetAllRelationships();

            // Assert
            result.Should().BeEquivalentTo(relationshipsFromRepository);
        }

        [Fact]
        public void GetRelationshipById_ShouldReturnRelationship()
        {
            // Arrange
            int relationshipId = 1;
            var relationshipFromRepository = new Relationship { Id = relationshipId, PersonId1 = 1, PersonId2 = 2, RelationshipType = "father-child" };

            relationshipRepositoryMock.Setup(repo => repo.GetById(relationshipId)).Returns(relationshipFromRepository);

            // Act
            var result = relationshipService.GetRelationshipById(relationshipId);

            // Assert
            result.Should().BeEquivalentTo(relationshipFromRepository);
        }

        [Fact]
        public void GetMotherIdByPersonId_ShouldReturnMotherId()
        {
            // Arrange
            int personId = 1;
            int motherIdFromRepository = 2;

            relationshipRepositoryMock.Setup(repo => repo.GetParentIdByPersonId(personId, "mother-child")).Returns(motherIdFromRepository);

            // Act
            var result = relationshipService.GetMotherIdByPersonId(personId);

            // Assert
            result.Should().Be(motherIdFromRepository);
        }

        [Fact]
        public void GetFatherIdByPersonId_ShouldReturnFatherId()
        {
            // Arrange
            int personId = 1;
            int fatherIdFromRepository = 3;

            relationshipRepositoryMock.Setup(repo => repo.GetParentIdByPersonId(personId, "father-child")).Returns(fatherIdFromRepository);

            // Act
            var result = relationshipService.GetFatherIdByPersonId(personId);

            // Assert
            result.Should().Be(fatherIdFromRepository);
        }

        [Fact]
        public void GetSpouseIdByPersonId_ShouldReturnSpouseId()
        {
            // Arrange
            int personId = 1;
            int spouseIdFromRepository = 2;

            relationshipRepositoryMock.Setup(repo => repo.GetSpouseIdByPersonId(personId)).Returns(spouseIdFromRepository);

            // Act
            var result = relationshipService.GetSpouseIdByPersonId(personId);

            // Assert
            result.Should().Be(spouseIdFromRepository);
        }

        [Fact]
        public void GetChildrenIdByPersonId_ShouldReturnChildrenIds()
        {
            // Arrange
            int personId = 1;
            var childrenIdsFromRepository = new List<int> { 2, 3 };

            relationshipRepositoryMock.Setup(repo => repo.GetChildrenIdByPersonId(personId)).Returns(childrenIdsFromRepository);

            // Act
            var result = relationshipService.GetChildrenIdByPersonId(personId);

            // Assert
            result.Should().BeEquivalentTo(childrenIdsFromRepository);
        }

        [Fact]
        public void AddRelationship_ShouldAddRelationshipToRepository()
        {
            // Arrange
            int personId1 = 1;
            int personId2 = 2;
            string relationshipType = "father-child";

            // Act
            relationshipService.AddRelationship(personId1, personId2, relationshipType);

            // Assert
            relationshipRepositoryMock.Verify(repo => repo.Add(It.IsAny<Relationship>()), Times.Once);
            relationshipRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void AddRelationship_ShouldThrowException()
        {
            // Arrange
            int personId1 = 1;
            int personId2 = 2;
            string relationshipType = "father-child";

            relationshipRepositoryMock.Setup(repo => repo.Add(It.IsAny<Relationship>())).Throws(new Exception());

            // Act
            Action act = () => relationshipService.AddRelationship(personId1, personId2, relationshipType);

            // Assert
            act.Should().Throw<Exception>().WithMessage("Не вдалося додати зв'язок");
            relationshipRepositoryMock.Verify(repo => repo.Save(), Times.Never);
        }

        [Fact]
        public void UpdateRelationship_ShouldUpdateRelationshipInRepository()
        {
            // Arrange
            int relationshipId = 1;
            int personId1 = 1;
            int personId2 = 2;
            string relationshipType = "father-child";

            var existingRelationship = new Relationship { Id = relationshipId, PersonId1 = personId1, PersonId2 = personId2, RelationshipType = relationshipType };

            relationshipRepositoryMock.Setup(repo => repo.GetById(relationshipId)).Returns(existingRelationship);

            // Act
            relationshipService.UpdateRelationship(relationshipId, personId1, personId2, relationshipType);

            // Assert
            relationshipRepositoryMock.Verify(repo => repo.Update(It.IsAny<Relationship>()), Times.Once);
            relationshipRepositoryMock.Verify(repo => repo.Save(), Times.Once);
            existingRelationship.PersonId1.Should().Be(personId1);
            existingRelationship.PersonId2.Should().Be(personId2);
            existingRelationship.RelationshipType.Should().Be(relationshipType);
        }

        [Fact]
        public void DeleteRelationship_ShouldDeleteRelationshipFromRepository()
        {
            // Arrange
            int relationshipId = 1;
            var relationshipFromRepository = new Relationship { Id = relationshipId, PersonId1 = 1, PersonId2 = 2, RelationshipType = "father-child" };

            relationshipRepositoryMock.Setup(repo => repo.GetById(relationshipId)).Returns(relationshipFromRepository);

            // Act
            relationshipService.DeleteRelationship(relationshipId);

            // Assert
            relationshipRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Relationship>()), Times.Once);
            relationshipRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public void DeleteRelationship_ShouldThrowException()
        {
            // Arrange
            int relationshipId = 1;

            relationshipRepositoryMock.Setup(repo => repo.GetById(relationshipId)).Throws(new Exception());

            // Act
            Action act = () => relationshipService.DeleteRelationship(relationshipId);

            // Assert
            act.Should().Throw<Exception>();
        }
    }
}


