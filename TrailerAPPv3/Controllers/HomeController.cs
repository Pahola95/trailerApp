using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrailerAPPv3.DTO;
using TrailerAPPv3.Models;
using Rotativa;

namespace TrailerAPPv3.Controllers
{
    public class HomeController : Controller
    {
        private trailers_dbEntities3 db = new trailers_dbEntities3();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult GeolocationMap()
        {
            ViewBag.Message = "Monitoring all trailers";
            var trackings = db.tracking
                .Include(m => m.trailer)
                .AsQueryable()
                .OrderByDescending(x => x.datetime)
                .AsNoTracking()
                .Select(x => new TrackingDTO { id = x.id, 
                    latitude = x.latitude, 
                    longitude = x.longitude, 
                    datetime = x.datetime, 
                    trailer_id = x.trailer.id, 
                    trailer_plate = x.trailer.plate, 
                    trailer_model = x.trailer.modelo,
                    trailer_status = x.trailer.status , 
                    trailer_linea=x.trailer.linea,
                    trailer_color= x.trailer.color });


            return View(trackings.ToList());
        }

        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("pahola.ponsoy@gmail.com"); // Replace with your email
                    mail.To.Add(model.ToEmail);
                    mail.Subject = model.Subject;
                    mail.Body = model.Message;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("pahola.ponsoy@gmail.com", "vyxf fiqy bikb sfes"); // Use environment variables in production
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    ViewBag.Message = "El correo fue enviado de manera satisfactoria!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error: " + ex.Message;
                }
            }

            return View(model);
        }
        public ActionResult TrailerReport()
        {
            var movement_trailer = db.movement_trailer.Include(m => m.trailer).AsQueryable().OrderByDescending(x => x.datetime);
            return View(movement_trailer.ToList());
        }

        public ActionResult GenerateReport()
        {
            var movement_trailer = db.movement_trailer.Include(m => m.trailer).AsQueryable().OrderByDescending(x => x.datetime);

            return new ViewAsPdf("TrailerReport", movement_trailer.ToList())
            {
                FileName = "ReporteTrailer.pdf"
            };
        }

        public ActionResult GraphsTrailers()
        {
            var trackings = db.movement_trailer
                .Include(m => m.trailer)
                .AsQueryable()
                .OrderByDescending(x => x.datetime)
                .AsNoTracking()
                .Select(x => new TrackingDTO {
                    id = x.id, 
                    latitude = x.latitude, 
                    longitude = x.longitude, 
                    datetime = x.datetime, 
                    trailer_id = x.trailer.id,
                    trailer_plate = x.trailer.plate, 
                    trailer_model = x.trailer.modelo, 
                    trailer_status = x.trailer.status
                });
            var trailerCounts = trackings

                .GroupBy(t => t.trailer_plate)
                .Select(g => new
                {
                    label = g.Key,
                    y = g.Count(),
                    latitude = g.FirstOrDefault().latitude,
                    longitude = g.FirstOrDefault().longitude

                })
                .ToList();
            ViewBag.TrailerCounts = trailerCounts;
            return View();
        }


    }
    
    }
