namespace FamilyTree.BLL
{
    using FamilyTree.DAL.Models;

    public class TreeInformation
    {
        public TreeInformation()
        {
        }

        public TreeInformation(Tree tree)
        {
            if (tree != null)
            {
                this.Id = tree.Id;
                this.Name = tree.Name;
                this.PrimaryPerson = tree.PrimaryPerson;
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int PrimaryPerson { get; set; }
    }
}
