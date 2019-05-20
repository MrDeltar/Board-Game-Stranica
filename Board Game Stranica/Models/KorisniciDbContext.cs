using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Board_Game_Stranica.Models
{
    public class KorisniciDbContext : DbContext
    {
        public DbSet<Korisnik> Korisnici { get; set; }
    }
}