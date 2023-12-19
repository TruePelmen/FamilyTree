namespace FamilyTree.DAL.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Relationship
    {
        public int Id { get; set; }

        public int PersonId1 { get; set; }

        public int PersonId2 { get; set; }

        public string RelationshipType { get; set; } = null!;

        public virtual Person PersonId1Navigation { get; set; } = null!;

        public virtual Person PersonId2Navigation { get; set; } = null!;
    }
}
