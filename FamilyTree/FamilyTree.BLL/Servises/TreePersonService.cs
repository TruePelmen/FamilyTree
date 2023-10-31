using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class TreePersonService : ITreePersonService
    {
        private IGenericRepository<TreePerson> _treePersonRepository;

        public TreePersonService(IGenericRepository<TreePerson> treePersonRepository)
        {
            _treePersonRepository = treePersonRepository;
        }

        public IEnumerable<TreePerson> GetAllTreePeople()
        {
            return _treePersonRepository.GetAll();
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
            _treePersonRepository.Remove(id);
            _treePersonRepository.Save();
        }
    }
}
