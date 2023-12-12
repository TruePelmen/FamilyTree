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
        private ITreePersonService treePersonService;
        private IPersonService personService;

        public TreeService(IGenericRepository<Tree> treeRepository, ITreePersonService treePersonService, IPersonService personService)
        {
            this.treeRepository = treeRepository;
            this.treePersonService = treePersonService;
            this.personService = personService;
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
                IEnumerable<PersonInformation> treePeople = this.treePersonService.GetTreePeopleByTreeId(id);
                IEnumerable<TreePerson> treePersonLinks = this.treePersonService.GetAllTreePeople();

                // Перевіряємо, чи особа має інші зв'язки з деревами, окрім поточного
                foreach (PersonInformation person in treePeople)
                {
                    if (treePersonLinks.Select(t => (t.PersonId == person.Id) && (t.TreeId != id)).Count() == 0)
                    {
                        this.personService.DeletePerson(person.Id);
                    }
                    else
                    {
                        this.treePersonService.DeleteTreePerson(id, person.Id);
                    }
                }

                Tree treeToDelete = this.GetTreeById(id);
                if (treeToDelete != null)
                {
                    this.treeRepository.Remove(treeToDelete);
                    this.treeRepository.Save();
                }
                else
                {
                    throw new Exception("Дерево з вказаним id не знайдено");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Не вдалося видалити дерево з id {id}. Помилка: {ex.Message}", ex);
            }
        }

    }
}
