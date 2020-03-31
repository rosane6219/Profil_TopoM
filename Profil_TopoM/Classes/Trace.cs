using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profil_TopoM
{
    class Trace
    {
        private int min,max,echelle,equidistance;
        private String image;
       // private Profil profil;
       public Trace(int mini,int maxi,int echel,int equi,String img)
        {
            this.min = mini;
            this.max = maxi;
            this.echelle = echel;
            this.equidistance = equi;
            this.image = img;
        }
        public int Min { get { return min ; } set { this.min = value; } }
        public int Max { get { return max ; } set { this.max = value; } }
        public int Echelle { get { return echelle ; } set { this.echelle = value; } }
        public int Equidistance { get { return equidistance; } set { this.equidistance = value; } }
        public String Image { get { return image ; } set { this.image = value ; } }
        //public afficher(){ }
        // public modifier() { }
        //public renommer(){}

    }
}
