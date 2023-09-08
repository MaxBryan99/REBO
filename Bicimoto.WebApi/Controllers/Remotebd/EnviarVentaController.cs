using Bicimoto.Comun.Dto.Data;
using Bicimoto.Comun.Dto.Intercambio;
using Bicimoto.Comun.Dto.Interface;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Bicimoto.WebApi.Controllers.Remotebd
{
    public class EnviarVentaController : ApiController
    {
        private readonly IVenta _venta;

        public EnviarVentaController()
        {
            _venta = _venta = UnityConfig.GetConfiguredContainer()
                .Resolve<IVenta>(GetType().Name);
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public DocumentoResponse Post([FromBody] Venta objVenta)
        {
            var response = new DocumentoResponse();
            try
            {
                _venta.Generar(objVenta);

                Boolean res = false;

                response.Exito = true;
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Pila = ex.StackTrace;
                response.Exito = false;
            }
            return response;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}