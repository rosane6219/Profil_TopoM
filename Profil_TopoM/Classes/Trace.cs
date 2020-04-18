using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profil_TopoM
{
   public  class Trace
    {
        public int min, max, echelle, equidistance;
        public  String image, nom;
        public DateTime date_creat, date_modif;
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

        // public  void modifier() { }
        //public void  renommer(){}
        // public void genererProfil(list< point >) {}

    }
}