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
        private IGenericRepository<Користувач> _userRepository;

        public UserService()
        {
            _userRepository = new GenericRepository<Користувач>();
        }

        // Отримати всіх користувачів
        public IEnumerable<Користувач> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        // Отримати користувача за логіном
        public Користувач GetUserByLogin(string login)
        {
            return _userRepository.GetById(login);
        }

        // Додати нового користувача
        public void AddUser(string login, string password)
        {
            // Генерувати хеш пароля перед збереженням
            password = BCrypt.Net.BCrypt.HashPassword(password);
            Користувач user = new Користувач();
            user.Логін = login;
            user.Пароль = password;
            _userRepository.Add(user);
            _userRepository.Save();
        }

        // Оновити інформацію про користувача
        public void UpdateUser(string login, string password)
        {
            Користувач user = new Користувач();
            user.Логін = login;
            user.Пароль = password;
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
             Користувач user = _userRepository.GetById(login);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Пароль))
            {
                return true;
            }
            return false;
        }
    }
}

