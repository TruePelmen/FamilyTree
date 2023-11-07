using FamilyTree.BLL.Interfaces;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.BLL
{
    public class PersonCardInformation
    {
        public Person Person { get; set; }

        public string BirthPlace { get; set; }

        public string DeathPlace { get; set; }

        public string MainPhoto { get; set; }

        public bool IsEmptyPerson
        {
            get { return Person == null; }
        }
    }
}
