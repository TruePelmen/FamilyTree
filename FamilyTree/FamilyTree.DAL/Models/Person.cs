namespace FamilyTree.DAL.Models
{
#nullable enable

    using System;
    using System.Collections.Generic;

    public partial class Person
    {
        public int Id { get; set; }

        public string Gender { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MaidenName { get; set; }

        public string? FirstName { get; set; }

        public string? OtherNameVariants { get; set; }

        public DateOnly? BirthDate { get; set; }

        public DateOnly? DeathDate { get; set; }

        public string? BirthPlace { get; set; }

        public string? DeathPlace { get; set; }

        public virtual ICollection<Event> Events { get; set; } = new List<Event>();

        public virtual ICollection<MediaPerson> MediaPeople { get; set; } = new List<MediaPerson>();

        public virtual ICollection<Relationship> RelationshipPersonId1Navigations { get; set; } = new List<Relationship>();

        public virtual ICollection<Relationship> RelationshipPersonId2Navigations { get; set; } = new List<Relationship>();

        public virtual ICollection<TreePerson> TreePeople { get; set; } = new List<TreePerson>();
    }
}
