using System;
using System.Collections.Generic;

namespace FamilyTree.DAL.Models
{
    public partial class МедіаОсоба
    {
        public int Id { get; set; }

        public int IdОсоби { get; set; }

        public int IdМедіа { get; set; }

        public virtual Медіа IdМедіаNavigation { get; set; } = null!;

        public virtual Особа IdОсобиNavigation { get; set; } = null!;
    }
}