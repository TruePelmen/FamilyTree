using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models;

public partial class SpecialRecord
{
    public int Id { get; set; }

    public string RecordType { get; set; } = null!;

    public int? HouseNumber { get; set; }

    public string? Priest { get; set; }

    public string Record { get; set; } = null!;

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;
}
