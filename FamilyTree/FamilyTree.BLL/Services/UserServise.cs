namespace FamilyTree.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using BCrypt.Net;
    using FamilyTree.BLL.Interfeces;
    using FamilyTree.DAL.Interfaces.Repositories;
    using FamilyTree.DAL.Models;
    using FamilyTree.DAL.Repositories;

    public class UserService : IUserService
    {
        private IGenericRepository<User> userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.userRepository.GetAll();
        }

        public User GetUserByLogin(string login)
        {
            return this.userRepository.GetById(login);
        }

        public bool FindUserByLogin(string login)
        {
            return this.GetUserByLogin(login) != null;
        }

        public void AddUser(string login, string password)
        {
            password = BCrypt.HashPassword(password);
            User user = new User();
            user.Login = login;
            user.Password = password;
            this.userRepository.Add(user);
            this.userRepository.Save();
        }

        public void UpdateUser(string login, string password)
        {
            User user = new User();
            user.Login = login;
            user.Password = password;
            this.userRepository.Update(user);
            this.userRepository.Save();
        }

        public void DeleteUser(string login)
        {
            this.userRepository.Remove(this.GetUserByLogin(login));
            this.userRepository.Save();
        }

        public bool AuthenticateUser(string login, string password)
        {
            User user = this.userRepository.GetById(login);
            if (user != null && BCrypt.Verify(password, user.Password))
            {
                return true;
            }

            return false;
        }
    }
}