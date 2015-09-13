using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MekanKesfi.Models;
using System.Web.Security;
using System.Globalization;
using System.IO;

namespace MekanKesfi.Controllers
{
    public class FirmaController : Controller
    {
        private MekanKesfiModel db = new MekanKesfiModel();
        public int mekaninIdsi;
        // GET: Firma
        public ActionResult Index()
        {
            return View(db.FirmaLogin.ToList());
        }

        // GET: Firma/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FirmaLogin firmaLogin = db.FirmaLogin.Find(id);
            if (firmaLogin == null)
            {
                return HttpNotFound();
            }
            return View(firmaLogin);
        }

        // GET: Firma/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Firma/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kullanici_adi,parola,mek_id")] FirmaLogin firmaLogin)
        {

            
            if (ModelState.IsValid)
            {
                db.FirmaLogin.Add(firmaLogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(firmaLogin);
        }

        // GET: Firma/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FirmaLogin firmaLogin = db.FirmaLogin.Find(id);
            if (firmaLogin == null)
            {
                return HttpNotFound();
            }
            return View(firmaLogin);
        }

        // POST: Firma/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kullanici_adi,parola,mek_id")] FirmaLogin firmaLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(firmaLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(firmaLogin);
        }

        // GET: Firma/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FirmaLogin firmaLogin = db.FirmaLogin.Find(id);
            if (firmaLogin == null)
            {
                return HttpNotFound();
            }
            return View(firmaLogin);
        }

        // POST: Firma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FirmaLogin firmaLogin = db.FirmaLogin.Find(id);
            db.FirmaLogin.Remove(firmaLogin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult FirmaLoginSayfasi()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirmaLoginSayfasi(FirmaLogin fl)
        {
           
                string hashresult = FormsAuthentication.HashPasswordForStoringInConfigFile(fl.parola, "SHA1");
                if (ModelState.IsValid)
                {
                    try
                    {
                    using (MekanKesfiModel mk = new MekanKesfiModel())
                    {
                        var v = mk.FirmaLogin.Where(a => a.kullanici_adi.Equals(fl.kullanici_adi) && a.parola.Equals(hashresult)).FirstOrDefault();
                        FirmaLogin firma = db.FirmaLogin.Find(fl.kullanici_adi);
                        Mekanlar mekan = db.Mekanlar.Find(firma.mek_id);
                        Session["anasayfa_gecis"] = mekan.id.ToString();
                        Session["latitude"] = mekan.latitude.ToString();
                        Session["longitude"] = mekan.longitude.ToString();
                        if (v != null)
                        {

                            return RedirectToAction("FirmaSahibiAnasayfa");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Hatalı parola");
                        }
                    }
                    }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Hatalı kullanıcı adı");
            }
                }




                return View(fl);
            
        }



        public ActionResult FirmaSahibiKayit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult FirmaSahibiKayit([Bind(Include = "kullanici_adi,parola,parolaTekrar")] FirmaKayit fk)
        {
            
            if (fk.parola.Equals(fk.parolaTekrar))
            {
        
                if (ModelState.IsValid)
                {

                    try
                    {
                        FirmaLogin fl = new FirmaLogin();
                        fl.kullanici_adi = fk.kullanici_adi;
                        fl.parola = FormsAuthentication.HashPasswordForStoringInConfigFile(fk.parola, "SHA1");
                        Session["kullanici_adi"] = fk.kullanici_adi;
                        db.FirmaLogin.Add(fl);
                        db.SaveChanges();
                   
                    

                    mekaninIdsi = fl.mek_id;
                    return RedirectToAction("MekanEkle");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Bu kullanıcı adı zaten var");
                        
                    }
                }
                return View(fk);
            }
            else
            {
                ModelState.AddModelError("", "Parola tekrarı hatalı");
                //return RedirectToAction("FirmaSahibiKayit");
            }
            return View(fk);
        }

        public ActionResult MekanEkle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult MekanEkle(MekanResim mekanResim)
        {
            
            FirmaLogin kullanicininMekIdBulma = db.FirmaLogin.Find(Session["kullanici_adi"]);
            if (kullanicininMekIdBulma == null)
            {
                return HttpNotFound();
            }
            mekanResim.id = kullanicininMekIdBulma.mek_id;

            Mekanlar mekan = new Mekanlar();

            mekan.email = mekanResim.email;
            mekan.id = mekanResim.id;
            mekan.latitude = mekanResim.latitude;
            mekan.longitude = mekanResim.longitude;
            mekan.mekan_adi = mekanResim.mekan_adi;
            mekan.mekan_adresi = mekanResim.mekan_adresi;
            mekan.telefon = mekanResim.telefon;

            if (mekanResim.photo != null && mekanResim.photo.ContentLength > 0 && (mekanResim.photo.ContentType == "image/jpeg" || mekanResim.photo.ContentType == "image/bmp" || mekanResim.photo.ContentType == "image/png"))
            {
                mekan.fotograf = new byte[mekanResim.photo.ContentLength];
                mekanResim.photo.InputStream.Read(mekan.fotograf, 0, mekanResim.photo.ContentLength);
            }
                
            

            //string lat = mekan.latitude.ToString();
            //lat = lat.Replace('.', ',');
            //string lon = mekan.longitude.ToString();
            //lon = lon.Replace('.', ',');


            //decimal lat2 = Convert.ToDecimal(lat,);

            //mekan.latitude = Convert.ToDecimal(lat);
            //mekan.longitude = Convert.ToDecimal(lon);



            //mekan.latitude = decimal.Parse(lat);
            //mekan.longitude = decimal.Parse(lon);
            //mekan.longitude = Convert.ToDecimal(mekan.latitude);

            //mekan.latitude = Convert.ToDecimal(mekan.latitude.ToString());
            //mekan.longitude = Convert.ToDecimal(mekan.longitude.ToString());


            Session["reklam_mekan_id"] = mekan.id.ToString();



            //if (mekan.File.ContentLength > (2 * 1024 * 1024))
            //{
            //    ModelState.AddModelError("CustomError", "Boyut 2mb tan küçük olmalı");
            //    return View();
            //}

            //if (mekan.File.ContentType != "image/jpeg" || mekan.File.ContentType != "image/gif")
            //{
            //    ModelState.AddModelError("CustomError", "Dosya tipi farklı");
            //    return View();

            //}


            //byte[] data = new byte[2 * 1024 * 1024];
            //mekan.File.InputStream.Read(data, 0, Convert.ToInt32(2 * 1024 * 1024));

            //mekan.fotograf = data;

            //Mekanlar mekan2 = new Mekanlar();
            //mekan2.id = mekan.id;
            //mekan2.mekan_adi = mekan.mekan_adi;
            //mekan2.mekan_adresi = mekan.mekan_adresi;
            //mekan2.telefon = mekan.telefon;
            //mekan2.email = mekan.email;
            //mekan2.fotograf = mekan.fotograf;
            //mekan2.latitude = mekan.latitude;
            //mekan2.longitude = mekan.longitude;



            if (ModelState.IsValid)
            {
                db.Mekanlar.Add(mekan);
                db.SaveChanges();
                return RedirectToAction("ReklamVer");
            }

            return View(mekan);
        }


        //---------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
        public ActionResult ReklamVer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ReklamVer([Bind(Include = "reklam_id,mekan_id,verilis_zamani,aciklama")] Reklam reklam)
        {

            reklam.mekan_id = Convert.ToInt32(Session["reklam_mekan_id"]);
            reklam.verilis_zamani = DateTime.Now;

            Session["anasayfa_gecis"] = reklam.mekan_id.ToString();
            
            Mekanlar mekan = db.Mekanlar.Find(reklam.mekan_id);
            

            Session["latitude"] = mekan.latitude.ToString();
            Session["longitude"] = mekan.longitude.ToString();

            if (ModelState.IsValid)
            {
                db.Reklam.Add(reklam);
                db.SaveChanges();
                return RedirectToAction("FirmaSahibiAnasayfa");
            }

            return View(reklam);

        }


        public ActionResult FirmaSahibiAnasayfa()
        {
            int anasayfa_gecis = Convert.ToInt32(Session["anasayfa_gecis"]);
            ViewData["latitude"] = Session["latitude"];
            ViewData["longitude"] = Session["longitude"];
            using (var db = new MekanKesfiModel())
            {
                return View(db.Reklam.FirstOrDefault(item => item.mekan_id == anasayfa_gecis ));
                //return View(db.Reklam.FirstOrDefault(item => item.mekan_id == Convert.ToInt32(id)));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult FirmaSahibiAnasayfa([Bind(Include = "reklam_id,mekan_id,verilis_zamani,aciklama")] Reklam reklam)
        {
           


            if (ModelState.IsValid)
            {
                Session["anasayfa_gecis"] = reklam.mekan_id.ToString();
                db.Entry(reklam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("FirmaSahibiAnasayfa");
            }
            return View(reklam);


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
