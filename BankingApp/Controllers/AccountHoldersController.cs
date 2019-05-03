using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankingApp.Models;
using BankingApp;
using BankingApp.Utilities;

namespace BankingApp.Controllers
{
    public class AccountHoldersController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: AccountHolders
        public ActionResult Index()
        {
            return View(db.AccountHolders.ToList());
        }

        // GET: AccountHolders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountHolder accountHolder = db.AccountHolders.Find(id);
            if (accountHolder == null)
            {
                return HttpNotFound();
            }
            return View(accountHolder);
        }

        // GET: AccountHolders/Create
        public ActionResult Create()
        {
            TempData["Pin"] = PinGen.RandomPinGen().ToString();
            return View();
        }

        // POST: AccountHolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "accountHolderID,firstname,lastname,password,passwordsalt,passwordhash,pinsalt,pinhash,DOB,email,mobile,firstLineaddr,cityOrTown,postcode")] AccountHolder accountHolder)
        {
            if (ModelState.IsValid)
            {
                //Save hashed password to db
                accountHolder.passwordsalt = HashSalt.GenerateSalt(4);
                accountHolder.passwordhash = HashSalt.GenerateHash(accountHolder.password, accountHolder.passwordsalt);

                //Saves hashed pin to db plus need to show to admin so they can send pin to user
                accountHolder.pin = TempData["Pin"].ToString();
                accountHolder.pinsalt = HashSalt.GenerateSalt(4);
                accountHolder.pinhash = HashSalt.GenerateHash(accountHolder.pin, accountHolder.pinsalt);

                db.AccountHolders.Add(accountHolder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountHolder);
        }

        // GET: AccountHolders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountHolder accountHolder = db.AccountHolders.Find(id);
            if (accountHolder == null)
            {
                return HttpNotFound();
            }
            return View(accountHolder);
        }

        // POST: AccountHolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "accountHolderID,firstname,lastname,passwordsalt,passwordhash,pinsalt,pinhash,DOB,email,mobile,firstLineaddr,cityOrTown,postcode")] AccountHolder accountHolder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountHolder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountHolder);
        }

        // GET: AccountHolders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountHolder accountHolder = db.AccountHolders.Find(id);
            if (accountHolder == null)
            {
                return HttpNotFound();
            }
            return View(accountHolder);
        }

        // POST: AccountHolders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountHolder accountHolder = db.AccountHolders.Find(id);
            db.AccountHolders.Remove(accountHolder);
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
