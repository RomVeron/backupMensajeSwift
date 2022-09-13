using System;
using System.Collections.Generic;
using System.Text;

namespace f2
{
    class versiones
    {
        public static string textoVersion()
        {
            string vuelto = "";
            string saltoLinea="\r\n";
            vuelto += "Finansys2 Alpha" + saltoLinea;

            vuelto += saltoLinea;
            vuelto += "dd/mm/yyyy" + saltoLinea;
            vuelto += "* que se hizo" + saltoLinea;
            vuelto += "* que se hizo" + saltoLinea;            
            vuelto += "Nombre del developer;" + saltoLinea;

            vuelto += saltoLinea;
            vuelto += "dd/mm/yyyy" + saltoLinea;
            vuelto += "* que se hizo" + saltoLinea;
            vuelto += "* que se hizo" + saltoLinea;
            vuelto += "Nombre del developer;" + saltoLinea;
            
            return vuelto;
        }
    }
}
