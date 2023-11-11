namespace FamilyTree.DAL.Models
{
#nullable enable

    using System;
    using System.Collections.Generic;

    public partial class Event
    {
        public int Id { get; set; }

        public string EventType { get; set; } = null!;

        public DateOnly? EventDate { get; set; }

        public string? EventPlace { get; set; }

        public int PersonId { get; set; }

        public string? Description { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<MediaEvent> MediaEvents { get; set; } = new List<MediaEvent>();

        public virtual Person Person { get; set; } = null!;

        public virtual ICollection<SpecialRecord> SpecialRecords { get; set; } = new List<SpecialRecord>();
    }
}