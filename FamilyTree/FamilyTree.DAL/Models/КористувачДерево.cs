using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class КористувачДерево
    {
        public int Id { get; set; }

        public string ЛогінКористувача { get; set; } = null!;

        public int IdДерева { get; set; }

        public string ТипДоступу { get; set; } = null!;

        public virtual Дерево IdДереваNavigation { get; set; } = null!;

        public virtual Користувач ЛогінКористувачаNavigation { get; set; } = null!;
    }
}