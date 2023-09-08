using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    public class ClsUbigeo
    {
        public string CODDPTO;
        public string CODPROV;
        public string CODDIST;
        public string NOMBRE;

        public ClsUbigeo()
        {
        }

        public ClsUbigeo(string CODDPTO, string CODPROV, string CODDIST, string NOMBRE)
        {
            this.CODDPTO = CODDPTO;
            this.CODPROV = CODPROV;
            this.CODDIST = CODDIST;
            this.NOMBRE = NOMBRE;
        }

        public Boolean BuscarDpto(string vDpto)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUbigeoBusDpto('" + vDpto.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CODDPTO = fila[0].ToString();
                    this.NOMBRE = fila[1].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarProv(string vDpto, string vProv)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUbigeoBusProv('" + vDpto.ToString() + "','" + vProv.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CODPROV = fila[0].ToString();
                    this.NOMBRE = fila[1].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarDist(string vDpto, string vProv, string vDist)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUbigeoBusDist('" + vDpto.ToString() + "','" + vProv.ToString() + "','" + vDist.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CODDIST = fila[0].ToString();
                    this.NOMBRE = fila[1].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }
    }
}