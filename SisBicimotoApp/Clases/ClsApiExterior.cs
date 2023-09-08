using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SisBicimotoApp.Clases
{
    internal class ClsApiExterna
    {
        public string numero;
        public string nombreCompleto;
        public string ruc;
        public string nombreORazonSocial;
        public string direccion;
        public string estado;

        public ClsApiExterna()
        {

        }

        public ClsApiExterna(string numero, string nombreCompleto, string ruc, string nombreORazonSocial, string direccion, string estado)
        {
            this.numero = numero;
            this.nombreCompleto = nombreCompleto;
            this.ruc = ruc;
            this.nombreORazonSocial = nombreORazonSocial;
            this.direccion = direccion;
            this.estado = estado;
        }

        private static readonly HttpClient _httpClient;


        public Boolean ObtenerRazonSocial(string numeroRuc)
        {
            HttpClient client = new HttpClient();
            string apiRuc = $"https://apiperu.dev/api/ruc/{numeroRuc}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "4e96942bcac70184a5384bed0fd09223713a31d7cb765c7de27a88c84b12f484");
            Boolean res = false;
            HttpResponseMessage responseMessage = client.GetAsync(apiRuc).GetAwaiter().GetResult();
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResult);

                bool success = data.success;
                if (success)
                {
                    this.nombreORazonSocial = data.data.nombre_o_razon_social;
                    this.direccion = data.data.direccion;
                    this.estado = data.data.estado;
                    res = true;
                }
                else
                {
                    
                }
            }
            return res;
        }

       

        public Boolean ObtenerNombreCompleto(string numero)
        {
            HttpClient client = new HttpClient();
            string apiDni = $"https://apiperu.dev/api/dni/{numero}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "4e96942bcac70184a5384bed0fd09223713a31d7cb765c7de27a88c84b12f484");
            Boolean res = false;
            HttpResponseMessage responseMessage = client.GetAsync(apiDni).GetAwaiter().GetResult();
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonResult = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResult);

                bool success = data.success;
                if (success)
                {
                    this.nombreCompleto = data.data.nombre_completo;
                    res = true;
                }
                else
                {
                    
                }
            }
            return res;
        }

        

    }
}
