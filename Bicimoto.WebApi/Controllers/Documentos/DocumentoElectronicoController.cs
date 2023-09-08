using Bicimoto.Datos.Entidades;
using Bicimoto.Firmado;
using System.Web.Http;

namespace Bicimoto.WebApi.Controllers.Documentos
{
    [RoutePrefix("api/Documentos")]
    public class DocumentoElectronicoController : ApiController
    {
        private readonly ISerializador _serializador;
        private readonly ICertificador _certificador;
        private readonly BicimotoDb _context;

        public DocumentoElectronicoController(ISerializador serializador, ICertificador certificador, BicimotoDb context)
        {
            _serializador = serializador;
            _certificador = certificador;
            _context = context;
        }

        //[Route("Documento/Generar/{tipodoc}")]
        //[HttpPost]
        //public async Task<CabeceraDocumento> Generar(string tipoDoc, [FromBody]DocumentoElectronico documentoElectronico)
        //{
        //    IDocumentoXml documentoXml;
        //    switch (tipoDoc)
        //    {
        //        case "01":
        //        case "03":
        //            documentoXml = new FacturaXml();
        //            break;
        //        case "07":
        //            documentoXml = new NotaCreditoXml();
        //            break;
        //        case "08":
        //            documentoXml = new NotaCreditoXml();
        //            break;
        //        default:
        //            throw new InvalidOperationException($"El tipo de Documento {tipoDoc} no está registrado");
        //    }

        //    try
        //    {
        //        var tipoDocumento = await _context.Set<TipoDocumento>()
        //            .SingleOrDefaultAsync(p => p.Codigo == documentoElectronico.TipoDocumento);

        //        var cabeceraDocumento = new CabeceraDocumento
        //        {
        //            IdDocumento = documentoElectronico.IdDocumento,

        //        };

        //        var documento = documentoXml.Generar(documentoElectronico);

        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLine(ex);
        //        throw;
        //    }
        //}
    }
}