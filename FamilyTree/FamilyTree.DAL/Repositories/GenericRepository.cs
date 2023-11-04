using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FamilyTree.DAL.Context;
using FamilyTree.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected FamilyTreeContext _context = null;

        public GenericRepository()
        {
            _context = new FamilyTreeContext();
        }

        public GenericRepository(FamilyTreeContext _context)
        {
            this._context = _context;
        }

        public T GetById(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
