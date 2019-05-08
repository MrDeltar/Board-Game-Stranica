using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Board_Game_Stranica.Models;

namespace Board_Game_Stranica.Controllers
{
    public class PopisDogadajaController : Controller
    {
        // model podataka
        private DogadajiModel baza = new DogadajiModel();

        // popis dogadaja
        public ActionResult PopisDogadaja(string naziv)
        {
            List<Dogadaj> pretrazi = baza.VratiDogadaje();
            // filtriranje po nazivu
            if (!String.IsNullOrEmpty(naziv))
            {
                pretrazi = pretrazi.Where(
                    x => (x.Ime).ToUpper().Contains
                            (naziv.ToUpper())
                    ).ToList();
            }
            ViewBag.Title = "Popis dogadaja";
            return View(pretrazi);
        }

        // detaljni podaci o studenatu
        public ActionResult Detaljno(int? id)
        {
            ViewBag.Title = "Informacije o dogadaju";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // lambda izraz koji traži prvog dogadaja s Id-om koji je zadan
            Dogadaj d = baza.VratiDogadaje().Find(st => st.Id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            return View(d);
        }

        // brisanje - get metoda
        // detaljni podaci o studenatu
        public ActionResult Brisi(int? id)
        {
            ViewBag.Title = "Brisanje dogadaja";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // lambda izraz koji traži prvog studenta s Id-om koji je zadan
            Dogadaj d = baza.VratiDogadaje().Find(st => st.Id == id);
            if (d == null)
            {
                return HttpNotFound();
            }
            return View(d);
        }

        // post metoda brisi
        [HttpPost, ActionName("Brisi")]
        [ValidateAntiForgeryToken]
        public ActionResult BrisiPotvrda(int id)
        {
            // brisanje
            baza.BrisanjeDogadaja(id);
            return RedirectToAction("PopisDogadaja");
        }

        // ažuriranje informacija dogadaja
        // get metoda ažuriraj
        [HttpGet]
        public ActionResult Azuriraj(int? id)
        {
            Dogadaj d;
            if (id == null)
            {
                d = new Dogadaj();
                ViewBag.Title = "Unos novog dogadaja";
            }
            else
            {
                // lambda izraz koji traži prvog studenta s Id-om koji je zadan
                d = baza.VratiDogadaje().Find(st => st.Id == id);
                if (d == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    return HttpNotFound();
                }
                ViewBag.Title = "Ažuriranje informacija dogadaja";
            }
            return View(d);
        }

        // post metoda azuriraj
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(
            [Bind(Include ="Id, Ime, DatumOdrzavanja")]
            Dogadaj d)
        {
            // ovdje možemo napraviti eksplicitnu validaciju 
            // provjera datuma rođenja
            if (d.DatumOdrzavanja < DateTime.Now)
                ModelState.AddModelError("DatumOdrzavanja",
                    "Datum odrzavanja igre ne moze biti u proslosti!");

            // provjera ispravnosti
            if (ModelState.IsValid)
            {
                if (d.Id == 0)
                {
                    // dodavanje u bazu
                    baza.DodajDogadaj(d);
                }
                else
                {
                    // sžuriranje studenta u bazi
                    baza.AzurirajDogadaja(d);
                }
                return RedirectToAction("PopisDogadaja", "Home");
            }
            else
            {
                ViewBag.Title = "Ponovno ažuriranje informacija dogadaja";
                return View(d);
            }
        }
    }
}