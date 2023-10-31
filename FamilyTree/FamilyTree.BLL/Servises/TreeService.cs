using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class TreeService : ITreeService
    {
        private IGenericRepository<Tree> _treeRepository;

        public TreeService(IGenericRepository<Tree> treeRepository)
        {
            _treeRepository = treeRepository;
        }

        public IEnumerable<Tree> GetAllTrees()
        {
            return _treeRepository.GetAll();
        }

        public Tree GetTreeById(int id)
        {
            return _treeRepository.GetById(id);
        }

        public void AddTree(string name)
        {
            Tree tree = new Tree
            {
                Name = name
            };

            _treeRepository.Add(tree);
            _treeRepository.Save();
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
            _treeRepository.Remove(id);
            _treeRepository.Save();
        }
    }
}
