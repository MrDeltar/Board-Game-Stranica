using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Board_Game_Stranica.Models
{
    public class Dogadaj
    {
        // id dogadaja
        [Display(Name = "ID dogadaja")]
        public int Id { get; set; }

        // ime dogadaja
        private string ime;

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan")]
        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        // Datum odrzavanja
        [Display(Name = "Datum odrzavanja dogadaja")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} je obavezan")]
        public DateTime DatumOdrzavanja { get; set; }


       /* [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$")]
        public string TelefonskiBroj { get; set; }

        [EmailAddress]
        [CreditCard]
        public string Email { get; set; } */

    }
}