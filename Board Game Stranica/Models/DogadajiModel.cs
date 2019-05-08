using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board_Game_Stranica.Models
{
    public class DogadajiModel
    {
        private static List<Dogadaj> dogadaji =
            new List<Dogadaj>();

        // zastavica za prvi put, da se pokrene punjenje
        private static bool puno = false;

        // vrati
        public List<Dogadaj> VratiDogadaje()
        {
            return dogadaji;
        }

        // ažuriranje
        public void AzurirajDogadaja(Dogadaj d)
        {
            int i = dogadaji.FindIndex(dogadaj => dogadaj.Id == d.Id);
            dogadaji[i] = d;
        }

        // dodavanje dogadaja
        public void DodajDogadaj(Dogadaj d)
        {
            int noviId;
            if (dogadaji.Count == 0)
                noviId = 1;
            else
                noviId = dogadaji.Max(x => x.Id) + 1;
            d.Id = noviId;
            dogadaji.Add(d);
        }

        // brisanje dogadaja
        public void BrisanjeDogadaja(int id)
        {
            int indeks = dogadaji.FindIndex(x => x.Id == id);
            dogadaji.RemoveAt(indeks);
        }

        // konstruktor
        public DogadajiModel()
        {
            if (!puno)
            {

                puno = true;

                // Dogadaj 1
                dogadaji.Add(new Dogadaj
                {
                    Id = 1,
                    Ime = "Star Wars Armada",
                    DatumOdrzavanja = new DateTime(2019, 7, 5),
                });

                // Dogadaj 2
                dogadaji.Add(new Dogadaj
                {
                    Id = 2,
                    Ime = "XCOM",
                    DatumOdrzavanja = new DateTime(2019, 6, 23),
                });
                // klasično
                Dogadaj d = new Dogadaj();
                d.Id = 3;
                d.Ime = "Firefly";
                d.DatumOdrzavanja = new DateTime(2019, 6, 21);
                // dodati u listu
                dogadaji.Add(d);
            }
        }
    }
}