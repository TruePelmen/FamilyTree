namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public IEnumerable<PersonInformation> GetTreePeopleByTreeId(int treeId)
        {
            return this.treePersonRepository.GetTreePeopleByTreeId(treeId).Select(person =>
                new PersonInformation(person)).ToList();
        }

        public int GetPersonsNumber(int treeId)
        {
            return this.treePersonRepository.GetPersonsNumber(treeId);
        }

        public int GetEventsNumber(int treeId)
        {
            return this.treePersonRepository.GetEventsNumber(treeId);
        }

        public int GetPhotosNumber(int treeId)
        {
            return this.treePersonRepository.GetPhotosNumber(treeId);
        }

        public TreePerson GetTreePersonById(int id)
        {
            return this.treePersonRepository.GetById(id);
        }

        public TreePerson GetTreePerson(int treeId, int personId)
        {
            return this.treePersonRepository.GetTreePerson(treeId, personId);
        }

        public void AddTreePerson(int treeId, int personId)
        {
            TreePerson treePerson = new TreePerson
            {
                TreeId = treeId,
                PersonId = personId,
            };

            try
            {
                this.treePersonRepository.Add(treePerson);
                this.treePersonRepository.Save();
            }
            catch (System.Exception)
            {
                throw new Exception("Не вдалося додати особу до дерева");
            }
        }

        public string UpdateTreePerson(int id, int treeId, int personId)
        {
            TreePerson treePerson = this.treePersonRepository.GetById(id);
            string message = string.Empty;
            if (treePerson != null)
            {
                treePerson.TreeId = treeId;
                treePerson.PersonId = personId;

                this.treePersonRepository.Update(treePerson);
                this.treePersonRepository.Save();
                message = "Дерево успішно оновлено";
            }
            else
            {
                message = "Дерево не знайдено";
            }
            return message;
        }

        public void DeleteTreePerson(int treeId, int personId)
        {
            try
            {
                this.treePersonRepository.Remove(this.treePersonRepository.GetTreePerson(treeId, personId));
                this.treePersonRepository.Save();
            }
            catch (System.Exception)
            {
                throw new Exception("Не вдалося видалити особу з дерева");
            }
        }
    }
}
