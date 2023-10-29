using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models;

public partial class TreePerson
{
    public int Id { get; set; }

    public int TreeId { get; set; }

    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Tree Tree { get; set; } = null!;
}
