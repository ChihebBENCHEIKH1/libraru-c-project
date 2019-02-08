using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio
{
    class livre : ouvrage
    {
        public String auteur;
        public String titre;
        public String editeur;

        public livre(String d, String n,String a,String t,String e):base(d,n)
        {
            auteur = a;
            titre = t;
            editeur = e;
        }
    }
}
