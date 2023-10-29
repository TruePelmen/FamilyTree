using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class Звязок
    {
        public int Id { get; set; }

        public int IdОсоби1 { get; set; }

        public int IdОсоби2 { get; set; }

        public string ТипЗвязку { get; set; } = null!;

        public virtual Особа IdОсоби1Navigation { get; set; } = null!;

        public virtual Особа IdОсоби2Navigation { get; set; } = null!;
    }
}