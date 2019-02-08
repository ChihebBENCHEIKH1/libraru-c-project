using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio
{
    class emprunt : ouvrage
    {
        public String client;
        public String cin;
        public String delai;
        public String typeOuvr;

        public emprunt(String d, String n, String cl, String cin, String delai, String type) :base(d,n)
        {
            client = cl;
            this.cin = cin;
            this.delai = delai;
            typeOuvr = type; 
        }

    }
}
