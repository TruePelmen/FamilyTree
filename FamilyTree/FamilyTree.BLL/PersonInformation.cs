namespace FamilyTree.BLL
{
    using System;
    using FamilyTree.DAL.Models;

    public class PersonInformation
    {
        public PersonInformation()
        {
        }

        public PersonInformation(Person person)
        {
            if (person != null)
            {
                this.LastName = person.LastName;
                this.FirstName = person.FirstName;
                this.BirthDate = person.BirthDate;
                this.DeathDate = person.DeathDate;
                this.BirthPlace = person.BirthPlace;
                this.DeathPlace = person.DeathPlace;
                this.Id = person.Id;
                this.MaidenName = person.MaidenName;
                this.OtherNameVariants = person.OtherNameVariants;
                this.Gender = person.Gender;
            }
        }

        public int Id { get; set; }

        public string Gender { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MaidenName { get; set; } = null;

        public string OtherNameVariants { get; set; } = null;

        public DateOnly? BirthDate { get; set; }

        public DateOnly? DeathDate { get; set; }

        public string BirthPlace { get; set; } = null;

        public string DeathPlace { get; set; } = null;

        public string MainPhoto { get; set; } = null;

        public bool IsEmptyPerson
        {
            get { return this.Id < 0 || this.LastName == null;  }
        }

        public string FullName
        {
            get
            {
                string fullName = string.Empty;

                if (this.LastName != null)
                {
                    fullName += this.LastName;
                }

                if (this.FirstName != null)
                {
                    fullName += " " + this.FirstName;
                }

                return fullName;
            }
        }

        public int AgeAtDeath
        {
            get
            {
                if (this.DeathDate != null)
                {
                    return this.DeathDate.Value.Year - this.BirthDate.Value.Year;
                }

                return 0;
            }
        }

        public int? CurrentAge(int? date)
        {
            if (this.BirthDate != null && date != null)
                {
                    return date - this.BirthDate.Value.Year;
                }

            return 0;
        }
    }
}
