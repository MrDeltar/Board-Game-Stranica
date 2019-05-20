using Board_Game_Stranica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Board_Game_Stranica.ViewModels
{
    public class KorisnikViewModel
    {
        public Korisnik korisnikInfo { get; set; }
        public RegisterViewModel RegViewMod { get; set; }
        public IndexViewModel IndViewMod { get; set; }
    }
}