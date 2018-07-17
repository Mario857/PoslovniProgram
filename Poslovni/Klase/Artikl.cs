/*
    Mario Lučki 
    2.7.2018
    Povezuje bazu sa skladistem artikla 
*/


using System.Drawing;

namespace Poslovni
{
    public struct Artikl
    {
        public int sifra;
        public string naziv; 
        public int kolicina;
        public int poreza_grupa;
        public float nab_cijena; 
        public float popust;
        public float MPC;
        public string ulaz;
        public string dobavljac;

        //LEFT TO IMP
        public string barkod;
        public string podgrupa;
        public string vrsta;
        public string robna_marka;
        public string osobine_artikla;
        public string opis_artikla;
        public float min_mpc;

        public Point ispisna_pozicija;
    }
}
