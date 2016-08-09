using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreetUP.Models;
using Microsoft.AspNet.Identity;

namespace GreetUP.Controllers
{
    public class RSVPsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RSVPs 
        [AllowAnonymous] 
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));
                ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

                List<RSVP> RSVPs = db.RSVPs.Where(p => p.ApplicationUser.Id == currentUser.Id) .ToList();

                if (RSVPs.Count == 0)
                    RSVPs = db.RSVPs.ToList();

                return View(RSVPs);
                    
            }
            var rsvp = db.Events.Include(o => o.EventName);
            return View(rsvp.ToList());
            //return View(db.RSVPs.ToList());
        }

        // GET: RSVPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            return View(rSVP);
        }

        // GET: RSVPs/Create
        public ActionResult Create()
        {
         
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName");

            return View();
        }

        //public ActionResult Going(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RSVP rsvp = db.RSVPs.Find(id);
        //    if (rsvp == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (rsvp != null)
        //    {
               
        //    }
        //}

        // POST: RSVPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int EventID)
        { 
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));
            ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());

            var rSVP = new RSVP();
            rSVP.Event = db.Events.Find(EventID);
            rSVP.ApplicationUser = currentUser;
            

            if (ModelState.IsValid)
            {
                db.RSVPs.Add(rSVP);
                db.SaveChanges(); 
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", rSVP.Event);
            return View(rSVP);
        }

        // GET: RSVPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events,"EventID", "EventName", rSVP.Event);
           
            return View(rSVP);
        }

        // POST: RSVPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventName")] RSVP rSVP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rSVP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events,"EventID", "EventName", rSVP.Event);
            return View(rSVP);
        }

        // GET: RSVPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RSVP rSVP = db.RSVPs.Find(id);
            if (rSVP == null)
            {
                return HttpNotFound();
            }
            return View(rSVP);
        }

        // POST: RSVPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RSVP rSVP = db.RSVPs.Find(id);
            db.RSVPs.Remove(rSVP);
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
