using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SisBicimotoApp.Lib;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp.Clases
{
    class ClsDocumento
    {
        public string Codigo;
        public string Nombre;
        public string NCorto;
        public string Modulo;
        public int NFila;
        public string Est;
        public string Formato_Imp;
        public string EnvSunat;
        public string UserCreacion;
        public string UserModi;
        public string Impresora;
        public string Imp;
        public string TipDocElectronico;
        public ClsDocumento()
        {

        }

        public ClsDocumento(string Codigo, string Nombre, string NCorto, int NFila, string Est, string Formato_Imp,
                            string EnvSunat, string UserCreacion, string UserModi, string Impresora, string Imp, string TipDocElectronico)
        {
            this.Codigo = Codigo;
            this.Nombre = Nombre;
            this.NCorto = NCorto;
            this.NFila = NFila;
            this.Est = Est;
            this.Formato_Imp = Formato_Imp;
            this.EnvSunat = EnvSunat;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.Impresora = Impresora;
            this.Imp = Imp;
            this.TipDocElectronico = TipDocElectronico;
        }

        public Boolean BuscarDoc(string vCod)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDocBusCod('" + vCod.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.NCorto = fila[2].ToString();
                    this.Modulo = fila[3].ToString();
                    this.NFila = Int32.Parse(fila[4].ToString().Equals("") ? "0" : fila[4].ToString());
                    this.Est = fila[5].ToString();
                    this.Formato_Imp = fila[6].ToString();
                    this.EnvSunat = fila[7].ToString();
                    this.Impresora = fila[8].ToString();
                    this.Imp = fila[9].ToString();
                    this.TipDocElectronico = fila[10].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarDocNomMod(string vDes, string vMod)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDocBusNombreMod('" + vDes.ToString() + "','" + vMod.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.NCorto = fila[2].ToString();
                    this.Modulo = fila[3].ToString();
                    this.NFila = Int32.Parse(fila[4].ToString().Equals("") ? "0" : fila[4].ToString());
                    this.Est = fila[5].ToString();
                    this.Formato_Imp = fila[6].ToString();
                    this.EnvSunat = fila[7].ToString();
                    this.Impresora = fila[8].ToString();
                    this.Imp = fila[9].ToString();
                    this.TipDocElectronico = fila[10].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarDocNCortoMod(string vNcorto, string vMod)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDocBusNomCortoMod('" + vNcorto.ToString() + "','" + vMod.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.NCorto = fila[2].ToString();
                    this.Modulo = fila[3].ToString();
                    this.NFila = Int32.Parse(fila[4].ToString().Equals("") ? "0" : fila[4].ToString());
                    this.Est = fila[5].ToString();
                    this.Formato_Imp = fila[6].ToString();
                    this.EnvSunat = fila[7].ToString();
                    this.Impresora = fila[8].ToString();
                    this.Imp = fila[9].ToString();
                    this.TipDocElectronico = fila[10].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarDocSerieNCortoMod(string vNcorto, string vSerie, string vMod)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDocBusSerieNCortoMod('" + vNcorto.ToString() + "','" + vSerie.ToString() + "','" + vMod.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.NCorto = fila[2].ToString();
                    this.Modulo = fila[3].ToString();
                    this.NFila = Int32.Parse(fila[4].ToString().Equals("") ? "0" : fila[4].ToString());
                    this.Est = fila[5].ToString();
                    this.Formato_Imp = fila[6].ToString();
                    this.EnvSunat = fila[7].ToString();
                    this.Impresora = fila[8].ToString();
                    this.Imp = fila[9].ToString();
                    this.TipDocElectronico = fila[10].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarDocSerieCodMod(string vDoc, string vSerie, string vMod)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDocBusSerieCodMod('" + vDoc.ToString() + "','" + vSerie.ToString() + "','" + vMod.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.NCorto = fila[2].ToString();
                    this.Modulo = fila[3].ToString();
                    this.NFila = Int32.Parse(fila[4].ToString().Equals("") ? "0" : fila[4].ToString());
                    this.Est = fila[5].ToString();
                    this.Formato_Imp = fila[6].ToString();
                    this.EnvSunat = fila[7].ToString();
                    this.Impresora = fila[8].ToString();
                    this.Imp = fila[9].ToString();
                    this.TipDocElectronico = fila[10].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDocCrear('" +

                                             this.Nombre.ToString() + "','" +
                                             this.NCorto.ToString() + "','" +
                                             this.Modulo.ToString() + "' , " +
                                             this.NFila + ",'" +
                                             this.EnvSunat.ToString() + "','" +
                                             this.Formato_Imp.ToString() + "','" +
                                             this.Impresora.ToString() + "','" +
                                             this.UserCreacion.ToString() + "','" +
                                             this.Imp.ToString() + "','" +
                                             this.TipDocElectronico.ToString() + "')");

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

        public Boolean Modificar()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDocActualiza('" +
                                                 this.Codigo.ToString() + "','" +
                                                 this.Nombre.ToString() + "','" +
                                                 this.NCorto.ToString() + "','" +
                                                 this.Modulo.ToString() + "' , " +
                                                 this.NFila + ",'" +
                                                 this.EnvSunat.ToString() + "','" +
                                                 this.Formato_Imp.ToString() + "','" +
                                                 this.Impresora.ToString() + "','" +
                                                 this.UserCreacion.ToString() + "','" +
                                                 this.Imp.ToString() + "','" +
                                                 this.TipDocElectronico.ToString() + "')");


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

        public Boolean Eliminar(string doc)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDocElimina('" +
                                                 doc.ToString() + "')");

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
