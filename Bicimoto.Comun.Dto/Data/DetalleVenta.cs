namespace Bicimoto.Comun.Dto.Data
{
    public class DetalleVenta
    {
        public string IdVenta { get; set; }
        public string Codigo { get; set; }
        public string Marca { get; set; }
        public string Unidad { get; set; }
        public string Proced { get; set; }
        public double PVenta { get; set; }
        public double Cantidad { get; set; }
        public double Dcto { get; set; }
        public double Igv { get; set; }
        public double Importe { get; set; }
        public string Almacen { get; set; }
        public string Empresa { get; set; }
        public string TipPrecio { get; set; }
        public string TipImpuesto { get; set; }
        public string FecCreacion { get; set; }
        public string UserCreacion { get; set; }
        public string FecModi { get; set; }
        public string UserModi { get; set; }
        public int Norden { get; set; }

        public DetalleVenta()
        {
        }
    }
}