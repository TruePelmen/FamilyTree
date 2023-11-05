namespace FamilyTree.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class UserTreeRepository : GenericRepository<UserTree>, IUserTreeRepository
    {
        public IEnumerable<Tree> GetAllTreeByUserLogin(string login)
        {
            return _context.UserTrees
        .Where(ut => ut.UserLogin == login)
        .Select(ut => ut.Tree)
        .ToList();
        }

        public string GetAccessTypeByTreeIdAndUserLogin(int treeId, string login)
        {
            return _context.UserTrees.Where(ut => ut.TreeId == treeId && ut.UserLogin == login).Select(ut => ut.AccessType).FirstOrDefault();
        }
    }
}
