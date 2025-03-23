using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrailerAPPv3.Models;

namespace TrailerAPPv3.Controllers
{
    public class trailersController : Controller
    {
        private trailers_dbEntities3 db = new trailers_dbEntities3();

        // GET: trailers
        public ActionResult Index(string placa,string status)
        {
            
            if (!String.IsNullOrEmpty(placa)&& String.IsNullOrEmpty(status))
            {
                var trailers = db.trailer.Where(s => s.plate.Contains(placa)); 
                return View(trailers.ToList());
            }
            if (!String.IsNullOrEmpty(status) && String.IsNullOrEmpty(placa))
            {
                var trailers = db.trailer.Where(s => s.status.Equals(status));
                return View(trailers.ToList());
            }
            if (!String.IsNullOrEmpty(status) && !String.IsNullOrEmpty(placa))
            {
                var trailers = db.trailer.Where(s => s.plate.Contains(placa)&&s.status.Equals(status));
                return View(trailers.ToList());
            }
            return View(db.trailer.ToList());
        }

        // GET: trailers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trailer trailer = db.trailer.Find(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // GET: trailers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: trailers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,plate,modelo,tipo,adquisition_datetime,status,linea,color")] trailer trailer)
        {
            if (ModelState.IsValid)
            {
                trailer.id = Guid.NewGuid();
                db.trailer.Add(trailer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trailer);
        }

        // GET: trailers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trailer trailer = db.trailer.Find(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // POST: trailers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,plate,modelo,tipo,adquisition_datetime,status,linea,color")] trailer trailer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trailer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trailer);
        }

        // GET: trailers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trailer trailer = db.trailer.Find(id);
            if (trailer == null)
            {
                return HttpNotFound();
            }
            return View(trailer);
        }

        // POST: trailers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            trailer trailer = db.trailer.Find(id);
            db.trailer.Remove(trailer);
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
    }
}
