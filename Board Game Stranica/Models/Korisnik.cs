using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Board_Game_Stranica.Models
{
    [Table("korisnici")]
    public class Korisnik
    {
            // id korisnika
            //[Display(Name = "ID korisnika")]
            [Key]
            public int Id_korisnik { get; set; }

            // ime
            private string ime;

            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezo")]
            public string Ime
            {
                get { return ime; }
                set { ime = value; }
            }

            // prezime
            [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezo")]
            public string Prezime { get; set; }

            // spol
            [Required(ErrorMessage = "{0} je obavezo")]
            public char Spol { get; set; }

            //mail
            [Required(ErrorMessage = "{0} je obavezno")]
            public string mail { get; set; }

            // Datum rodenja
            [Column("datum_rodenja")]
            [Display(Name = "Datum rođenja")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "{0} je obavezno")]
            public DateTime DatumRodenja { get; set; }

       
    }

}