using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsSerie
    {
        public string Doc;
        public string Serie;
        public string PrefijoSerie;
        public int Numero;
        public string Correla;
        public string NumSerieImp;
        public string UserCreacion;
        public string UserModi;
        public string SerieAnt;
        public string Formato_Imp;
        public string Impresora;

        public ClsSerie()
        {
        }

        public ClsSerie(string Doc, string Serie, string PrefijoSerie, int Numero, string Correla, string NumSerieImp, string UserCreacion, string UserModi, string SerieAnt, string Formato_Imp, string Impresora)
        {
            this.Doc = Doc;
            this.Serie = Serie;
            this.PrefijoSerie = PrefijoSerie;
            this.Numero = Numero;
            this.Correla = Correla;
            this.NumSerieImp = NumSerieImp;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.SerieAnt = SerieAnt;
            this.Formato_Imp = Formato_Imp;
            this.Impresora = Impresora;
        }

        public Boolean ActualizaCorrela(string vDoc, string vSerie)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpSerieActualizaCorrela('" + vDoc.ToString() + "','" + vSerie.ToString() + "')");

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

        public Boolean BuscarDocSerie(string vDoc, string vSerie)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpSeriebusDocSer('" + vDoc.ToString() + "','" + vSerie.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Doc = fila[0].ToString();
                    this.Serie = fila[1].ToString();
                    this.PrefijoSerie = fila[2].ToString();
                    this.Numero = Int32.Parse(fila[3].ToString());
                    this.Correla = fila[4].ToString();
                    this.NumSerieImp = fila[5].ToString();
                    this.Formato_Imp = fila[6].ToString();
                    this.Impresora = fila[7].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarDocSerieVentas(string vDoc, string vSerie)
        {
            Boolean res = false;
            int nValor = 0;
            DataSet datos = csql.dataset_cadena("Call SpSerieBusVentas('" + vDoc.ToString() + "', '" + vSerie.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    nValor = int.Parse(fila[0].ToString());
                    if (nValor > 0)
                    {
                        res = true;
                    }
                }
            }
            else
            {
                res = false;
            }

            return res;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpSerieCrear('" +
                                             this.Doc.ToString() + "','" +
                                             this.Serie.ToString() + "','" +
                                             this.PrefijoSerie.ToString() + "' ," +
                                             this.Numero + ",'" +
                                             this.Correla.ToString() + "','" +
                                             this.NumSerieImp.ToString() + "','" +
                                             this.UserCreacion.ToString() + "','" +
                                             this.Formato_Imp.ToString() + "','" +
                                             this.Impresora.ToString() + "')");

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

            int resultado = csql.comando_cadena("Call SpSerieActualiza('" +
                                             this.Doc.ToString() + "','" +
                                             this.Serie.ToString() + "','" +
                                             this.PrefijoSerie.ToString() + "' ," +
                                             this.Numero + ",'" +
                                             this.Correla.ToString() + "','" +
                                             this.NumSerieImp.ToString() + "','" +
                                             this.UserCreacion.ToString() + "','" +
                                             this.SerieAnt.ToString() + "','" +
                                             this.Formato_Imp.ToString() + "','" +
                                             this.Impresora.ToString() + "')");

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