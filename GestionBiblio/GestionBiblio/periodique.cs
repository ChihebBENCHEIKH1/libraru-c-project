using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio
{
    class periodique : ouvrage
    {
        public String nom; public String numéro; public String periodicite;
        public periodique(String d, String n, String numéro, String periodicite, String nom) : base(d, n)
        {
            this.nom = nom;
            this.numéro = numéro;
            this.periodicite = periodicite;
        }
    }
}
