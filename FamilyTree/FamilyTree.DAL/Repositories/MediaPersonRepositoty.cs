using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;

namespace FamilyTree.DAL.Repositories
{
    public class MediaPersonRepositoty : GenericRepository<MediaPerson>, IMediaPersonRepository
    {
        public Media GetMainPhotoByPersonId(int personId)
        {
            return _context.MediaPeople
                .Where(mp => mp.PersonId == personId)
                .Select(mp => mp.Media).Where(m => m.MainPhoto == true && m.MediaType == "photo")
                .FirstOrDefault();
        }
    }
}
