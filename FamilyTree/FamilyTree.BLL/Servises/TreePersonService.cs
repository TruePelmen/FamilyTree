using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using FamilyTree.BLL.Interfaces;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class TreePersonService : ITreePersonService
    {
        private ITreePersonRepository _treePersonRepository;

        public TreePersonService(ITreePersonRepository treePersonRepository)
        {
            _treePersonRepository = treePersonRepository;
        }

        public IEnumerable<TreePerson> GetAllTreePeople()
        {
            return _treePersonRepository.GetAll();
        }
        public IEnumerable<Person> GetTreePeopleByTreeId(int treeId)
        {
            return _treePersonRepository.GetTreePeopleByTreeId(treeId);
        }
        public TreePerson GetTreePersonById(int id)
        {
            return _treePersonRepository.GetById(id);
        }

        public void AddTreePerson(int treeId, int personId)
        {
            TreePerson treePerson = new TreePerson
            {
                TreeId = treeId,
                PersonId = personId
            };

            _treePersonRepository.Add(treePerson);
            _treePersonRepository.Save();
        }

        public void UpdateTreePerson(int id, int treeId, int personId)
        {
            TreePerson treePerson = _treePersonRepository.GetById(id);

            if (treePerson != null)
            {
                treePerson.TreeId = treeId;
                treePerson.PersonId = personId;

                _treePersonRepository.Update(treePerson);
                _treePersonRepository.Save();
            }
        }

        public void DeleteTreePerson(int id)
        {
            _treePersonRepository.Remove(GetTreePersonById(id));
            _treePersonRepository.Save();
        }
    }
}
