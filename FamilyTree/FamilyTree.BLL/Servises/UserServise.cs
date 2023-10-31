using System;
using System.Collections.Generic;
using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using FamilyTree.DAL.Repositories;
using FamilyTree.BLL.Interfeces;
using BCrypt.Net;

namespace FamilyTree.BLL.Services
{
    public class UserService : IUserService
    {
        private IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // Отримати всіх користувачів
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        // Отримати користувача за логіном
        public User GetUserByLogin(string login)
        {
            return _userRepository.GetById(login);
        }
        public bool FindUserByLogin(string login)
        {
            return GetUserByLogin(login) != null;
        }

        // Додати нового користувача
        public void AddUser(string login, string password)
        {
            // Генерувати хеш пароля перед збереженням
            password = BCrypt.Net.BCrypt.HashPassword(password);
            User user = new User();
            user.Login = login;
            user.Password = password;
            _userRepository.Add(user);
            _userRepository.Save();
        }

        // Оновити інформацію про користувача
        public void UpdateUser(string login, string password)
        {
            User user = new User();
            user.Login = login;
            user.Password = password;
            _userRepository.Update(user);
            _userRepository.Save();
        }

        // Видалити користувача
        public void DeleteUser(string login)
        {
            _userRepository.Remove(GetUserByLogin(login));
            _userRepository.Save();
        }

        public bool AuthenticateUser(string login, string password)
        {
            User user = _userRepository.GetById(login);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return true;
            }
            return false;
        }
    }
}

