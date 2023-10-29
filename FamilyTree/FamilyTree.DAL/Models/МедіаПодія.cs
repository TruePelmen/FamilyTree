using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class МедіаПодія
    {
        public int Id { get; set; }

        public int? IdПодії { get; set; }

        public int IdМедіа { get; set; }

        public virtual Медіа IdМедіаNavigation { get; set; } = null!;

        public virtual Подія? IdПодіїNavigation { get; set; }
    }
}