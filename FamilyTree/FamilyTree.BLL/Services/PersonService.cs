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

        public IEnumerable<PersonInformation> GetAllPeople()
        {
            return this.personRepository.GetAll().Select(person =>
                new PersonInformation(person)).ToList();
        }

        public PersonInformation GetPersonById(int id)
        {
            PersonInformation personInformation = this.GetShortInformationAboutPerson(id);
            var birthEvent = this.eventService.GetAllEventsByPersonIdAndEventType(id, "birth").FirstOrDefault();
            if (birthEvent != null)
            {
                personInformation.BirthPlace = birthEvent.EventPlace;
            }

            var deathEvent = this.eventService.GetAllEventsByPersonIdAndEventType(id, "death").FirstOrDefault();
            if (deathEvent != null)
            {
                personInformation.DeathPlace = deathEvent.EventPlace;
            }

            return personInformation;
        }

        public PersonInformation GetShortInformationAboutPerson(int id)
        {
            var person = this.personRepository.GetById(id);
            PersonInformation personInformation = new PersonInformation(person);
            var mainPhoto = this.mediaPersonService.GetMainPhotoByPersonId(id);
            if (mainPhoto != null)
            {
                personInformation.MainPhoto = mainPhoto.FilePath;
            }

            return personInformation;
        }

        public int AddPerson(PersonInformation personInformation)
        {
            Person person = new Person
            {
                LastName = personInformation.LastName,
                Gender = personInformation.Gender,
                MaidenName = personInformation.MaidenName,
                FirstName = personInformation.FirstName,
                OtherNameVariants = personInformation.OtherNameVariants,
                BirthDate = personInformation.BirthDate,
                DeathDate = personInformation.DeathDate,
            };

            this.personRepository.Add(person);
            int id = person.Id;
            this.personRepository.Save();
            return id;
        }

        public void UpdatePerson(PersonInformation personInformation)
        {
            Person person = this.personRepository.GetById(personInformation.Id);

            if (person != null)
            {
                person.LastName = personInformation.LastName;
                person.Gender = personInformation.Gender;
                person.MaidenName = personInformation.MaidenName;
                person.FirstName = personInformation.FirstName;
                person.OtherNameVariants = personInformation.OtherNameVariants;
                person.BirthDate = personInformation.BirthDate;
                person.DeathDate = personInformation.DeathDate;

                this.personRepository.Update(person);
                this.personRepository.Save();
            }
        }

        public void DeletePerson(int id)
        {
            Person person = this.personRepository.GetById(id);
            if (person != null)
            {
                this.personRepository.Remove(person);
                this.personRepository.Save();
            }
        }
    }
}
