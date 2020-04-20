using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profil_TopoM
{
   public  class Trace
    {
       
        // private Profil profil;
        public Trace(String name, DateTime creat, DateTime modif, int mini, int maxi, int echel, int equi, String img)
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
        public int echelle { get; set; }
        public int equidistance { get; set; }
        public DateTime date_creat { get; set; }
        public DateTime date_modif { get; set; }


        // public  void modifier() { }
        //public void  renommer(){}
        // public void genererProfil(list< point >) {}

    }
}