using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Board_Game_Stranica.Ispis_PDF;
using Board_Game_Stranica.Models;

namespace Board_Game_Stranica.Controllers
{
    public class DogadajController : Controller
    {
        // model podataka
        private DogadajiDbContext baza = new DogadajiDbContext();

        //popis dogadaja
        [HttpGet]
        public ActionResult PopisDogadaja(string nazivI)
        {
            List<Dogadaj> pretrazi = baza.Dogadaji.ToList();
            // filtriranje po nazivu
            if (!String.IsNullOrEmpty(nazivI))
            {
                pretrazi = pretrazi.Where(x => (x.Naziv).ToUpper().Contains(nazivI.ToUpper())).ToList();
            }
            ViewBag.Title = "Popis Drustvenih Igara";
            return View(pretrazi);
        }

        // napravi dogadaj - get
        public ActionResult NapraviDogadaj()
        {
            return View();
        }

        // napravi dogadaj - post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,Mjesto,DatumOdrzavanja,Organizator")] Dogadaj dogadaj)
        {
            if (ModelState.IsValid)
            {
                baza.Dogadaji.Add(dogadaj);
                baza.SaveChanges();
                return RedirectToAction("PopisDogadaja");
            }

            return View(dogadaj);
        }

        // detaljni podaci dogadaja
        public ActionResult Detaljno(int? id)
        {
            ViewBag.Title = "Podaci o drustevnoj igri";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // lambda izraz
            Dogadaj d = baza.Dogadaji.Find(id);
            if (d == null)
            {
                return HttpNotFound();
            }
                  return View(d);        
        }


        // ispis
        public FileStreamResult Ispis(int? id)
        {
            // lambda izraz
            Dogadaj d = baza.Dogadaji.Find(id);

            IspisDogadaja i = new IspisDogadaja(d);
            return new FileStreamResult(new MemoryStream(i.Podaci), "application/pdf");
        }


        // brisanje - get metoda
        // detaljni podaci dogadaja
        public ActionResult BrisiDogadaj(int? id)
        {
            ViewBag.Title = "Brisanje drustvene igre";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // lambda izraz
            Dogadaj d = baza.Dogadaji.Find(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            return View(d);

         
        }

        // brisi - post metoda
        [HttpPost, ActionName("BrisiDogadaj")]
        [ValidateAntiForgeryToken]
        public ActionResult BrisiPotvrda(int id)
        {
            Dogadaj d = baza.Dogadaji.Find(id);
            baza.Dogadaji.Remove(d);
            baza.SaveChanges();
            return RedirectToAction("PopisDogadaja");
        }

        // azuriranje informacija dogadaja
        // azuriraj - get metoda
        [HttpGet]
        public ActionResult AzurirajDogadaj(int? id)
        {
            Dogadaj d;
            if (id == null)
            {
                d = new Dogadaj();
                ViewBag.Title = "Novo organiziranje";
            }
            else
            {
                // lambda izraz
                d = baza.Dogadaji.Find(id);
                if (d == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Title = "Ažuriranje drustvene igre";
            }
            return View(d);
        }

        // azuriraj - post metoda
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AzurirajDogadaj(
            [Bind(Include ="Id, Naziv, Datum, Mjesto, Organizator")] Dogadaj d)
        {
            //validacija
            // provjera datuma odrzavanja
            //if (d.DatumOdrzavanja < DateTime.Now)
            //   ModelState.AddModelError("DatumOdrzavanja", "Datum odrzavanja ne može biti u prošlosti");
            // provjera ispravnosti modela
            if (ModelState.IsValid)
            {
                if (d.Id == 0)
                {
                    // dodavanje u bazu
                    
                }
                else
                {
                    // azuriranje u bazi
                    baza.Entry(d).State = EntityState.Modified;
                }
                baza.SaveChanges();
                return RedirectToAction("PopisDogadaja", "Dogadaj");
            }
            else
            {
                ViewBag.Title = "Ponovno ažuriranje drustvene igre";
                return View(d);
            }
        }
    }
}