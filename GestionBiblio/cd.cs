using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio
{
    class cd: ouvrage
    {
        public String auteur;
        public String titre;

        public cd(String d, String n, String a, String t) : base(d, n)
        {
            auteur = a;
            titre = t;
        }
    }
}