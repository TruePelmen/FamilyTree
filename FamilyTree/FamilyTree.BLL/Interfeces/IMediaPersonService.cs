using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface IMediaPersonService
    {
        IEnumerable<MediaPerson> GetAllMediaPeople();
        MediaPerson GetMediaPersonById(int id);
        void AddMediaPerson(int personId, int mediaId);
        void UpdateMediaPerson(int id, int personId, int mediaId);
        void DeleteMediaPerson(int id);
    }
}
