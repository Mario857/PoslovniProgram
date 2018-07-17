using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/*
 *  Mario Lučki 
 *  2.7.2018
 *  Printer naljenica korstenjem opensource html
 */

namespace Poslovni.Klase
{
    public delegate string UbaciElem();
    public delegate string UbaciElemSkripte();



    public enum Barkod {
        ean8 = 0,
        code128a = 1,
        code128auto = 2,
        code128b = 3,
        code128c = 4,
        code39ascii = 5,
        ean13 = 6,
        ext2 = 7,
        ext5 = 8,
        gs1databar14 = 9,
        i2of5 = 10,
        images = 11,
        industrial2of5 = 12,
        modifiedplessy = 13,
        uccean = 14,
        upca = 15,
        upce = 16,
        codabar = 17,
        code39 = 18
    }




    public class PrinterNaljepnica
    {
        private static string ean13barkod = "barkodovi/ean13/csshtmlEAN13Barcode.html";


        public static string UbaciElemente(List<Point> pozicije, List<Artikl> artikli,string kalkulacija) {
            StringBuilder stringBuilder = new StringBuilder();
            int p = 0;
            foreach (Artikl art in artikli) {
                stringBuilder.AppendLine(UbaciBarKod("barcode" + p, pozicije[p], art, kalkulacija));
                p++;
            }
            return stringBuilder.ToString();
        }

        public static string UbaciElementeSkripta(List<Artikl> artikli)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int p = 0;
            foreach (Artikl art in artikli)
            {
                stringBuilder.AppendLine(UbaciSkriptuZaElement("barcode"+p));
                p++;
            }
            return stringBuilder.ToString();
        }


        public static string UbaciSkriptuZaElement(string naziv_html_elementa) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Clear();
            stringBuilder.Append("function get_object(id) { var object = null; if (document.layers) { object = document.layers[id]; } else if (document.all) { object = document.all[id]; } else if (document.getElementById) { object = document.getElementById(id); } return object; } get_object('"+naziv_html_elementa+"').innerHTML=DrawHTMLBarcode_EAN13(get_object('"+ naziv_html_elementa + "').innerHTML,'yes','in ',0,2.5,1,'bottom','center','','black','white');");
            return stringBuilder.ToString();
        }
        public static string UbaciBarKod(string naziv_html_elementa, Point pozicija,Artikl art,string primka_ispis) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Clear();
            stringBuilder.Append("<div id='barcodecontainer' style='width: 5in '> <h2 style ='position: absolute; left: "+ (100 + pozicija.X)+ "px; top: "+ (pozicija.Y) + "px; ' > "+ art.naziv + " </h2> <h5 style = 'position: absolute; left: "+(300 + pozicija.X)+"px; top: "+(pozicija.Y)+"px; '> KAL : "+ primka_ispis + " </h5> <h2 style = 'position: absolute; left: "+(170 + pozicija.X)+"px; top: "+(30+pozicija.Y)+"px; '> "+art.MPC+" KN </h2> <div id='"+naziv_html_elementa+ "' style = 'position: absolute; left:  "+(80+pozicija.X)+ "px; top:"+(80+pozicija.Y)+"px;'>" + art.sifra+"</div>");
            return stringBuilder.ToString();
        }
        private static string StilZaglavlje() {
            return "<html xmlns='http://www.w3.org/1999/xhtml'> <head> <title>HTML EAN13 Barcode</title> <script type='text/javascript' src='js/connectcode-javascript-ean13.js'></script> <style type='text/css'> #barcode {font-weight: normal; font-style: normal; line-height:normal; sans-serif; font-size: 12pt} </style> </head> <body>";
        }

        public static void KreirajTestno(UbaciElem ubaciElem, UbaciElemSkripte elemSkripte) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(StilZaglavlje());



            stringBuilder.AppendLine(ubaciElem());
            //KLJUCIN


            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("<script type='text/javascript'>");



            stringBuilder.AppendLine(elemSkripte());
            //KLJUCIN

            stringBuilder.AppendLine("</script> </body> </html>");

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + ean13barkod, stringBuilder.ToString());

            Ispisi();
        }



        private static void Ispisi() {
            string path = AppDomain.CurrentDomain.BaseDirectory + ean13barkod;
            System.Diagnostics.Process.Start(path);
        }

    }
    
}
