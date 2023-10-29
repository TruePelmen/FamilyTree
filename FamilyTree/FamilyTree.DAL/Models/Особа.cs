using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class Особа
    {
        public int Id { get; set; }

        public bool ГоловнаОсоба { get; set; }

        public string Стать { get; set; } = null!;

        public string Прізвище { get; set; } = null!;

        public string? ДівочеПрізвище { get; set; }

        public string? Імя { get; set; }

        public string? ІншіВаріантиІмені { get; set; }

        public DateOnly? ДатаНародження { get; set; }

        public DateOnly? ДатаСмерті { get; set; }

        public virtual ICollection<ДеревоОсоба> ДеревоОсобаs { get; set; } = new List<ДеревоОсоба>();

        public virtual ICollection<Звязок> ЗвязокIdОсоби1Navigations { get; set; } = new List<Звязок>();

        public virtual ICollection<Звязок> ЗвязокIdОсоби2Navigations { get; set; } = new List<Звязок>();

        public virtual ICollection<МедіаОсоба> МедіаОсобаs { get; set; } = new List<МедіаОсоба>();

        public virtual ICollection<Подія> Подіяs { get; set; } = new List<Подія>();
    }
}