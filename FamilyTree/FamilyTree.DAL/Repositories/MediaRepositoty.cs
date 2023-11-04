using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;

namespace FamilyTree.DAL.Repositories
{
    public class MediaRepositoty : GenericRepository<Media>, IMediaRepository
    {
        public Media GetMainPhotoByPersonId(int personId)
        {
            return _context.Media
            .SingleOrDefault(m => m.TaggedPerson == personId && m.MainPhoto == true);
        }
    }
}
