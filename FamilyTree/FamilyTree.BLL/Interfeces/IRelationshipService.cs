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
        int GetMotherIdByPersonId(int id);
        int GetFatherIdByPersonId(int id);
        int GetSpouseIdByPersonId(int id);
        IEnumerable<int> GetChildrenIdByPersonId(int id);
        void AddRelationship(int personId1, int personId2, string relationshipType);
        void UpdateRelationship(int id, int personId1, int personId2, string relationshipType);
        void DeleteRelationship(int id);
    }
}
