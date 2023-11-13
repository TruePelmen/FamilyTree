namespace FamilyTree.DAL.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Models;

    public interface IMediaPersonRepository : IGenericRepository<MediaPerson>
    {
        Media GetMainPhotoByPersonId(int personId);

        IEnumerable<Media> GetAllMediaByPersonId(int personId);

        IEnumerable<int> GetAllPersonsIdByMediaId(int mediaId);
    }
}
