using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Services
{
    public class UserTreeService : IUserTreeService
    {
        private IGenericRepository<UserTree> _userTreeRepository;

        public UserTreeService(IGenericRepository<UserTree> userTreeRepository)
        {
            _userTreeRepository = userTreeRepository;
        }

        public IEnumerable<UserTree> GetAllUserTrees()
        {
            return _userTreeRepository.GetAll();
        }

        public UserTree GetUserTreeById(int id)
        {
            return _userTreeRepository.GetById(id);
        }

        public void AddUserTree(string userLogin, int treeId, string accessType)
        {
            UserTree userTree = new UserTree
            {
                UserLogin = userLogin,
                TreeId = treeId,
                AccessType = accessType
            };

            _userTreeRepository.Add(userTree);
            _userTreeRepository.Save();
        }

        public void UpdateUserTree(int id, string userLogin, int treeId, string accessType)
        {
            UserTree userTree = _userTreeRepository.GetById(id);

            if (userTree != null)
            {
                userTree.UserLogin = userLogin;
                userTree.TreeId = treeId;
                userTree.AccessType = accessType;

                _userTreeRepository.Update(userTree);
                _userTreeRepository.Save();
            }
        }

        public void DeleteUserTree(int id)
        {
            _userTreeRepository.Remove(id);
            _userTreeRepository.Save();
        }
    }
}
