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
    public class CITAController : Controller
    {
        private PETIPUASEntities1 db = new PETIPUASEntities1();

        // GET: CITA
        public ActionResult indexCita(decimal id)
        {
            var cITA = db.CITA.Include(c => c.MASCOTA).Include(c => c.TIPO_CITA);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CITA cita = db.CITA.Where(x => x.ID_mascota.Equals(id)).FirstOrDefault();
            if (cita == null)
            {
                return RedirectToAction("Create");
            }



            return View(cITA.Where(x => x.ID_mascota.Equals(id)).ToList());
        }

        // GET: CITA/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITA cITA = db.CITA.Find(id);
            if (cITA == null)
            {
                return HttpNotFound();
            }
            return View(cITA);
        }

        // GET: CITA/Create
        public ActionResult Create()
        {
            ViewBag.ID_mascota = new SelectList(db.MASCOTA, "ID_mascota", "Nombre_mascota");
            ViewBag.ID_tipo_cita = new SelectList(db.TIPO_CITA, "ID_tipo_cita", "Tipo_cita1");
            return View();
        }

        // POST: CITA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_cita,ID_mascota,Fecha_cita,Observaciones,ID_tipo_cita")] CITA cITA)
        {
            if (ModelState.IsValid)
            {
              
                db.CITA.Add(cITA);
                db.SaveChanges();

                using (PETIPUASEntities1 db = new PETIPUASEntities1())
                {
                    var obj = db.CITA.Where(x => x.ID_cita.Equals(cITA.ID_cita)).FirstOrDefault();
                    if (obj != null)
                    {

                        Session["ID_CITA"] = obj.ID_cita;
                        Session["ID_MASCOTA"] = obj.MASCOTA.ID_mascota.ToString();
                        return RedirectToAction("indexCita", new { id = Session["ID_MASCOTA"] });

                    }

                }
               
            }

            ViewBag.ID_mascota = new SelectList(db.MASCOTA, "ID_mascota", "Nombre_mascota", cITA.ID_mascota);
            ViewBag.ID_tipo_cita = new SelectList(db.TIPO_CITA, "ID_tipo_cita", "Tipo_cita1", cITA.ID_tipo_cita);
            return View(cITA);
        }

        // GET: CITA/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITA cITA = db.CITA.Find(id);
            if (cITA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_mascota = new SelectList(db.MASCOTA, "ID_mascota", "Nombre_mascota", cITA.ID_mascota);
            ViewBag.ID_tipo_cita = new SelectList(db.TIPO_CITA, "ID_tipo_cita", "Tipo_cita1", cITA.ID_tipo_cita);
            return View(cITA);
        }

        // POST: CITA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_cita,ID_mascota,Fecha_cita,Observaciones,ID_tipo_cita")] CITA cITA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cITA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("indexCita");
            }
            ViewBag.ID_mascota = new SelectList(db.MASCOTA, "ID_mascota", "Nombre_mascota", cITA.ID_mascota);
            ViewBag.ID_tipo_cita = new SelectList(db.TIPO_CITA, "ID_tipo_cita", "Tipo_cita1", cITA.ID_tipo_cita);
            return View(cITA);
        }

        // GET: CITA/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITA cITA = db.CITA.Find(id);
            if (cITA == null)
            {
                return HttpNotFound();
            }
            return View(cITA);
        }

        // POST: CITA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            CITA cITA = db.CITA.Find(id);
            db.CITA.Remove(cITA);
            db.SaveChanges();
            return RedirectToAction("indexMascota","MASCOTA", new { id = Session["ID_usuario"] });
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
