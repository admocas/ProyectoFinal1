using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyectofinal1.Models;

namespace Proyectofinal1.Controllers
{
    public class MASCOTAController : Controller
    {
        private PETIPUASEntities1 db = new PETIPUASEntities1();

        // GET: MASCOTA
        public ActionResult indexMascota(decimal id)
        {
            var mASCOTA = db.MASCOTA.Include(m => m.ANIMAL).Include(m => m.USUARIO);

            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MASCOTA masc = db.MASCOTA.Where(x => x.ID_usuario.Equals(id)).FirstOrDefault();
            if (masc == null)
            {
                
                return RedirectToAction("Create");
            }


            return View(mASCOTA.Where(x => x.ID_usuario.Equals(id)).ToList());
        }

        // GET: MASCOTA/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MASCOTA mASCOTA = db.MASCOTA.Find(id);
            if (mASCOTA == null)
            {
                return HttpNotFound();
            }
            return View(mASCOTA);
        }

        // GET: MASCOTA/Create
        public ActionResult Create()
        {
            ViewBag.ID_animal = new SelectList(db.ANIMAL, "ID_ANIMAL", "Animal1");
            ViewBag.ID_usuario = new SelectList(db.USUARIO, "ID_usuario", "Nombre");
            return View();
        }

        // POST: MASCOTA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_mascota,Nombre_mascota,Edad,ID_animal,ID_usuario")] MASCOTA mASCOTA)
        {
            if (ModelState.IsValid)
            {
                db.MASCOTA.Add(mASCOTA);
                db.SaveChanges();
                return RedirectToAction("indexMascota", new { id = Session["ID_usuario"] });
            }

            ViewBag.ID_animal = new SelectList(db.ANIMAL, "ID_ANIMAL", "Animal1", mASCOTA.ID_animal);
            ViewBag.ID_usuario = new SelectList(db.USUARIO, "ID_usuario", "Nombre", mASCOTA.ID_usuario);
            return View(mASCOTA);
        }

        // GET: MASCOTA/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MASCOTA mASCOTA = db.MASCOTA.Find(id);
            if (mASCOTA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_animal = new SelectList(db.ANIMAL, "ID_ANIMAL", "Animal1", mASCOTA.ID_animal);
            ViewBag.ID_usuario = new SelectList(db.USUARIO, "ID_usuario", "Nombre", mASCOTA.ID_usuario);
            return View(mASCOTA);
        }

        // POST: MASCOTA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_mascota,Nombre_mascota,Edad,ID_animal,ID_usuario")] MASCOTA mASCOTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mASCOTA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("indexMascota", new { id = Session["ID_usuario"] });
            }
            ViewBag.ID_animal = new SelectList(db.ANIMAL, "ID_ANIMAL", "Animal1", mASCOTA.ID_animal);
            ViewBag.ID_usuario = new SelectList(db.USUARIO, "ID_usuario", "Nombre", mASCOTA.ID_usuario);
            return View(mASCOTA);
        }

        // GET: MASCOTA/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MASCOTA mASCOTA = db.MASCOTA.Find(id);
            if (mASCOTA == null)
            {
                return HttpNotFound();
            }
            return View(mASCOTA);
        }

        // POST: MASCOTA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            MASCOTA mASCOTA = db.MASCOTA.Find(id);
            db.MASCOTA.Remove(mASCOTA);
            db.SaveChanges();
            return RedirectToAction("indexMascota", new { id = Session["ID_usuario"] });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
