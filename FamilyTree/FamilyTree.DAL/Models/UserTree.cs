namespace FamilyTree.DAL.Models
{
    using System;
    using System.Collections.Generic;

    public partial class UserTree
    {
        public int Id { get; set; }

        public string UserLogin { get; set; } = null!;

        public int TreeId { get; set; }

        public string AccessType { get; set; } = null!;

        public virtual Tree Tree { get; set; } = null!;

        public virtual User UserLoginNavigation { get; set; } = null!;
    }
}