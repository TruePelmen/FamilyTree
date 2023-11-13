namespace FamilyTree.BLL
{
    using System;
    using FamilyTree.DAL.Models;

    public class Photo
    {
        public Photo(Media media)
        {
            if (media != null)
            {
                this.Id = media.Id;
                this.Description = media.Description;
                this.FilePath = media.FilePath;
                this.Date = media.Date;
                this.Place = media.Place;
                this.MainPhoto = media.MainPhoto;
                this.MediaType = media.MediaType;
                this.TaggedPerson = media.TaggedPerson;
            }
        }

        public int Id { get; set; }

        public string MediaType { get; set; }

        public string FilePath { get; set; }

        public int? TaggedPerson { get; set; }

        public string Description { get; set; }

        public DateOnly? Date { get; set; }

        public string Place { get; set; }

        public bool? MainPhoto { get; set; }
    }
}
