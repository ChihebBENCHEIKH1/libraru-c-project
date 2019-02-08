using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio
{
    class ouvrage
    {   
        protected String date;
        protected String numero;

        public ouvrage(String d,String n)
        {
            date = d;
            numero = n;
        }
        // setters
        public void setDate(String d)
        {
            date = d;
        }
        public void setNumero(String n)
        {
            numero = n;
        }
        // getters
        public String getDate()
        {
            return date;
        }
        public String getNumero()
        {
            return numero;
        }

    }
}
