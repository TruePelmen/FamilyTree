using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface IRelationshipService
    {
        IEnumerable<Relationship> GetAllRelationships();
        Relationship GetRelationshipById(int id);
        void AddRelationship(int personId1, int personId2, string relationshipType);
        void UpdateRelationship(int id, int personId1, int personId2, string relationshipType);
        void DeleteRelationship(int id);
    }
}
