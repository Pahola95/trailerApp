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
    public class movement_trailerController : Controller
    {
        private trailers_dbEntities3 db = new trailers_dbEntities3();

        // GET: movement_trailer
        public ActionResult Index(string placa)
        {

            if (!String.IsNullOrEmpty(placa))
            {
                var trailers = db.movement_trailer.Include(m => m.trailer).AsQueryable().OrderByDescending(x => x.datetime).Where(s => s.trailer.plate.Contains(placa));
                return View(trailers.ToList());
            }

            var movement_trailer = db.movement_trailer.Include(m => m.trailer).AsQueryable().OrderByDescending(x => x.datetime);
            return View(movement_trailer.ToList());
        }

        // GET: movement_trailer/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movement_trailer movement_trailer = db.movement_trailer.Find(id);
            if (movement_trailer == null)
            {
                return HttpNotFound();
            }
            return View(movement_trailer);
        }

        // GET: movement_trailer/Create
        public ActionResult Create()
        {
            ViewBag.trailer_id = new SelectList(db.trailer, "id", "plate");
            return View();
        }

        // POST: movement_trailer/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,trailer_id,latitude,longitude")] movement_trailer movement_trailer)
        {
            if (ModelState.IsValid)
            {
                movement_trailer.id = Guid.NewGuid();
                movement_trailer.datetime = DateTime.Now;
                db.movement_trailer.Add(movement_trailer);
                db.SaveChanges();

                var tracking = db.tracking.Include(u => u.trailer).FirstOrDefault(u => u.trailer_id == movement_trailer.trailer_id);
                if (tracking!=null)
                {
                    tracking.latitude= movement_trailer.latitude;
                    tracking.longitude= movement_trailer.longitude;
                    tracking.datetime = movement_trailer.datetime;
                    db.Entry(tracking).State = EntityState.Modified;
                    db.SaveChanges();
                }else
                {
                    var new_tracking = new tracking();
                    new_tracking.id = Guid.NewGuid();
                    new_tracking.trailer_id = movement_trailer.trailer_id;
                    new_tracking.latitude = movement_trailer.latitude;
                    new_tracking.longitude = movement_trailer.longitude;
                    new_tracking.datetime = movement_trailer.datetime;
                    db.tracking.Add(new_tracking);
                    db.SaveChanges();
                }
                    
                return RedirectToAction("Index");
            }

            ViewBag.trailer_id = new SelectList(db.trailer, "id", "plate", movement_trailer.trailer_id);
            return View(movement_trailer);
        }

        // GET: movement_trailer/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movement_trailer movement_trailer = db.movement_trailer.Find(id);
            if (movement_trailer == null)
            {
                return HttpNotFound();
            }
            ViewBag.trailer_id = new SelectList(db.trailer, "id", "plate", movement_trailer.trailer_id);
            return View(movement_trailer);
        }

        // POST: movement_trailer/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,trailer_id,latitude,longitude")] movement_trailer movement_trailer)
        {
            if (ModelState.IsValid)
            {
                movement_trailer.datetime = DateTime.Now;
                db.Entry(movement_trailer).State = EntityState.Modified;
                db.SaveChanges();

                var tracking = db.tracking.Include(u => u.trailer).FirstOrDefault(u => u.trailer_id == movement_trailer.trailer_id);
                if (tracking != null)
                {
                    tracking.latitude = movement_trailer.latitude;
                    tracking.longitude = movement_trailer.longitude;
                    tracking.datetime = movement_trailer.datetime;
                    db.Entry(tracking).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    var new_tracking = new tracking();
                    new_tracking.id = Guid.NewGuid();
                    new_tracking.trailer_id = movement_trailer.trailer_id;
                    new_tracking.latitude = movement_trailer.latitude;
                    new_tracking.longitude = movement_trailer.longitude;
                    new_tracking.datetime = movement_trailer.datetime;
                    db.tracking.Add(new_tracking);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.trailer_id = new SelectList(db.trailer, "id", "plate", movement_trailer.trailer_id);
            return View(movement_trailer);
        }

        // GET: movement_trailer/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            movement_trailer movement_trailer = db.movement_trailer.Find(id);
            if (movement_trailer == null)
            {
                return HttpNotFound();
            }
            return View(movement_trailer);
        }

        // POST: movement_trailer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            movement_trailer movement_trailer = db.movement_trailer.Find(id);
            db.movement_trailer.Remove(movement_trailer);
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
