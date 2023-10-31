using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class RelationshipService : IRelationshipService
    {
        private IGenericRepository<Relationship> _relationshipRepository;

        public RelationshipService(IGenericRepository<Relationship> relationshipRepository)
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
            _relationshipRepository.Remove(id);
            _relationshipRepository.Save();
        }
    }
}
