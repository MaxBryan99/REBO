using Bicimoto.Comun.Dto.Intercambio;
using Bicimoto.Comun.Dto.Modelos;
using Bicimoto.Firmado;
using Bicimoto.Xml;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;

namespace Bicimoto.API
{
    public class GenerarResumenDiario
    {
        private IDocumentoXml _documentoXml;
        private readonly ISerializador _serializador;

        public GenerarResumenDiario(ISerializador serializador)
        {
            _serializador = serializador;
            _documentoXml = UnityConfig.GetConfiguredContainer()
                .Resolve<IDocumentoXml>(GetType().Name);
        }

        public async Task<DocumentoResponse> Post(ResumenDiario resumen)
        {
            var response = new DocumentoResponse();
            try
            {
                var summary = _documentoXml.Generar(resumen);
                response.TramaXmlSinFirma = await _serializador.GenerarXml(summary);
                response.Exito = true;
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.MensajeError = ex.Message;
                response.Pila = ex.StackTrace;
            }

            return response;
        }

        public async Task<DocumentoResponse> ResumenNuevo(ResumenDiarioNuevo resumen)
        {
            var response = new DocumentoResponse();
            try
            {
                // Solucion temporal --> Issue #58
                _documentoXml = UnityConfig.GetConfiguredContainer().Resolve<IDocumentoXml>();
                var summary = _documentoXml.Generar(resumen);
                response.TramaXmlSinFirma = await _serializador.GenerarXml(summary);
                response.Exito = true;
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.MensajeError = ex.Message;
                response.Pila = ex.StackTrace;
            }

            return response;
        }
    }
}