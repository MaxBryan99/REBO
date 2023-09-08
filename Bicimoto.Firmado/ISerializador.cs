using Bicimoto.Comun;
using Bicimoto.Comun.Dto.Intercambio;
using System.Threading.Tasks;

namespace Bicimoto.Firmado
{
    public interface ISerializador
    {
        Task<string> GenerarXml<T>(T objectToSerialize) where T : IEstructuraXml;

        Task<string> GenerarZip(string tramaXml, string nombreArchivo);

        Task<EnviarDocumentoResponse> GenerarDocumentoRespuesta(string constanciaRecepcion);
    }
}