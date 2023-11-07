using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.BLL.Interfaces;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class RelationshipService : IRelationshipService
    {
        private IRelationshipRepository _relationshipRepository;

        public RelationshipService(IRelationshipRepository relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }

        public IEnumerable<Relationship> GetAllRelationships()
        {
            return _relationshipRepository.GetAll();
        }

        public Relationship GetRelationshipById(int id)
        {
            return _relationshipRepository.GetById(id);
        }
        public int GetMotherIdByPersonId(int id)
        {
            return _relationshipRepository.GetParentIdByPersonId(id, "mother-child");
        }
        public int GetFatherIdByPersonId(int id)
        {
            return _relationshipRepository.GetParentIdByPersonId(id, "father-child");
        }
        public int GetSpouseIdByPersonId(int id)
        {
            return _relationshipRepository.GetSpouseIdByPersonId(id);
        }
        public IEnumerable<int> GetChildrenIdByPersonId(int id)
        {
            return _relationshipRepository.GetChildrenIdByPersonId(id);
        }
        public void AddRelationship(int personId1, int personId2, string relationshipType)
        {
            Relationship newRelationship = new Relationship
            {
                PersonId1 = personId1,
                PersonId2 = personId2,
                RelationshipType = relationshipType
            };

            _relationshipRepository.Add(newRelationship);
            _relationshipRepository.Save();
        }

        public void UpdateRelationship(int id, int personId1, int personId2, string relationshipType)
        {
            Relationship existingRelationship = _relationshipRepository.GetById(id);

            if (existingRelationship != null)
            {
                existingRelationship.PersonId1 = personId1;
                existingRelationship.PersonId2 = personId2;
                existingRelationship.RelationshipType = relationshipType;

                _relationshipRepository.Update(existingRelationship);
                _relationshipRepository.Save();
            }
        }

        public void DeleteRelationship(int id)
        {
            _relationshipRepository.Remove(GetRelationshipById(id));
            _relationshipRepository.Save();
        }
    }
}
