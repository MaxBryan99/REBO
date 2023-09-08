namespace SisBicimotoApp.Interface
{
    public interface IAddItemCompra
    {
        void AddNewItem(string codPro, string nomPro, string undMed, double preCosto, double cantidad, double igv, double pordesc, double desc, double percep, double importe);
    }
}