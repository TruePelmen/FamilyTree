﻿namespace FamilyTree.BLL.Services
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
                BirthPlace = personInformation.BirthPlace,
                DeathPlace = personInformation.DeathPlace,
            };

            this.personRepository.Add(person);
            this.personRepository.Save();
            return person.Id;
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
                person.BirthPlace = personInformation.BirthPlace;
                person.DeathPlace = personInformation.DeathPlace;

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
