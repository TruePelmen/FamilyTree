using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAllPeople();
        Person GetPersonById(int id);
        void AddPerson(string name, string lastname, string gender, string maidenName, string firstName, string otherNameVariants, DateOnly? birthDate, DateOnly? deathDate);
        void UpdatePerson(int id, string name, string lastname, string gender, string maidenName, string firstName, string otherNameVariants, DateOnly? birthDate, DateOnly? deathDate);
        void DeletePerson(int id);
    }
}
