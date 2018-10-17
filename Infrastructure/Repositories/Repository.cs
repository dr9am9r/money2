using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Money2.Domain;

namespace Money2.Infrastructure
{
    public abstract class Repository<T> : IDisposable where T : class, IEntity
    {
        private Money2Context _context;

        protected EntityEntry<T> Entry( T entity )
        {
            return _context.Entry( entity );
        }

        protected IQueryable<T> Query
        {
            get { return _context.Set<T>().AsNoTracking(); }
        }

        protected IQueryable<E> CommonQuery<E>() where E : class
        {
            return _context.Set<E>().AsNoTracking();
        }

        protected DbSet<T> Set
        {
            get { return _context.Set<T>(); }
        }

        protected DbSet<E> CommonSet<E>() where E : class
        {
            return _context.Set<E>();
        }

        public Repository( Money2Context context )
        {
            _context = context;
        }

        public void Delete( T t )
        {
            _context.Set<T>().Remove( t );
        }

        public T Get( Int32 id )
        {
            return _context.Set<T>().FirstOrDefault( e => e.Id == id );
        }

        public void Save( T t )
        {
            _context.Set<T>().Update( t );

            SaveChanges();
        }

        protected void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
