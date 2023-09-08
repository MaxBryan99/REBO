using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SisBicimotoApp.Lib
{
    internal class cini
    {
        [DllImport("kernel32", SetLastError = true)]
        private static extern int WritePrivateProfileString(string psSection, string psKey, string psValue, string psFile);

        [DllImport("kernel32", SetLastError = true)]
        private static extern int GetPrivateProfileString(string psSection, string psKey, string psDefault, byte[] psrReturn, int piBufferLen, string psFile);

        private string lsIniFilename;
        private int liBufferLen = 255;

        /// <summary>
        /// Declaracion de la Clase INI
        /// </summary>
        public cini(string psIniFilename)
        {
            lsIniFilename = psIniFilename;
        }

        /// <summary>
        /// INI directorio de ubicacion
        /// </summary>
        public string IniFile
        {
            get { return lsIniFilename; }
            set { lsIniFilename = value; }
        }

        /// <summary>
        /// Retorna la Longitud de la data
        /// </summary>
        public int BufferLen
        {
            get { return liBufferLen; }
            set { liBufferLen = value; }
        }

        /// <summary>
        /// Lee el valor de la variable (Observacion Julio, se pone la clave y un valor por default en caso de no encontrar clave)
        /// </summary>
        public string ReadValue(string psSection, string psKey, string psDefault)
        {
            byte[] sGetBuffer = new byte[this.liBufferLen];
            ASCIIEncoding oAscii = new ASCIIEncoding();
            int i = GetPrivateProfileString(psSection, psKey, psDefault, sGetBuffer, this.liBufferLen, this.lsIniFilename);
            return oAscii.GetString(sGetBuffer, 0, i);
        }

        /// <summary>
        /// Escribe valor en archivo INI (Observacion Julio, no se probo con multiples claves, pero no es necesario ya que no escribimos)
        /// </summary>
        public void WriteValue(string psSection, string psKey, string psValue)
        {
            WritePrivateProfileString(psSection, psKey, psValue, this.lsIniFilename);
        }

        /// <summary>
        /// Borra valor del archivo INI (No se usa)
        /// </summary>
        public void RemoveValue(string psSection, string psKey)
        {
            WritePrivateProfileString(psSection, psKey, null, this.lsIniFilename);
        }

        /// <summary>
        /// Leer Valor
        /// </summary>
        public void ReadValues(string psSection, ref Array poValues)
        {
            byte[] sGetBuffer = new byte[this.liBufferLen];
            int i = GetPrivateProfileString(psSection, null, null, sGetBuffer, this.liBufferLen, this.lsIniFilename);
            if (i != 0)
            {
                ASCIIEncoding oAscii = new ASCIIEncoding();
                poValues = oAscii.GetString(sGetBuffer, 0, i - 1).Split((char)0);
            }
        }

        /// <summary>
        /// Leer Seccion en formato Ascci
        /// </summary>
        public void ReadSections(ref Array poSections)
        {
            byte[] sGetBuffer = new byte[this.liBufferLen];
            int i = GetPrivateProfileString(null, null, null, sGetBuffer, this.liBufferLen, this.lsIniFilename);
            if (i != 0)
            {
                ASCIIEncoding oAscii = new ASCIIEncoding();
                poSections = oAscii.GetString(sGetBuffer, 0, i - 1).Split((char)0);
            }
        }

        /// <summary>
        /// Eliminar seccion del archivo (No usamos)
        /// </summary>
        public void RemoveSection(string psSection)
        {
            WritePrivateProfileString(psSection, null, null, this.lsIniFilename);
        }
    }
}