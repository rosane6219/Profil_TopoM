
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profil_TopoM
{
    public class Trace
    {

        // private Profil profil;
        public Trace(String name, String creat, String modif, int mini, int maxi, double echel, int equi, String img)
        {
            this.nom = name;
            this.date_creat = creat;
            this.date_modif = modif;// pour l'affichage => DateTime.Now.ToSrting("dd/mm/yyyy hh:mm:ss")
            this.min = mini;
            this.max = maxi;
            this.echelle = echel;

            this.equidistance = equi;
            this.image = img;
        }
        public Trace()
        {

        }

        public string nom { get; set; }
        public string image { get; set; }

        public int min { get; set; }
        public int max { get; set; }
        public double echelle { get; set; }

        public int equidistance { get; set; }
        public String date_creat { get; set; }
        public String date_modif { get; set; }


        // public  void modifier() { }
        //public void  renommer(){}
        // public void genererProfil(list< point >) {}

    }
}