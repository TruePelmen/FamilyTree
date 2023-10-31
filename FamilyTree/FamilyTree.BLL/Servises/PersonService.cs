using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using FamilyTree.BLL.Interfaces;
using System.Collections.Generic;
using System;

namespace FamilyTree.BLL.Services
{
    public class PersonService : IPersonService
    {
        private IGenericRepository<Person> _personRepository;

        public PersonService(IGenericRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _personRepository.GetAll();
        }

        public Person GetPersonById(int id)
        {
            return _personRepository.GetById(id);
        }

        public int AddPerson(bool primaryPersone, string lastname, string gender, string maidenName, string firstName, string otherNameVariants, DateOnly? birthDate, DateOnly? deathDate)
        {
            Person person = new Person
            {
                PrimaryPerson = primaryPersone,
                LastName = lastname,
                Gender = gender,
                MaidenName = maidenName,
                FirstName = firstName,
                OtherNameVariants = otherNameVariants,
                BirthDate = birthDate,
                DeathDate = deathDate
            };

            _personRepository.Add(person);
            _personRepository.Save();
            return person.Id;
        }

        public void UpdatePerson(int id, bool primaryPersone, string lastname, string gender, string maidenName, string firstName, string otherNameVariants, DateOnly? birthDate, DateOnly? deathDate)
        {
            Person person = _personRepository.GetById(id);

            if (person != null)
            {
                person.PrimaryPerson= primaryPersone;
                person.LastName = lastname;
                person.Gender = gender;
                person.MaidenName = maidenName;
                person.FirstName = firstName;
                person.OtherNameVariants = otherNameVariants;
                person.BirthDate = birthDate;
                person.DeathDate = deathDate;

                _personRepository.Update(person);
                _personRepository.Save();
            }
        }

        public void DeletePerson(int id)
        {
            _personRepository.Remove(GetPersonById(id));
            _personRepository.Save();
        }
    }
}
