using Bicimoto.Comun.Dto.Data;
using System.Collections.Generic;

namespace Bicimoto.Comun.Dto.Interface
{
    public interface IVenta
    {
        void Generar(Venta _venta);

        void EnviarCorreo(Venta _venta, List<string> ArchivoPedido_ = null);
    }
}