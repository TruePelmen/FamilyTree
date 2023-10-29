using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models;

public partial class MediaPerson
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int MediaId { get; set; }

    public virtual Media Media { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
