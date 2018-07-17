using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Poslovni.Klase
{
    public class ArtikliAtributi
    {
        public List<String> _grupa { get; set; }
        public List<String> _podgrupa { get; set; }
        public List<String> _vrsta_robe { get; set; }

        public List<String> _robna_marka { get; set; }
        public List<String> _porezna_grupa { get; set; }
        public List<String> _osobine_artikla { get; set; }


        //SET VALUES
        public void SetGrupa(List<String> grupa)
        {
            _grupa = grupa;
        }
        public void SetPodgrupa(List<String> podgrupa)
        {
            _podgrupa = podgrupa;
        }
        public void SetVrstaRobe(List<String> vrsta_robe)
        {
            _vrsta_robe = vrsta_robe;
        }
        public void SetRobnaMarka(List<String> robna_marka)
        {
            _robna_marka = robna_marka;
        }
        public void SetPoreznaGrupa(List<String> porezna_grupa)
        {
            _porezna_grupa = porezna_grupa;
        }
        public void SetOsobineArtikla(List<String> osobine_artikla)
        {
            _osobine_artikla = osobine_artikla;
        }


        //GET VALUES

        public List<String> GetGrupa()
        {
            return _grupa;
        }
        public List<String> GetPodgrupa()
        {
            return _podgrupa;
        }
        public List<String> GetVrstaRobe()
        {
            return _vrsta_robe;
        }
        public List<String> GetRobnaMarka()
        {
            return _robna_marka;
        }
        public List<String> GetPoreznaGrupa()
        {
            return _porezna_grupa;
        }
        public List<String> GetOsobineArtikla()
        {
            return _osobine_artikla;
        }

        string dirpath = AppDomain.CurrentDomain.BaseDirectory + "/" + "Atributi";

        public void Init()
        {

            Directory.CreateDirectory(dirpath);

            if (!File.Exists(dirpath + "/" + "grupe.txt"))
            {
                File.Create(dirpath + "/" + "grupe.txt");
            }
            if (!File.Exists(dirpath + "/" + "podgrupe.txt"))
            {
                File.Create(dirpath + "/" + "podgrupe.txt");
            }
            if (!File.Exists(dirpath + "/" + "vrsterobe.txt"))
            {
                File.Create(dirpath + "/" + "vrsterobe.txt");
            }
            if (!File.Exists(dirpath + "/" + "robnamarka.txt"))
            {
                File.Create(dirpath + "/" + "robnamarka.txt");
            }
            if (!File.Exists(dirpath + "/" + "poreznagrupa.txt"))
            {
                File.Create(dirpath + "/" + "poreznagrupa.txt");
            }
            if (!File.Exists(dirpath + "/" + "osobineartikla.txt"))
            {
                File.Create(dirpath + "/" + "osobineartikla.txt");
            }


            SetGrupa(File.ReadAllLines(dirpath + "/" + "grupe.txt").ToList());
            SetPodgrupa(File.ReadAllLines(dirpath + "/" + "podgrupe.txt").ToList());
            SetRobnaMarka(File.ReadAllLines(dirpath + "/" + "robnamarka.txt").ToList());
            SetOsobineArtikla(File.ReadAllLines(dirpath + "/" + "osobineartikla.txt").ToList());
        }
        public void UpdateFileWithValues(int id)
        {
            switch (id)
            {
                case 0:
                    using (TextWriter tw = new StreamWriter(dirpath + "/" + "grupe.txt"))
                    {
                        foreach (String s in _grupa)
                            tw.WriteLine(s);
                    }
                    break;
                case 1:
                    using (TextWriter tw = new StreamWriter(dirpath + "/" + "podgrupe.txt"))
                    {
                        foreach (String s in _podgrupa)
                            tw.WriteLine(s);
                    }
                    break;
                case 2:
                    using (TextWriter tw = new StreamWriter(dirpath + "/" + "osobineartikla.txt"))
                    {
                        foreach (String s in _osobine_artikla)
                            tw.WriteLine(s);
                    }
                    break;
                case 3:
                    using (TextWriter tw = new StreamWriter(dirpath + "/" + "robnamarka.txt"))
                    {
                        foreach (String s in _robna_marka)
                            tw.WriteLine(s);
                    }
                    break;
            }
        }
    }
}
