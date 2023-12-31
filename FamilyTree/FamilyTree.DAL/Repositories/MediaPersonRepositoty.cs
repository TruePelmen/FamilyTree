﻿namespace FamilyTree.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class MediaPersonRepositoty : GenericRepository<MediaPerson>, IMediaPersonRepository
    {
        public Media GetMainPhotoByPersonId(int personId)
        {
            return this.context.MediaPeople
                .Where(mp => mp.PersonId == personId)
                .Select(mp => mp.Media).Where(m => m.MainPhoto == true && m.MediaType == "photo")
                .FirstOrDefault();
        }

        public IEnumerable<Media> GetAllMediaByPersonId(int personId)
        {
            return this.context.MediaPeople
                .Where(mp => mp.PersonId == personId)
                .Select(mp => mp.Media).Where(m => m.MediaType == "photo")
                .ToList();
        }

        public IEnumerable<int> GetAllPersonsIdByMediaId(int mediaId)
        {
            return this.context.MediaPeople
                .Where(mp => mp.MediaId == mediaId)
                .Select(mp => mp.PersonId)
                .ToList();
        }
    }
}
