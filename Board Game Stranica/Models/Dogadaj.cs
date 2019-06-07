using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Board_Game_Stranica.Models
{
    //koju bazu koristi
    [Table("dogadaji")]
    public class Dogadaj
    {
        // id dogadaja
        [Display(Name = "ID drustvene igre")]
        [Key]
        public int Id { get; set; }

        // naziv dogadaja
        private string naziv;

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} je obavezan")]
        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }
        //mjesto odrzavanja
        [Column("mjesto")]
        [Display(Name = "Mjesto odrzavanja")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Mjesto { get; set; }

        // Datum odrzavanja
        [Column("datum")]
        [Display(Name = "Datum odrzavanja")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:mm-dd-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} je obavezan")]
        public DateTime DatumOdrzavanja { get; set; }

        //organizator
        [Column("organizator")]
        [Display(Name = "Organizator")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Organizator { get; set; }


    }
}