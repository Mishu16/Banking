using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankingWebsite.Models;
using BankingWebsite.Utilities;

namespace BankingWebsite.Controllers
{
    public class AccountHoldersController : Controller
    {
        private masterEntities db = new masterEntities();

        // GET: AccountHolders
        public ActionResult Index()
        {
            return View(db.AccountHolders.ToList());
        }

        //GET: AccountHolder/AccountHome

        public ActionResult AccountHome(int? id)
        {
            return View(db.Cards.Where(a => a.accountholder.ToString() == id.ToString()).ToList());
        }

        ////POST: AccountHolder/AccountHome
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AccountHome(Session[)
        //{
        //    return View();
        //}

        // GET: AccountHolders/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountHolders/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "accountHolderID, firstname, lastname, password, passwordsalt, passwordhash, pin, pinsalt, pinhash, DOB, customerRef, email, mobile, firstLineaddr, cityOrTown, postcode")] AccountHolder accountHolder)
        {
            accountHolder.firstname = "Empty";
            accountHolder.lastname = "Empty";
            accountHolder.DOB = new DateTime().Date;
            accountHolder.email = "empty@empty.com";
            accountHolder.confirmPassword = accountHolder.password;
            accountHolder.mobile = "07000000000";
            accountHolder.firstLineaddr = "empty";
            accountHolder.cityOrTown = "city";
            accountHolder.postcode = "n22 3qn";


            AccountHolder ah = db.AccountHolders.SingleOrDefault(a => (a.DOB.ToString().Replace("-", "") + a.accountHolderID.ToString()) == accountHolder.customerRef.ToString());
            if (ah == null || accountHolder.customerRef == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                string attemptedpasswordHash = HashSalt.GenerateHash(accountHolder.password, ah.passwordsalt);
                string attemptedpinHash = HashSalt.GenerateHash(accountHolder.pin, ah.pinsalt);

                if (attemptedpasswordHash == ah.passwordhash && attemptedpinHash == ah.pinhash)
                {
                    Session["ahID"] = ah.accountHolderID;
                    Session["DOB"] = ah.DOB;
                    Session["fname"] = ah.firstname;
                    Session["lname"] = ah.lastname;

                    return RedirectToAction("AccountHome/" + Session["ahID"]);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
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
            //Generate random pin and show pin so they can send pin to user
            ViewData["Pin"] = PinGen.RandomPinGen().ToString();
            TempData["Pin"] = ViewData["pin"];
            return View();
        }

        // POST: AccountHolders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "accountHolderID,firstname,lastname,password,confirmPassword,passwordsalt,passwordhash,pin,pinsalt,pinhash,DOB,customerRef,email,mobile,firstLineaddr,cityOrTown,postcode")] AccountHolder accountHolder)
        {
            if (ModelState.IsValid)
            {
                accountHolder.customerRef = "Empty123";

                //Save hashed password to db
                accountHolder.passwordsalt = HashSalt.GenerateSalt(4);
                accountHolder.passwordhash = HashSalt.GenerateHash(accountHolder.password, accountHolder.passwordsalt);

                //Save pin displayed on form to DB
                accountHolder.pin = TempData["Pin"].ToString();
                accountHolder.pinsalt = HashSalt.GenerateSalt(4);
                accountHolder.pinhash = HashSalt.GenerateHash(accountHolder.pin, accountHolder.pinsalt);

                db.AccountHolders.Add(accountHolder);
                db.SaveChanges();
                return RedirectToAction("Login");

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
            ViewData["EditPin"] = PinGen.RandomPinGen().ToString();
            TempData["EditPin"] = ViewData["EditPin"];
            return View(accountHolder);
        }

        // POST: AccountHolders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "accountHolderID,firstname,lastname,password,confirmPassword,passwordsalt,passwordhash,pin,pinsalt,pinhash,DOB,customerRef,email,mobile,firstLineaddr,cityOrTown,postcode")] AccountHolder accountHolder)
        {
            if (ModelState.IsValid)
            {
                accountHolder.customerRef = accountHolder.DOB.ToString() + accountHolder.accountHolderID.ToString();

                //Save hashed password to db
                accountHolder.passwordsalt = HashSalt.GenerateSalt(4);
                accountHolder.passwordhash = HashSalt.GenerateHash(accountHolder.password, accountHolder.passwordsalt);

                //Saves hashed pin to db plus need to show to admin so they can send pin to user
                accountHolder.pin = TempData["EditPin"].ToString();
                accountHolder.pinsalt = HashSalt.GenerateSalt(4);
                accountHolder.pinhash = HashSalt.GenerateHash(accountHolder.pin, accountHolder.pinsalt);

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
