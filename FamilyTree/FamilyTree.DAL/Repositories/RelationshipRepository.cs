namespace FamilyTree.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class RelationshipRepository: GenericRepository<Relationship>, IRelationshipRepository
    {
        public int GetParentIdByPersonId(int personId, string type)
        {
            var fatherRelationship = this.context.Relationships
                .FirstOrDefault(r => r.PersonId2 == personId && r.RelationshipType == type);

            if (fatherRelationship != null)
            {
                return fatherRelationship.PersonId1;
            }

            return -1;
        }

        public int GetSpouseIdByPersonId(int personId)
        {
            var spouseRelationshipFirst = this.context.Relationships
                .Where(r => r.RelationshipType == "spouse" && r.PersonId1 == personId)
                .OrderByDescending(r => r.Id)
                .LastOrDefault();

            if (spouseRelationshipFirst != null)
            {
                return spouseRelationshipFirst.PersonId2;
            }

            var spouseRelationshipSecond = this.context.Relationships
                .Where(r => r.RelationshipType == "spouse" && r.PersonId2 == personId)
                .OrderByDescending(r => r.Id)
                .LastOrDefault();

            if (spouseRelationshipSecond != null)
            {
                return spouseRelationshipSecond.PersonId1;
            }

            return -1;
        }

        public IEnumerable<int> GetChildrenIdByPersonId(int id)
        {
            var parentRelationships = this.context.Relationships.Where(r => r.PersonId1 == id && (r.RelationshipType == "father-child" || r.RelationshipType == "mother-child")).ToList();
            var childrenIds = parentRelationships.Select(r => r.PersonId2).ToList();
            return childrenIds;
        }

    }
}
