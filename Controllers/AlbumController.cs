using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaMusical.Models;

namespace TiendaMusical.Controllers
{
    public class AlbumController : Controller
    {
        ModelMS db = new ModelMS();

        // GET: Album
        public ActionResult Index()
        {
            var albums = db.Albums.ToList(); 
            return View(albums);
        }

        // GET: Album/Details/5
        public ActionResult Details(int id)
        {
            var album = db.Albums.Where(alb => alb.AlbumId == id).SingleOrDefault();
            return View(album);
        }

        // GET: Album/Create
        public ActionResult Create()
        {
            var album = new Albums();
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            return View(album);
        }

        // POST: Album/Create
        [HttpPost]
        public ActionResult Create(Albums album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {

            if (id == null)// verifica si el id existe o no sea nullo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);// si el id existe entonces nos devuelve el objeto
            }
            Albums albums = db.Albums.Find(id);
            if (albums == null) // si album esigual a nullo entonces nos devuelve un erro
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", albums.ArtistId); // lo almacena temporalemte para editar parte del artista
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", albums.GenreId);// almacena temporalmente para editar parte de los generos
            return View(albums);//retorna el modelo album
        }

        // POST: Album/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Albums albums) // recive los datos a actualizar como parametro.
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid) // si el modelo es valido envia los datos actualizados del objeto  y guarda los cambios
                {
                    db.Entry(albums).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");//redirecciona a la vista index
                }
                ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", albums.ArtistId);
                ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", albums.GenreId);
                return View(albums);
            }
            catch
            {
                return View();
            }
        }

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null) // verifica que el id exista o no sea nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Albums albums = db.Albums.Find(id); // busca si el  elemento existe en el modelo Albums
            if (albums == null )//verifica si el modelo albums exista
            {
                return HttpNotFound();
            }
            return View(albums); //retorna el modelo albums
        }

        // POST: Album/Delete/5
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                  Albums albums = db.Albums.Find(id); //busca el objeto en el modelo albums
            db.Albums.Remove(albums);//remueve el objeto mediante el id en el modelo albums
            db.SaveChanges();// Guarda cambios 
            return RedirectToAction("Index");// refresca la pagina ya con el cambio hecho
            }
            catch
            {
                return View(); //si existiera un error  me envia al aa vista 
            }
        }
    }
}
