using System;

public class Class1
{
    public Class1()
    {
        
    }

    public void soloLetras(ByRef e As System.Windows.Forms.KeyPressEventArgs) {

        if (Char.IsDigit(e.KeyChar)) { //Aca verificamos si es una letra, si es asi lo incluya
            e.Handled = True //Aca le decimos que lo incluya
        }
        else if (Char.IsControl(e.KeyChar))
        {
            e.Handled = False
        }
        else
        {        //En este caso es porque ingreso un numero
            e.Handled = False    'lo incluye
        }
    }
}
