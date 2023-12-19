namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class RelationshipService : IRelationshipService
    {
        private IRelationshipRepository relationshipRepository;

        public RelationshipService(IRelationshipRepository relationshipRepository)
        {
            this.relationshipRepository = relationshipRepository;
        }

        public IEnumerable<Relationship> GetAllRelationships()
        {
            return this.relationshipRepository.GetAll();
        }

        public Relationship GetRelationshipById(int id)
        {
            return this.relationshipRepository.GetById(id);
        }

        public int GetMotherIdByPersonId(int id)
        {
            return this.relationshipRepository.GetParentIdByPersonId(id, "mother-child");
        }

        public int GetFatherIdByPersonId(int id)
        {
            return this.relationshipRepository.GetParentIdByPersonId(id, "father-child");
        }

        public int GetSpouseIdByPersonId(int id)
        {
            return this.relationshipRepository.GetSpouseIdByPersonId(id);
        }

        public IEnumerable<int> GetChildrenIdByPersonId(int id)
        {
            return this.relationshipRepository.GetChildrenIdByPersonId(id);
        }

        public void AddRelationship(int personId1, int personId2, string relationshipType)
        {
            Relationship newRelationship = new Relationship
            {
                PersonId1 = personId1,
                PersonId2 = personId2,
                RelationshipType = relationshipType,
            };
            try
            {
                this.relationshipRepository.Add(newRelationship);
                this.relationshipRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося додати зв'язок");
            }
        }

        public void UpdateRelationship(int id, int personId1, int personId2, string relationshipType)
        {
            Relationship existingRelationship = this.relationshipRepository.GetById(id);

            if (existingRelationship != null)
            {
                existingRelationship.PersonId1 = personId1;
                existingRelationship.PersonId2 = personId2;
                existingRelationship.RelationshipType = relationshipType;

                this.relationshipRepository.Update(existingRelationship);
                this.relationshipRepository.Save();
            }
        }

        public void DeleteRelationship(int id)
        {
            try
            {
                this.relationshipRepository.Remove(this.GetRelationshipById(id));
                this.relationshipRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося видалити зв'язок");
            }
        }
    }
}
