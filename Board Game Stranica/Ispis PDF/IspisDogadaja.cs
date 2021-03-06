﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using Board_Game_Stranica.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;


namespace Board_Game_Stranica.Ispis_PDF
{
    public class IspisDogadaja
    {
        public byte[] Podaci { get; private set; }

        public IspisDogadaja(Dogadaj d)
        {
            // mjere
            Document pdfDokument = new Document(PageSize.A4, 72, 36, 50, 50);

            // generiranje u memoriju
            MemoryStream memStream = new MemoryStream();
            PdfWriter.GetInstance(pdfDokument, memStream);

            // otvaranje
            pdfDokument.Open();

            //====pišemo u pdf=====

            // definiranje fontova
            BaseFont bazniFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, true);
            Font header = new Font(bazniFont, 12, Font.NORMAL, BaseColor.DARK_GRAY);
            Font naslov = new Font(bazniFont, 14, Font.BOLDITALIC, BaseColor.BLACK);
            Font tekst = new Font(bazniFont, 10, Font.NORMAL, BaseColor.BLACK);


            // header
            pdfDokument.Add(
                new Paragraph(DateTime.Now.ToString("dd.MM.yyyy"), header));

            // slika
            var slika = Image.GetInstance(HostingEnvironment.MapPath("~/Pictures/520571-dice-100.png"));

            // alignment
            slika.Alignment = Element.ALIGN_LEFT;

            // scale
            slika.ScaleAbsoluteHeight(100);
            slika.ScaleAbsoluteWidth(100);
            pdfDokument.Add(slika);

            // naslov
            Paragraph p = new Paragraph("Detalji o odrzavanju društvene igre", naslov);
            p.Alignment = Element.ALIGN_LEFT;
            p.SpacingBefore = 30;
            p.SpacingAfter = 30;
            pdfDokument.Add(p);

            // info o dogadaju
            Paragraph info = new Paragraph("", tekst);
            info.Add("Test");
            //info.Add(d.Naziv);
            //info.Add(d.Mjesto);
            //info.Add(d.DatumOdrzavanja.ToString());
            //info.Add(d.Organizator);
            pdfDokument.Add(info);

            //=====zavrsavamo s pisanjem=====

            //zatvaramo
            pdfDokument.Close();
            Podaci = memStream.ToArray();
        }
    }
}