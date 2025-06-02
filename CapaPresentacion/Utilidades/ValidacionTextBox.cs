using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Utilidades
{
    public class ValidacionTextBox
    {
            public void soloLetras(System.Windows.Forms.KeyPressEventArgs e)
            {

                if (Char.IsDigit(e.KeyChar))
                { //Aca verificamos si es una letra, si es asi lo incluya
                e.Handled = true; //Aca le decimos que lo incluya
                }
                else if (Char.IsControl(e.KeyChar)) //Verificamos si es tecla de control
            {
                e.Handled = false; //lo excluye
                }
                else
                {        //En este caso es porque ingreso un numero
                e.Handled = false;    //lo excluye
                }
            }

            public void soloNumeros(System.Windows.Forms.KeyPressEventArgs e)
            {

                if (Char.IsDigit(e.KeyChar))
                { //Aca verificamos si es una letra, si es asi lo incluya
                    e.Handled = false; //Aca le decimos que lo excluya
                }
                else if (Char.IsControl(e.KeyChar)) //Verificamos si es tecla de control
                {
                    e.Handled = false; //lo excluye
                }
                else
                {        //En este caso es porque ingreso un numero
                    e.Handled = true;    //lo incluye
                }
            }
    }
}
