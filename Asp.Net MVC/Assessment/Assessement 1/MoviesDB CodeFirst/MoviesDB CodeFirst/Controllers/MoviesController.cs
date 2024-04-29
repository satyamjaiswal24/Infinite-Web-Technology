using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MoviesDB_CodeFirst.Controllers.Repository;
using MoviesDB_CodeFirst.Models;

namespace MoviesDB_CodeFirst.Controllers
{
    public class MoviesController : Controller
    {
        IMovieRepository<Movie> movieRepository = null;

        public MoviesController()
        {
            movieRepository = new MoviesRepository<Movie>();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = movieRepository.GetAll();
            return View(movies);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieRepository.Insert(movie);
                movieRepository.Save();

                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = movieRepository.GetById(id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieRepository.Update(movie);
                movieRepository.Save();

                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = movieRepository.GetById(id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            movieRepository.Delete(id);
            movieRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
