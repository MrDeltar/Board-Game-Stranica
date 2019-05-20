using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
        [HttpPost, ActionName("Brisi")]
        [ValidateAntiForgeryToken]
        public ActionResult BrisiPotvrda(int id)
        {
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
            [Bind(Include ="Id, Naziv, Datum, Mjesto, Organizator")]
            Dogadaj d)
        {
            //validacija
            // provjera datuma odrzavanja
            if (d.DatumOdrzavanja < DateTime.Now)
                ModelState.AddModelError("DatumOdrzavanja",
                    "Datum odrzavanja ne može biti u prošlosti");
            // provjera ispravnosti modela
            if (ModelState.IsValid)
            {
                if (d.Id == 0)
                {
                    // dodavanje u bazu
                    baza.Dogadaji.Add(d);
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