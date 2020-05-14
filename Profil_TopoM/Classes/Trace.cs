
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profil_TopoM
{
    public class Trace
    {
        public string nom { get; set; }
        public string image { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double echelle { get; set; }
        public double echellecarte { get; set; }
        public int equidistance { get; set; }
        public String date_creat { get; set; }
        public String date_modif { get; set; }

        public Trace(String name, String creat, String modif, double mini, double maxi, double echel, double echel2, int equi, String img)
        {
            this.nom = name;
            this.date_creat = creat;
            this.date_modif = modif;
            this.min = mini;
            this.max = maxi;
            this.echelle = echel;
            this.echellecarte = echel2;
            this.equidistance = equi;
            this.image = img;
        }
        public Trace() {}
    }
}