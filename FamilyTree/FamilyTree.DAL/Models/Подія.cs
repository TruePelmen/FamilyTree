using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class Подія
    {
        public int Id { get; set; }

        public string ТипПодії { get; set; } = null!;

        public DateOnly? ДатаПодії { get; set; }

        public string? МісцеПодії { get; set; }

        public int ОсобаId { get; set; }

        public string? Опис { get; set; }

        public int? Вік { get; set; }

        public virtual ICollection<МедіаПодія> МедіаПодіяs { get; set; } = new List<МедіаПодія>();

        public virtual Особа Особа { get; set; } = null!;

        public virtual ICollection<СпеціальнийЗапис> СпеціальнийЗаписs { get; set; } = new List<СпеціальнийЗапис>();
    }
}