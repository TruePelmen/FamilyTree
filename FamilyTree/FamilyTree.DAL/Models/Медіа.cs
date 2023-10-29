using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class Медіа
    {
        public int Id { get; set; }

        public string ТипМедіа { get; set; } = null!;

        public string ШляхДоФайлу { get; set; } = null!;

        public int? ПозначеніОсоби { get; set; }

        public string? Опис { get; set; }

        public DateOnly? Дата { get; set; }

        public string? Місце { get; set; }

        public bool? ГоловнеФото { get; set; }

        public virtual ICollection<МедіаОсоба> МедіаОсобаs { get; set; } = new List<МедіаОсоба>();

        public virtual ICollection<МедіаПодія> МедіаПодіяs { get; set; } = new List<МедіаПодія>();
    }
}