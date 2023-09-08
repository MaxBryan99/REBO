using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp.Clases
{
    internal class ClsTipoCambio
    {
        public string Fecha;
        public double Valor;

        public ClsTipoCambio()
        {
        }

        public ClsTipoCambio(string Fecha, double Valor)
        {
            this.Fecha = Fecha;
            this.Valor = Valor;
        }

        public Boolean BuscarTipoCambio()
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpTipoCambioBusTC()");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Fecha = fila[0].ToString();
                    this.Valor = double.Parse(fila[1].ToString());
                    res = true;
                }
            }
            else
            {
                MessageBox.Show("Tipo de Cambio no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean Actualiza()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpTipoCambioActualiza(" +
                                            this.Valor + ")");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }
    }
}