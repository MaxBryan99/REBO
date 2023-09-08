namespace SisBicimotoApp.Interface
{
    internal interface IAddItemVenta
    {
        void AddNewItemVenta(string codPro, string nomPro, string marca, string undMed, double cantidad, double Dcto, double preVenta, double igv, double importe, string proced);
    }
}