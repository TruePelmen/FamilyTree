using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using FamilyTree.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree.BLL.Services
{
    public class TreeService : ITreeService
    {
        private IGenericRepository<Tree> _treeRepository;
        private ITreePersonService _treePersonService;

        public TreeService(IGenericRepository<Tree> treeRepository, ITreePersonService treePersonService)
        {
            _treeRepository = treeRepository;
            _treePersonService = treePersonService;
        }

        public IEnumerable<Tree> GetAllTrees()
        {
            return _treeRepository.GetAll();
        }

        public Tree GetTreeById(int id)
        {
            return _treeRepository.GetById(id);
        }
        public int GetPrimaryPersonId(int treeId)
        {
            var primaryPerson = _treePersonService.GetTreePeopleByTreeId(treeId).FirstOrDefault(person => person.PrimaryPerson);
            if (primaryPerson != null)
            {
                return primaryPerson.Id;
            }
            return -1; 
        }

        public int AddTree(string name)
        {
            Tree tree = new Tree
            {
                Name = name
            };

            _treeRepository.Add(tree);
            _treeRepository.Save();
            return tree.Id;
        }

        public void UpdateTree(int id, string name)
        {
            Tree tree = _treeRepository.GetById(id);

            if (tree != null)
            {
                tree.Name = name;

                _treeRepository.Update(tree);
                _treeRepository.Save();
            }
        }

        public void DeleteTree(int id)
        {
            _treeRepository.Remove(GetTreeById(id));
            _treeRepository.Save();
        }
    }
}
