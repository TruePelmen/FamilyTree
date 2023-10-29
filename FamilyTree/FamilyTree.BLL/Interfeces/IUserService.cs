using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree.BLL.Interfeces
{
    public interface IUserService
    {
        IEnumerable<Користувач> GetAllUsers();
        Користувач GetUserByLogin(string login);
        void AddUser(string login, string password);
        void UpdateUser(string login, string password);
        void DeleteUser(string login);
        bool AuthenticateUser(string login, string password);
    }
}
