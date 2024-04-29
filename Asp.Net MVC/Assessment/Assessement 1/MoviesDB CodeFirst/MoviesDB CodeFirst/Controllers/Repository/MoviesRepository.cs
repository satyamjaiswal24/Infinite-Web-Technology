using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MoviesDB_CodeFirst.Models;

namespace MoviesDB_CodeFirst.Controllers.Repository
{
    
    public class MoviesRepository<T> : IMovieRepository<T> where T : class
    {
        MoviesDbContext db;
        DbSet<T> dbset;

        public MoviesRepository()
        {
            db = new MoviesDbContext();
            dbset = db.Set<T>();
        }
        public void Delete(Object Id)
        {
            T getmodel = dbset.Find(Id);
            dbset.Remove(getmodel);
        }

        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public T GetById(object Id)
        {
            return dbset.Find(Id);
        }

        public void Insert(T obj)
        {
            dbset.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }
    }
}