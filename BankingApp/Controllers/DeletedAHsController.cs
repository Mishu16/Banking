using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankingApp.Models;

namespace BankingApp.Controllers
{
    public class DeletedAHsController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: DeletedAHs
        public ActionResult Index()
        {
            return View(db.DeletedAHs.ToList());
        }

        // GET: DeletedAHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeletedAH deletedAH = db.DeletedAHs.Find(id);
            if (deletedAH == null)
            {
                return HttpNotFound();
            }
            return View(deletedAH);
        }

        // GET: DeletedAHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeletedAHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "accountHolderID,firstname,lastname,passwordsalt,passwordhash,pinsalt,pinhash,DOB,email,mobile,firstLineaddr,cityOrTown,postcode")] DeletedAH deletedAH)
        {
            if (ModelState.IsValid)
            {
                db.DeletedAHs.Add(deletedAH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deletedAH);
        }

        // GET: DeletedAHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeletedAH deletedAH = db.DeletedAHs.Find(id);
            if (deletedAH == null)
            {
                return HttpNotFound();
            }
            return View(deletedAH);
        }

        // POST: DeletedAHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "accountHolderID,firstname,lastname,passwordsalt,passwordhash,pinsalt,pinhash,DOB,email,mobile,firstLineaddr,cityOrTown,postcode")] DeletedAH deletedAH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deletedAH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deletedAH);
        }

        // GET: DeletedAHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeletedAH deletedAH = db.DeletedAHs.Find(id);
            if (deletedAH == null)
            {
                return HttpNotFound();
            }
            return View(deletedAH);
        }

        // POST: DeletedAHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeletedAH deletedAH = db.DeletedAHs.Find(id);
            db.DeletedAHs.Remove(deletedAH);
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
