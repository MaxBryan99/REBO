using Bicimoto.Comun.Dto.Intercambio;
using System.Threading.Tasks;

namespace Bicimoto.Firmado
{
    public interface ICertificador
    {
        Task<FirmadoResponse> FirmarXml(FirmadoRequest request);
    }
}