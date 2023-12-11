namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class TreeService : ITreeService
    {
        private IGenericRepository<Tree> treeRepository;

        public TreeService(IGenericRepository<Tree> treeRepository)
        {
            this.treeRepository = treeRepository;
        }

        public IEnumerable<Tree> GetAllTrees()
        {
            return this.treeRepository.GetAll();
        }

        public Tree GetTreeById(int id)
        {
            return this.treeRepository.GetById(id);
        }

        public int GetPrimaryPersonId(int treeId)
        {
            return this.GetTreeById(treeId).PrimaryPerson;
        }

        public int AddTree(string name, int primaryPersonId)
        {
            Tree tree = new Tree
            {
                Name = name,
                PrimaryPerson = primaryPersonId,
            };
            try
            {
                this.treeRepository.Add(tree);
                this.treeRepository.Save();
                return tree.Id;
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося додати дерево");
            }
        }

        public string UpdateTree(int id, string name)
        {
            Tree tree = this.treeRepository.GetById(id);
            string message = string.Empty;
            if (tree != null)
            {
                tree.Name = name;

                this.treeRepository.Update(tree);
                this.treeRepository.Save();
                message = "Дерево успішно оновлено";
            }
            else
            {
                message = "Не вдалося знайти дерево з таким id";
            }
            return message;
        }

        public void DeleteTree(int id)
        {
            try
            {
                this.treeRepository.Remove(this.GetTreeById(id));
                this.treeRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося видалити дерево з таким id");
            }
        }
    }
}
