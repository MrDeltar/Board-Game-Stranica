using Board_Game_Stranica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Board_Game_Stranica.ViewModels;
namespace Board_Game_Stranica.Controllers
{
    public class KorisnikController : Controller
    {
        // model podataka
        private KorisniciDbContext baza = new KorisniciDbContext();
        private int id_kor;
        private string ime;
        private string prezime;
        private char spol;
        private string mail;
        private DateTime datum;
        private string email;
        private string pass;
        private bool haspass;
        private string conpass;

        //ModelView poziv
        public ActionResult GetKorisnikViewModel()
        {
            KorisnikViewModel korisnikViewModel = new KorisnikViewModel();
            korisnikViewModel.korisnikInfo = GetKorModel(id_kor, ime, prezime, spol, mail, datum);
            korisnikViewModel.RegViewMod = GetRegViewModel(email, pass, conpass);
            korisnikViewModel.IndViewMod = GetIndViewModel(haspass);
            return View(korisnikViewModel);
        }

        public Korisnik GetKorModel(int id_kor, string ime, string prezime, char spol, string mail, DateTime datum)
        {
            Korisnik kormod = new Korisnik();
            id_kor = kormod.Id_korisnik;
            ime = kormod.Ime;
            prezime = kormod.Prezime;
            spol = kormod.Spol;
            mail = kormod.mail;
            datum = kormod.DatumRodenja;
            return kormod;
        }

        public RegisterViewModel GetRegViewModel(string email, string pass, string conpass)
        {
            RegisterViewModel regview = new RegisterViewModel();
            email = regview.Email;
            pass = regview.Password;
            conpass = regview.ConfirmPassword;
            return regview;
        }

        public IndexViewModel GetIndViewModel(bool hasPass)
        {
            IndexViewModel indview = new IndexViewModel();
            hasPass = indview.HasPassword;
            return indview;
        }

        // GET: Korisnik
        public ActionResult Index()
        {
            ViewBag.Title = "Baza korisnika";
            return View();
        }


        // pregled profila
        public ActionResult Index(int? id_korisnik)
        {
            ViewBag.Title = "Profil korisnika";
            if (id_korisnik == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // lambda izraz
            Korisnik k = baza.Korisnici.Find(id_korisnik);
            if (k == null)
            {
                return HttpNotFound();
            }
            return View(k);
        }

        // azuriranje podataka korisnika
        // azuriraj - get metoda
        [HttpGet]
        public ActionResult Profil_Azuriraj(int? id_korisnik)
        {
            Korisnik s;
            if (id_korisnik == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                // lambda izraz
                s = baza.Korisnici.Find(id_korisnik);
                if (s == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Title = "Ažuriranje osobnih informacija";
            }
            return View(s);
        }
        /*
        // post metoda azuriraj
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(
            [Bind(Include ="Id, Spol, Ime, Prezime, Oib, DatumRodjenja, GodinaStudija, RedovniStudent")]
            Korisnik s)
        {
            //validacija
            // provjera datuma rođenja
            if (s.DatumRodenja > DateTime.Now)
                ModelState.AddModelError("DatumRodjenja",
                    "Datum rođenja ne može biti veći od današnjeg datuma");
            if (s.DatumRodenja < new DateTime(1900, 1, 1))
                ModelState.AddModelError("DatumRodjenja",
                    "Datum rođenja ne može biti manji od 1.1.1900");

            // provjeriti dali je model ispravan
            if (ModelState.IsValid)
            {
                if (s.Id == 0)
                {
                    // dodavanje u bazu
                    // baza.DodajStudenta(s);
                    baza.Studenti.Add(s);
                }
                else
                {
                    // ažuriranje studenta u bazi
                    //baza.AzurirajStudenta(s);
                    baza.Entry(s).State = EntityState.Modified;
                }
                baza.SaveChanges();
                return RedirectToAction("Popis", "Student");
            }
            else
            {
                ViewBag.Title = "Ponovno ažuriranje podataka o studentu/ici";
                return View(s);
            }
        }
        */
    }
}