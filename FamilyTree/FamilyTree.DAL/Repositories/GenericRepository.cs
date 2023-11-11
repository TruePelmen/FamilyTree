namespace FamilyTree.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FamilyTree.DAL.Context;
    using FamilyTree.DAL.Interfaces.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected FamilyTreeContext context = null;

        public GenericRepository()
        {
            this.context = new FamilyTreeContext();
        }

        public GenericRepository(FamilyTreeContext context)
        {
            this.context = context;
        }

        public T GetById(object id)
        {
            return this.context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.context.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.context.Set<T>().Where(predicate);
        }

        public void Add(T entity)
        {
            this.context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
