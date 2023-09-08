namespace SisBicimotoApp.Clases
{
    public class ClsResumen
    {
        public int Item { get; set; }
        public string TipoComprobante { get; set; }
        public string Serie { get; set; }
        public int Inicio { get; set; }
        public int Fin { get; set; }
        public double Gravadas { get; set; }
        public double Exoneradas { get; set; }
        public double Gratuitas { get; set; }
        public double Igv { get; set; }
        public double SumaTotal { get; set; }
        public double Total { get; set; }
        public string TDoc { get; set; }
        public int Conteo { get; set; }
        public int Dife { get; set; }
    }
}