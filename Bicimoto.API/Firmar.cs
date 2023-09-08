using Bicimoto.Comun.Dto.Intercambio;
using Bicimoto.Firmado;
using System;
using System.Threading.Tasks;

namespace Bicimoto.API
{
    public class Firmar
    {
        private readonly ICertificador _certificador;

        public Firmar(ICertificador certificador)
        {
            _certificador = certificador;
        }

        public async Task<FirmadoResponse> Post(FirmadoRequest request)
        {
            var response = new FirmadoResponse();

            try
            {
                response = await _certificador.FirmarXml(request);
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
    }
}