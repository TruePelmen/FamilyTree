using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class ДеревоОсоба
    {
        public int Id { get; set; }

        public int IdДерева { get; set; }

        public int IdОсоби { get; set; }

        public virtual Дерево IdДереваNavigation { get; set; } = null!;

        public virtual Особа IdОсобиNavigation { get; set; } = null!;
    }
}