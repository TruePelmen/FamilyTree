namespace FamilyTree.DAL.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;
    using Serilog;

    public class UserTreeRepository : GenericRepository<UserTree>, IUserTreeRepository
    {
        public IEnumerable<Tree> GetAllTreeByUserLogin(string login)
        {
            return this.context.UserTrees
        .Where(ut => ut.UserLogin == login)
        .Select(ut => ut.Tree)
        .ToList();
        }

        public IEnumerable<UserTree> GetAllUserTreeByUserLogin(string login)
        {
            return this.context.UserTrees
        .Where(ut => ut.UserLogin == login)
        .Select(ut => ut)
        .ToList();
        }

        public IEnumerable<UserTree> GetAllUserTreeByTreeId(int treeId)
        {
            return this.context.UserTrees
        .Where(ut => ut.TreeId == treeId)
        .Select(ut => ut)
        .ToList();
        }

        public string GetAccessTypeByTreeIdAndUserLogin(int treeId, string login)
        {
            return this.context.UserTrees.Where(ut => ut.TreeId == treeId && ut.UserLogin == login).Select(ut => ut.AccessType).FirstOrDefault();
        }
    }
}
