namespace SisBicimotoApp.Interface
{
    public interface IVenta
    {
        void CargarConsulta(string validaAnulaElimina);

        void GuardarVenta(string vCliente, string vtipdoc, string vSerie, string vComprobante, string vTipoPago);

        void GuardarProforma(string vCliente, string vtipdoc, string vSerie, string vComprobante);

        void validarVenta(string vVal);

        void validarPrecio(string vVal);
    }
}