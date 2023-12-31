﻿namespace FamilyTree.DAL.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGenericRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Add(T obj);

        void Update(T obj);

        void Remove(T obj);

        void Save();
    }
}
