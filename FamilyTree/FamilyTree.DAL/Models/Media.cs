namespace FamilyTree.DAL.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Media
    {
        public int Id { get; set; }

        public string MediaType { get; set; } = null!;

        public string FilePath { get; set; } = null!;

        public int? TaggedPerson { get; set; }

        public string Description { get; set; }

        public DateOnly? Date { get; set; }

        public string Place { get; set; }

        public bool? MainPhoto { get; set; }

        public virtual ICollection<MediaEvent> MediaEvents { get; set; } = new List<MediaEvent>();

        public virtual ICollection<MediaPerson> MediaPeople { get; set; } = new List<MediaPerson>();
    }
}
