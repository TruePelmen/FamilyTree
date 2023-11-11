namespace FamilyTree.BLL
{
    using FamilyTree.DAL.Models;

    public class PersonCardInformation
    {
        public Person Person { get; set; }

        public string BirthPlace { get; set; }

        public string DeathPlace { get; set; }

        public string MainPhoto { get; set; }

        public bool IsEmptyPerson
        {
            get { return this.Person == null; }
        }
    }
}
