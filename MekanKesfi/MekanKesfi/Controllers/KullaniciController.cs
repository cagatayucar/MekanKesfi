using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MekanKesfi.Models;
using PagedList;

namespace MekanKesfi.Controllers
{
    public class KullaniciController : Controller
    {
        private MekanKesfiModel db = new MekanKesfiModel();

        // GET: Kullanici
        public ActionResult Index()
        {
            return View(db.Reklam.ToList());
        }

        // GET: Kullanici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reklam reklam = db.Reklam.Find(id);
            if (reklam == null)
            {
                return HttpNotFound();
            }
            return View(reklam);
        }

        // GET: Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kullanici/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reklam_id,mekan_id,verilis_zamani,aciklama")] Reklam reklam)
        {
            if (ModelState.IsValid)
            {
                db.Reklam.Add(reklam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reklam);
        }

        // GET: Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reklam reklam = db.Reklam.Find(id);
            if (reklam == null)
            {
                return HttpNotFound();
            }
            return View(reklam);
        }

        // POST: Kullanici/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reklam_id,mekan_id,verilis_zamani,aciklama")] Reklam reklam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reklam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reklam);
        }

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reklam reklam = db.Reklam.Find(id);
            if (reklam == null)
            {
                return HttpNotFound();
            }
            return View(reklam);
        }

        // POST: Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reklam reklam = db.Reklam.Find(id);
            db.Reklam.Remove(reklam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //--------------------------------------------------------------------------------------------------------------------
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //--------------------------------------------------------------------------------------------------------------------

        public ActionResult KullaniciAnasayfa()
        {

            MekanReklamModel mekanReklam = new MekanReklamModel();
            mekanReklam.mekanlar = db.Mekanlar.ToList();
            mekanReklam.reklam = db.Reklam.ToList();

            return View(mekanReklam);
        }


        public ActionResult YoluGoster(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mekanlar mekan = db.Mekanlar.Find(id);
            if (mekan == null)
            {
                return HttpNotFound();
            }

            ViewData["lat"] = mekan.latitude.ToString();
            ViewData["lon"] = mekan.longitude.ToString();


            return View(mekan);
            
          
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
