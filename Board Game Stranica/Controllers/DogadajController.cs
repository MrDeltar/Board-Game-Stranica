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

        // detaljni podaci o studenatu
        public ActionResult Detaljno(int? id)
        {
            ViewBag.Title = "Podaci o drustevnoj igri";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // lambda izraz koji traži prvog studenta s Id-om koji je zadan
            Dogadaj d = baza.Dogadaji.Find(id);
            if (d == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                return HttpNotFound();
            }
            return View(d);
        }


        // brisanje - get metoda
        // detaljni podaci o studenatu
        public ActionResult BrisiDogadaj(int? id)
        {
            ViewBag.Title = "Brisanje drustvene igre";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // lambda izraz koji traži prvog studenta s Id-om koji je zadan
            Dogadaj d = baza.Dogadaji.Find(id);
            if (d == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
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
            //baza.Dogadaji(id);
            return RedirectToAction("PopisDogadaja");
        }

        // ažuriranje informacija dogadaja
        // get metoda ažuriraj
        [HttpGet]
        public ActionResult AzurirajDogadaj(int? id)
        {
            //ViewBag.Title = "Ažuriranje podataka o studentu/ici";
            Dogadaj d;
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                d = new Dogadaj();
                ViewBag.Title = "Novo organiziranje";
            }
            else
            {
                // lambda izraz koji traži prvog studenta s Id-om koji je zadan
                d = baza.Dogadaji.Find(id);
                if (d == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    return HttpNotFound();
                }
                ViewBag.Title = "Ažuriranje drustvene igre";
            }
            return View(d);
        }

        // post metoda azuriraj
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AzurirajDogadaj(
            [Bind(Include ="Id, Naziv, Datum, Mjesto, Organizator")]
            Dogadaj d)
        {
            //validacija
            // provjera datuma odrzavanja

            //if (d.DatumOdrzavanja < DateTime.Now)
            //    ModelState.AddModelError("DatumOdrzavanja",
            //        "Datum odrzavanja ne moze biti u proslosti");

            // provjera je li model ispravan
            if (ModelState.IsValid)
            {
                if (d.Id == 0)
                {
                    // dodavanje u bazu
                    // baza.DodajStudenta(s);
                    baza.Dogadaji.Add(d);
                }
                else
                {
                    // ažuriranje studenta u bazi
                    //baza.AzurirajStudenta(s);
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