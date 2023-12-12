namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FamilyTree.BLL.Interfaces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;

    public class UserTreeService : IUserTreeService
    {
        private IUserTreeRepository userTreeRepository;

        public UserTreeService(IUserTreeRepository userTreeRepository)
        {
            this.userTreeRepository = userTreeRepository;
        }

        public IEnumerable<UserTree> GetAllUserTrees()
        {
            return this.userTreeRepository.GetAll();
        }

        public UserTree GetUserTreeById(int id)
        {
            return this.userTreeRepository.GetById(id);
        }

        public IEnumerable<UserTree> GetAllUserTreeByTreeId(int treeId)
        {
            return this.userTreeRepository.GetAllUserTreeByTreeId(treeId);
        }

        public IEnumerable<Tree> GetAllTreeByUserLogin(string userLogin)
        {
            return this.userTreeRepository.GetAllTreeByUserLogin(userLogin);
        }

        public IEnumerable<UserTree> GetAllUserTreeByUserLogin(string userLogin)
        {
            return this.userTreeRepository.GetAllUserTreeByUserLogin(userLogin);
        }

        public IEnumerable<string> GetFreeUsersLoginByTreeId(int treeId)
        {
            return this.userTreeRepository.GetFreeUsersLoginByTreeId(treeId);
        }

        public string GetAccessTypeByUserLoginAndTreeId(string userLogin, int treeId)
        {
            return this.userTreeRepository.GetAccessTypeByTreeIdAndUserLogin(treeId, userLogin);
        }

        public void AddUserTree(string userLogin, int treeId, string accessType)
        {
            UserTree userTree = new UserTree
            {
                UserLogin = userLogin,
                TreeId = treeId,
                AccessType = accessType,
            };
            try
            {
                this.userTreeRepository.Add(userTree);
                this.userTreeRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося додати дерево для користувача");
            }
        }

        public void UpdateUserTree(int id, string userLogin, int treeId, string accessType)
        {
            UserTree userTree = this.userTreeRepository.GetById(id);

            if (userTree != null)
            {
                userTree.UserLogin = userLogin;
                userTree.TreeId = treeId;
                userTree.AccessType = accessType;

                this.userTreeRepository.Update(userTree);
                this.userTreeRepository.Save();
            }
        }

        public void DeleteUserTree(int id)
        {
            try
            {
                this.userTreeRepository.Remove(this.GetUserTreeById(id));
                this.userTreeRepository.Save();
            }
            catch (Exception)
            {
                throw new Exception("Не вдалося видалити дерево для користувача");
            }
        }

        public void DeleteUserTree(string login)
        {
            foreach (UserTree x in this.GetAllUserTreeByUserLogin(login))
            {
                int treeId = x.TreeId;
                this.userTreeRepository.Remove(x);
                if (!this.GetAllUserTreeByTreeId(treeId).Any())
                {
                    // TODO: treeservice call with removing userless tree
                }
            }

            this.userTreeRepository.Save();
        }
    }
}
