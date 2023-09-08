using Newtonsoft.Json;

namespace Bicimoto.Comun.Dto.Data
{
    public class Cliente
    {
        [JsonProperty(Required = Required.Always)]
        public string RucDni { get; set; }

        public string TipDoc { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public string DireccionEnvio { get; set; }
        public string Region { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public double LimCredito { get; set; }
        public string CodVendedor { get; set; }
        public string Est { get; set; }
        public string FecCreacion { get; set; }
        public string UserCreacion { get; set; }
        public string FecModi { get; set; }
        public string UserModi { get; set; }
        public string RucEmpresa { get; set; }

        public Cliente()
        {
        }
    }
}