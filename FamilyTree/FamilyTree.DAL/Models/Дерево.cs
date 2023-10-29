using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class Дерево
    {
        public int Id { get; set; }

        public string Назва { get; set; } = null!;

        public virtual ICollection<ДеревоОсоба> ДеревоОсобаs { get; set; } = new List<ДеревоОсоба>();

        public virtual ICollection<КористувачДерево> КористувачДеревоs { get; set; } = new List<КористувачДерево>();
    }
}