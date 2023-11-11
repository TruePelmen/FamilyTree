namespace FamilyTree.DAL.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FamilyTree.DAL.Models;

    public interface IUserTreeRepository : IGenericRepository<UserTree>
    {
        IEnumerable<Tree> GetAllTreeByUserLogin(string login);

        string GetAccessTypeByTreeIdAndUserLogin(int treeId, string login);
    }
}
