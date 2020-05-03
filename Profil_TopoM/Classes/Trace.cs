using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profil_TopoM
{
   public  class Trace
    {
        public double min, max;
        public int  echelle,echellecarte, equidistance;
        public  String image, nom;
        public DateTime date_creat, date_modif;
      
        public Trace(String name, DateTime creat, DateTime modif, double mini, double maxi, int echel, int equi, String img,int carte)
        {
            this.nom = name;
            this.date_creat = creat;
            this.date_modif = modif;
            this.min = mini;
            this.max = maxi;
            this.echelle = echel;
            this.equidistance = equi;
            this.image = img;
            this.echellecarte = carte;
        }
      public Trace()
        {

        }

     

    }
}