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
        private String image,nom;
        private DateTime date_creat, date_modif;
       // private Profil profil;
       public Trace(String name,DateTime creat,DateTime modif,int mini,int maxi,int echel,int equi,String img)
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
        public int Min { get { return min ; } set { this.min = value; } }
        public int Max { get { return max ; } set { this.max = value; } }
        public int Echelle { get { return echelle ; } set { this.echelle = value; } }
        public int Equidistance { get { return equidistance; } set { this.equidistance = value; } }
        public String Image { get { return image ; } set { this.image = value ; } }
        public String Nom { get { return nom ; } set { this.nom = value ;} }
        public DateTime Datecreation { get { return date_creat; } set { date_creat = value; } }
        public DateTime Datemodification { get { return date_modif ; } set { date_modif = value ; } }
        
        // public  void modifier() { }
        //public void  renommer(){}
        // public void genererProfil(list< point >) {}

    }
}
