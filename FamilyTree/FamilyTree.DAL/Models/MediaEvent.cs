using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models;

public partial class MediaEvent
{
    public int Id { get; set; }

    public int? EventId { get; set; }

    public int MediaId { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Media Media { get; set; } = null!;
}
