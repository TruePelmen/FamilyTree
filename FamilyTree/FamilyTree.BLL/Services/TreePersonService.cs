namespace FamilyTree.BLL.Services
{
    using System.Collections.Generic;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class TreePersonService : ITreePersonService
    {
        private ITreePersonRepository treePersonRepository;

        public TreePersonService(ITreePersonRepository treePersonRepository)
        {
            this.treePersonRepository = treePersonRepository;
        }

        public IEnumerable<TreePerson> GetAllTreePeople()
        {
            return this.treePersonRepository.GetAll();
        }

        public IEnumerable<Person> GetTreePeopleByTreeId(int treeId)
        {
            return this.treePersonRepository.GetTreePeopleByTreeId(treeId);
        }

        public TreePerson GetTreePersonById(int id)
        {
            return this.treePersonRepository.GetById(id);
        }

        public void AddTreePerson(int treeId, int personId)
        {
            TreePerson treePerson = new TreePerson
            {
                TreeId = treeId,
                PersonId = personId,
            };

            this.treePersonRepository.Add(treePerson);
            this.treePersonRepository.Save();
        }

        public void UpdateTreePerson(int id, int treeId, int personId)
        {
            TreePerson treePerson = this.treePersonRepository.GetById(id);

            if (treePerson != null)
            {
                treePerson.TreeId = treeId;
                treePerson.PersonId = personId;

                this.treePersonRepository.Update(treePerson);
                this.treePersonRepository.Save();
            }
        }

        public void DeleteTreePerson(int id)
        {
            this.treePersonRepository.Remove(this.GetTreePersonById(id));
            this.treePersonRepository.Save();
        }
    }
}
