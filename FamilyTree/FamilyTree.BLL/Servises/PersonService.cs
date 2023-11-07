using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using FamilyTree.BLL.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace FamilyTree.BLL.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository;
        private IEventService _eventService;
        private IMediaPersonService _mediaPersonService;

        public PersonService(IPersonRepository personRepository, IEventService eventService, IMediaPersonService mediaPersonService)
        {
            _personRepository = personRepository;
            _eventService = eventService;
            _mediaPersonService = mediaPersonService;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _personRepository.GetAll();
        }
        public Person GetPersonById(int id)
        {
            return _personRepository.GetById(id);
        }
        public PersonCardInformation GetFullInformarionAboutPerson(int id)
        {
            PersonCardInformation person = GetShortInformationAboutPerson(id);

            var birthEvent = _eventService.GetAllEventsByPersonIdAndEventType(id, "Birth").FirstOrDefault();
            if (birthEvent != null)
            {
                person.BirthPlace = birthEvent.EventPlace;
            }

            var deathEvent = _eventService.GetAllEventsByPersonIdAndEventType(id, "Death").FirstOrDefault();
            if (deathEvent != null)
            {
                person.DeathPlace = deathEvent.EventPlace;
            }

            return person;
        }

        public PersonCardInformation GetShortInformationAboutPerson(int id)
        {
            PersonCardInformation person = new PersonCardInformation();
            person.Person = _personRepository.GetById(id);

            var mainPhoto = _mediaPersonService.GetMainPhotoByPersonId(id);

            if (mainPhoto != null)
            {
                person.MainPhoto = mainPhoto.FilePath;
            }

            return person;
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
