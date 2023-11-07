using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface IUserTreeService
    {
        IEnumerable<UserTree> GetAllUserTrees();

        UserTree GetUserTreeById(int id);

        IEnumerable<Tree> GetAllTreeByUserLogin(string userLogin);

        string GetAccessTypeByUserLoginAndTreeId(string userLogin, int treeId);

        void AddUserTree(string userLogin, int treeId, string accessType);

        void UpdateUserTree(int id, string userLogin, int treeId, string accessType);

        void DeleteUserTree(int id);
    }
}
