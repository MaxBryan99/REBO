using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    internal static class Program
    {
        public static string CarpetaXml => "./XML";
        public static string CarpetaCdr => "./CDR";

        public static string NomAplicativo => "SisBicimotoApp";

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        public static void cerrar()
        {
            Application.Exit();
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}