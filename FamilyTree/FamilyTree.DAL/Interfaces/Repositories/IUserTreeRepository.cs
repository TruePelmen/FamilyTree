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

        IEnumerable<UserTree> GetAllUserTreeByUserLogin(string login);

        IEnumerable<UserTree> GetAllUserTreeByTreeId(int treeId);

        string GetAccessTypeByTreeIdAndUserLogin(int treeId, string login);
    }
}
