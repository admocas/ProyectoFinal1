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
    public class USUARIOController : Controller
    {
        private PETIPUASEntities1 db = new PETIPUASEntities1();

        // GET: USUARIO
        public ActionResult Index()
        {
            var uSUARIO = db.USUARIO.Include(u => u.PROVINCIA);
            return View(uSUARIO.ToList());
        }

        // GET: USUARIO/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // GET: USUARIO/Create
        public ActionResult Create()
        {
            ViewBag.ID_provincia = new SelectList(db.PROVINCIA, "ID_provincia", "Provincia1");
            return View();
        }

        // POST: USUARIO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_usuario,Nombre,Primer_apellido,Segundo_apellido,Email,Nombre_usuario,Contrasena,Edad,Sexo,ID_provincia,Direccion")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                db.USUARIO.Add(uSUARIO);
                db.SaveChanges();
                using (PETIPUASEntities1 db = new PETIPUASEntities1())
                {
                    var obj = db.USUARIO.Where(x => x.Nombre_usuario.Equals(uSUARIO.Nombre_usuario)).FirstOrDefault();
                     if (obj != null)
                    {

                        Session["ID_usuario"] = obj.ID_usuario.ToString();
                        Session["Nombre_usuario"] = obj.Nombre_usuario.ToString();
                        return RedirectToAction("Tablero");
                    }
                }
                   
            }

            ViewBag.ID_provincia = new SelectList(db.PROVINCIA, "ID_provincia", "Provincia1", uSUARIO.ID_provincia);
            return View(uSUARIO);
        }

        // GET: USUARIO/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_provincia = new SelectList(db.PROVINCIA, "ID_provincia", "Provincia1", uSUARIO.ID_provincia);
            return View(uSUARIO);
        }

        // POST: USUARIO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_usuario,Nombre,Primer_apellido,Segundo_apellido,Email,Nombre_usuario,Contrasena,Edad,Sexo,ID_provincia,Direccion")] USUARIO uSUARIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_provincia = new SelectList(db.PROVINCIA, "ID_provincia", "Provincia1", uSUARIO.ID_provincia);
            return View(uSUARIO);
        }

        // GET: USUARIO/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // POST: USUARIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            db.USUARIO.Remove(uSUARIO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USUARIO usuario)
        {
            if (ModelState.IsValid)
            {
                using (PETIPUASEntities1 db = new PETIPUASEntities1())
                {
                    var obj = db.USUARIO.Where(x => x.Nombre_usuario.Equals(usuario.Nombre_usuario) &&
                    x.Contrasena.Equals(usuario.Contrasena)).FirstOrDefault();

                    if (obj != null)
                    {

                        Session["ID_usuario"] = obj.ID_usuario.ToString();
                        Session["Nombre_usuario"] = obj.Nombre_usuario.ToString();
                        return RedirectToAction("Tablero");
                    }
                }
            }
            return View(usuario);

        }

        public ActionResult Tablero()
        {
            if (Session["ID_usuario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session["ID_usuario"] = null;
            Session["Nombre_usuario"] = null;
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}


