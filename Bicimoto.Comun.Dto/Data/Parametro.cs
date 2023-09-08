using Newtonsoft.Json;
using System;
using System.Data;

namespace Bicimoto.Comun.Dto.Data
{
    public class Parametro
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public string UserCreacion { get; set; }
        public string UserModi { get; set; }

        public Parametro()
        {
        }

        public Boolean BuscarParametro(string vId)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpParametroBusCod('" + vId.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Id = fila[0].ToString();
                    this.Descripcion = fila[1].ToString();
                    this.Valor = fila[2].ToString();
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