using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface ITreePersonService
    {
        IEnumerable<TreePerson> GetAllTreePeople();
        TreePerson GetTreePersonById(int id);
        void AddTreePerson(int treeId, int personId);
        void UpdateTreePerson(int id, int treeId, int personId);
        void DeleteTreePerson(int id);
    }
}
