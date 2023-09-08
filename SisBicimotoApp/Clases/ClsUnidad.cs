using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SisBicimotoApp.Clases
{
    class ClsUnidad
    {
        public string Codigo;
        public string Nombre;

        public ClsUnidad()
        {

        }

        public ClsUnidad(string codigo, string nombre)
        {
            Codigo=codigo;
            Nombre=nombre;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpUnidadCrear('"+this.Nombre.ToString()+"')");

            if (resultado > 0) {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean BuscarUnidad(string vCodUnidad)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUnidadBusCod('" + vCodUnidad.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Nombre = fila[1].ToString();
                    res = true;
                }
            }
            else
            {   
                MessageBox.Show("Unidad no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean Modificar(string vCodigo)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpUnidadActualiza('" + vCodigo.ToString() + "','" + this.Nombre.ToString() + "')");

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

        public Boolean Eliminar_Est(string vCodigo)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpUnidadElimina('" + vCodigo.ToString() + "')");

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

        public Boolean BuscarUnidadCod(string vCod)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUnidadBusCod('" + vCod.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    //this.Codigo = fila[0].ToString();
                    this.Nombre = fila[0].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Empleado no encontrado", "SISMAD");				
            }
            return res;
        }
    }
}
