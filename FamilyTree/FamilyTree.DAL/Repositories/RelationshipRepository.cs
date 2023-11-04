using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;

namespace FamilyTree.DAL.Repositories
{
    public class RelationshipRepository: GenericRepository<Relationship>, IRelationshipRepository
    {
        public int GetParentIdByPersonId(int personId, string type)
        {
            var fatherRelationship = _context.Relationships
                .FirstOrDefault(r => r.PersonId2 == personId && r.RelationshipType == type);

            if (fatherRelationship != null)
            {
                return fatherRelationship.PersonId1;
            }
            return -1;
        }
        public int GetSpouseIdByPersonId(int personId)
        {
            var spouseRelationshipFirst = _context.Relationships
                .FirstOrDefault(r => r.PersonId1 == personId && r.RelationshipType == "spouse");

            if (spouseRelationshipFirst != null)
            {
                return spouseRelationshipFirst.PersonId2;
            }
            var spouseRelationshipSecond = _context.Relationships
                .FirstOrDefault(r => r.PersonId2 == personId && r.RelationshipType == "spouse");

            if (spouseRelationshipSecond != null)
            {
                return spouseRelationshipSecond.PersonId1;
            }
            return -1;
        }
        public IEnumerable<int> GetChildrenIdByPersonId(int id)
        {
            var parentRelationships = _context.Relationships.Where(r => r.PersonId1 == id && (r.RelationshipType == "father-child" || r.RelationshipType == "mother-child")).ToList();
            var childrenIds = parentRelationships.Select(r => r.PersonId2).ToList();
            return childrenIds;
        }

    }
}
