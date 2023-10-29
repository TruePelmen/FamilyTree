using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models;

public partial class User
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<UserTree> UserTrees { get; set; } = new List<UserTree>();
}
