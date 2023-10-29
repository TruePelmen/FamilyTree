using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class СпеціальнийЗапис
    {
        public int Id { get; set; }

        public string ТипЗапису { get; set; } = null!;

        public int? НомерБудинку { get; set; }

        public string? Священик { get; set; }

        public string Запис { get; set; } = null!;

        public int ПодіяId { get; set; }

        public virtual Подія Подія { get; set; } = null!;
    }
}