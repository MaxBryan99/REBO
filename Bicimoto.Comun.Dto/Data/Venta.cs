using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

//using System.Collections.Generic;

namespace Bicimoto.Comun.Dto.Data
{
    public class Venta
    {
        [JsonProperty(Required = Required.Always)]
        public string IdVenta { get; set; }

        public string Fecha { get; set; }

        //public string Cliente { get; set; }
        public string TipDocCli { get; set; }

        public Cliente ObjCliente { get; set; }
        public string Cliente { get; set; }
        public string Doc { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public string TMoneda { get; set; }
        public string NPedido { get; set; }
        public double TCambio { get; set; }
        public string TVenta { get; set; }
        public int NDias { get; set; }
        public string FVence { get; set; }
        public double TBruto { get; set; }
        public double TExonerada { get; set; }
        public double TInafecta { get; set; }
        public double TGratuita { get; set; }
        public double TIgv { get; set; }
        public double Total { get; set; }
        public string TEst { get; set; }
        public string Est { get; set; }
        public string Empresa { get; set; }
        public string Almacen { get; set; }
        public string Vendedor { get; set; }
        public string Usuario { get; set; }
        public string NomArchXml { get; set; }
        public string UserCreacion { get; set; }
        public string UserModi { get; set; }
        public string FecCreacion { get; set; }
        public string FecModi { get; set; }
        public string Egratuita { get; set; }
        public string TComp { get; set; }
        public double Dcto { get; set; }
        public string ArchivoXml { get; set; }
        public string ArchXml { get; set; }
        public List<DetalleVenta> Items { get; set; }

        public Venta()
        {
            ObjCliente = new Cliente();
            Items = new List<DetalleVenta>();
        }

        public Boolean BuscarVenta(string vIdVenta, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpVentaBuscar('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Fecha = fila[0].ToString();
                    this.Cliente = fila[1].ToString();
                    this.TipDocCli = fila[2].ToString();
                    this.Doc = fila[3].ToString();
                    this.Serie = fila[4].ToString();
                    this.Numero = fila[5].ToString();
                    this.TMoneda = fila[6].ToString();
                    this.NPedido = fila[7].ToString();
                    this.TCambio = Double.Parse(fila[8].ToString().Equals("") ? "0" : fila[8].ToString());
                    this.TVenta = fila[9].ToString();
                    this.NDias = Int32.Parse(fila[10].ToString().Equals("") ? "0" : fila[10].ToString());
                    this.FVence = fila[11].ToString();
                    this.TBruto = Double.Parse(fila[12].ToString().Equals("") ? "0" : fila[12].ToString());
                    this.TIgv = Double.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.Total = Double.Parse(fila[14].ToString().Equals("") ? "0" : fila[14].ToString());
                    //byte[] Bitsdatos = new byte[0];
                    //Bitsdatos = (byte[])fila[15];
                    this.ArchXml = fila[15].ToString();
                    this.NomArchXml = fila[16].ToString();
                    this.FecCreacion = fila[17].ToString();
                    this.Vendedor = fila[18].ToString();
                    this.TExonerada = Double.Parse(fila[19].ToString().Equals("") ? "0" : fila[19].ToString());
                    this.TInafecta = Double.Parse(fila[20].ToString().Equals("") ? "0" : fila[20].ToString());
                    this.TGratuita = Double.Parse(fila[21].ToString().Equals("") ? "0" : fila[21].ToString());
                    this.Egratuita = fila[22].ToString();
                    this.TComp = fila[23].ToString();
                    this.Dcto = Double.Parse(fila[24].ToString().Equals("") ? "0" : fila[24].ToString());
                    this.ArchivoXml = fila[25].ToString();
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