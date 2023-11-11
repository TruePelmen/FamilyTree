namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class PersonService : IPersonService
    {
        private IPersonRepository personRepository;
        private IEventService eventService;
        private IMediaPersonService mediaPersonService;

        public PersonService(IPersonRepository personRepository, IEventService eventService, IMediaPersonService mediaPersonService)
        {
            this.personRepository = personRepository;
            this.eventService = eventService;
            this.mediaPersonService = mediaPersonService;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return this.personRepository.GetAll();
        }

        public Person GetPersonById(int id)
        {
            return this.personRepository.GetById(id);
        }

        public PersonCardInformation GetFullInformationAboutPerson(int id)
        {
            PersonCardInformation person = this.GetShortInformationAboutPerson(id);

            var birthEvent = this.eventService.GetAllEventsByPersonIdAndEventType(id, "Birth").FirstOrDefault();
            if (birthEvent != null)
            {
                person.BirthPlace = birthEvent.EventPlace;
            }

            var deathEvent = this.eventService.GetAllEventsByPersonIdAndEventType(id, "Death").FirstOrDefault();
            if (deathEvent != null)
            {
                person.DeathPlace = deathEvent.EventPlace;
            }

            return person;
        }

        public PersonCardInformation GetShortInformationAboutPerson(int id)
        {
            PersonCardInformation person = new PersonCardInformation();
            person.Person = this.personRepository.GetById(id);

            var mainPhoto = this.mediaPersonService.GetMainPhotoByPersonId(id);

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
                DeathDate = deathDate,
            };

            this.personRepository.Add(person);
            this.personRepository.Save();
            return person.Id;
        }

        public void UpdatePerson(int id, bool primaryPersone, string lastname, string gender, string maidenName, string firstName, string otherNameVariants, DateOnly? birthDate, DateOnly? deathDate)
        {
            Person person = this.personRepository.GetById(id);

            if (person != null)
            {
                person.PrimaryPerson = primaryPersone;
                person.LastName = lastname;
                person.Gender = gender;
                person.MaidenName = maidenName;
                person.FirstName = firstName;
                person.OtherNameVariants = otherNameVariants;
                person.BirthDate = birthDate;
                person.DeathDate = deathDate;

                this.personRepository.Update(person);
                this.personRepository.Save();
            }
        }

        public void DeletePerson(int id)
        {
            this.personRepository.Remove(this.GetPersonById(id));
            this.personRepository.Save();
        }
    }
}
