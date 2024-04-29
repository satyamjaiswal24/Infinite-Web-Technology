using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesDB_CodeFirst.Controllers.Repository
{
    public interface IMovieRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);  //a particular product
        void Insert(T obj);
        void Update(T obj);
        void Delete(Object Id);
        void Save();
    }
 
}