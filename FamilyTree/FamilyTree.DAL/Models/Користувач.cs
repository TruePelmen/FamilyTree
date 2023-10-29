using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class Користувач
    {
        public string Логін { get; set; } = null!;

        public string Пароль { get; set; } = null!;

        public virtual ICollection<КористувачДерево> КористувачДеревоs { get; set; } = new List<КористувачДерево>();
    }
}