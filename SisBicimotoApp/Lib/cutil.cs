using Microsoft.Win32;
using System.Windows.Forms;

namespace SisBicimotoApp.Lib
{
    public sealed class cutil
    {
        public cutil()
        {
        }

        public static string encriptaclave(string Texto)
        {
            string Rpta = "";
            for (int K = 0; (K <= Texto.Length - 1); K++)
            {
                if (((int)Texto[K]) <= 127)
                    Rpta = @Rpta + (char)(127 - (int)(Texto[K]) + 31);
                else
                    Rpta = @Rpta + @Texto[K];
            }
            return Rpta;
        }

        public static int sacarentero(string cadena)
        {
            int numero = 0;
            if (cadena.Length > 0)
            {
                numero = System.Convert.ToInt32(cadena);
            }
            return numero;
        }

        public static void formatofecha()
        {
            try
            {
                //Cambiar el formato
                RegistryKey reg1 = Registry.CurrentUser;
                reg1 = reg1.CreateSubKey(@"Control Panel\International");
                reg1.SetValue("sShortDate", @"dd/MM/yyyy");
                reg1.SetValue("sDecimal", ".");
                Registry.CurrentUser.Flush();

                RegistryKey reg2 = Registry.Users;
                reg2 = reg2.CreateSubKey(@".DEFAULT\Control Panel\International");
                reg2.SetValue("sShortDate", @"dd/MM/yyyy");
                reg2.SetValue("sDecimal", ".");
                Registry.Users.Flush();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}