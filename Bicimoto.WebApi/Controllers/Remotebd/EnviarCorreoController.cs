using Bicimoto.Comun.Dto.Data;
using Bicimoto.Comun.Dto.Intercambio;
using Bicimoto.Comun.Dto.Interface;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Bicimoto.WebApi.Controllers.Remotebd
{
    public class EnviarCorreoController : ApiController
    {
        private readonly IVenta _venta;

        public EnviarCorreoController()
        {
            _venta = _venta = UnityConfig.GetConfiguredContainer()
                .Resolve<IVenta>(GetType().Name);
        }

        // GET: api/EnviarCorreo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EnviarCorreo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/EnviarCorreo
        public DocumentoResponse Post([FromBody] Venta objVenta)
        {
            var response = new DocumentoResponse();
            try
            {
                _venta.EnviarCorreo(objVenta);

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

        // PUT: api/EnviarCorreo/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/EnviarCorreo/5
        public void Delete(int id)
        {
        }
    }
}