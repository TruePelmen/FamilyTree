using FamilyTree.DAL.Interfaces.Repositories;
using FamilyTree.DAL.Models;
using System;
using System.Collections.Generic;

namespace FamilyTree.BLL.Interfaces
{
    public interface ITreeService
    {
        IEnumerable<Tree> GetAllTrees();
        Tree GetTreeById(int id);
        int AddTree(string name);
        void UpdateTree(int id, string name);
        void DeleteTree(int id);
    }
}
