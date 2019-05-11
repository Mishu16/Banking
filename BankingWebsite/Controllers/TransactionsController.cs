using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankingWebsite.Models;

namespace BankingWebsite.Controllers
{
    public class TransactionsController : Controller
    {
        private masterEntities1 db = new masterEntities1();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Card).Include(t => t.Card1);
            return View(transactions.ToList());
        }

        // GET: TransactionHistory
        public ActionResult PreTransactionHistory(int? id)
        {
            return View(db.Cards.Where(a => a.accountholder.ToString() == id.ToString()).ToList());
        }

        // GET: TransactionHistory
        public ActionResult TransactionHistory(int? id)
        {
            ViewBag.cardID = id.ToString();
            return View(db.Transactions.Where(w => w.toAccount == id || w.fromAccount == id).Include(t => t.Card).Include(t => t.Card1).ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            string ahID = Session["ahID"].ToString();

            ViewBag.fromAccount = new SelectList(db.Cards.Where(c => c.accountholder.ToString() == ahID), "cardID", "cardNo");
            ViewBag.toAccount = new SelectList(db.Cards.Where(c => c.accountholder.ToString() == ahID ), "cardID", "cardNo");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transID,transDate,moneyIn,moneyOut,balance,fromAccount,toAccount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.moneyIn = 0;

                transaction.balance = db.Cards.Single(c => c.cardID == transaction.fromAccount).balance - transaction.moneyOut;
                db.Cards.Single(c => c.cardID == transaction.fromAccount).balance = (decimal) transaction.balance;

                transaction.toBalance = db.Cards.Single(c => c.cardID == transaction.toAccount).balance + transaction.moneyOut;
                db.Cards.Single(c => c.cardID == transaction.toAccount).balance = (decimal)transaction.toBalance;

                transaction.transDate = DateTime.Now.Date;
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("AccountHome/" + Session["ahID"], "AccountHolders1");
            }

            ViewBag.fromAccount = new SelectList(db.Cards, "cardID", "cardNo", transaction.fromAccount);
            ViewBag.toAccount = new SelectList(db.Cards, "cardID", "cardNo", transaction.toAccount);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.fromAccount = new SelectList(db.Cards, "cardID", "cardNo", transaction.fromAccount);
            ViewBag.toAccount = new SelectList(db.Cards, "cardID", "cardNo", transaction.toAccount);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transID,transDate,moneyIn,moneyOut,balance,fromAccount,toAccount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fromAccount = new SelectList(db.Cards, "cardID", "cardNo", transaction.fromAccount);
            ViewBag.toAccount = new SelectList(db.Cards, "cardID", "cardNo", transaction.toAccount);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
