namespace FamilyTree.DAL.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Models;

    public interface ITreePersonRepository : IGenericRepository<TreePerson>
    {
        IEnumerable<Person> GetTreePeopleByTreeId(int treeId);

        TreePerson GetTreePerson(int treeId, int personId);

        int GetPersonsNumber(int treeId);

        int GetEventsNumber(int treeId);

        int GetPhotosNumber(int treeId);
    }
}
