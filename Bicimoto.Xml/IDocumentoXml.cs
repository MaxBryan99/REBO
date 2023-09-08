using Bicimoto.Comun;
using Bicimoto.Comun.Dto.Contratos;

namespace Bicimoto.Xml
{
    public interface IDocumentoXml
    {
        IEstructuraXml Generar(IDocumentoElectronico request);
    }
}