using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaMusical.Models;

namespace TiendaMusical.Controllers
{
    public class HomeController : Controller
    {
        ModelMS db = new ModelMS();

        public ActionResult Index()
        {
            var generos = db.Genres.ToList();
            return View(generos);
        }

        public ActionResult BuscarGenero(int id)
        {
            var genero = db.Genres.Include("Albums").Where(gn => gn.GenreId == id).SingleOrDefault();
            return View(genero);
        }

    }
}