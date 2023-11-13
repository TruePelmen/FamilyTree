namespace FamilyTree.BLL
{
    using System;
    using FamilyTree.DAL.Models;

#nullable enable
    public class EventInformation
    {
        private string fullEventType;

        public EventInformation()
        {

        }

        public EventInformation(Event eventInfo)
        {
           this.Id = eventInfo.Id;
           this.EventType = eventInfo.EventType;
           this.GetFullEventType();
           this.EventDate = eventInfo.EventDate;
           this.EventPlace = eventInfo.EventPlace;
           this.PersonId = eventInfo.PersonId;
           this.Description = eventInfo.Description;
           this.Age = eventInfo.Age;
        }

        public int Id { get; set; }

        public string EventType{ get; set; }

        public string FullEventType
        {
            get
            {
                return this.fullEventType;
            }

            set
            {
                this.fullEventType = value;
            }
        }

        public DateOnly? EventDate { get; set; }

        public string? EventPlace { get; set; }

        public int PersonId { get; set; }

        public string? Description { get; set; }

        public int? Age { get; set; }

        private void GetFullEventType()
        {
            switch (this.EventType)
            {
                case "birth":
                    this.FullEventType = "Народження";
                    break;
                case "death":
                    this.FullEventType = "Смерть";
                    break;
                case "marriage":
                    this.FullEventType = "Одруження";
                    break;
                case "other event":
                    this.FullEventType = "Інша подія";
                    break;
            }
        }
    }
}
