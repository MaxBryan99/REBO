using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsProducto
    {
        public string CodArt;
        public string Nombre;
        public string CodInternac;
        public string TipProducto;
        public string CodMarca;
        public string CodModelo;
        public string CodFamilia;
        public string CodLinea;
        public string CodProced;
        public string CodUnidad;
        public string TipMoneda;
        public string GenStock;
        public double PCosto;
        public double PVenta;
        public double PMayorista;
        public double PVolumen;
        public double StockMin;
        public string Ubicacion;
        public string Proveedor;
        public string TSerie;
        public string IsVehiculo;
        public string Descricpcion;
        public byte[] Image;
        public string Est;
        public string PAraNac;
        public string ParNab;
        public string RucEmpresa;
        public string UserCreacion;
        public string UserModi;
        public string Almacen;
        public Double Stock;
        public Double Stock_CC;
        public Double Stock_Almacen;
        public Double CantidadProd;
        public Double TotalProm;
        public double PFOB;
        public double PCIF;
        public string VentaMin;
        public string StockReal;
        public double CantPrecioVolum;
        public string CodBarras;
        public double PorVenta;

        public ClsProducto()
        {
        }

        public ClsProducto(string CodArt, string Nombre, string CodInternac, string TipProducto, string CodMarca,
                            string CodModelo, string CodFamilia, string CodLinea, string CodProced, string CodUnidad,
                            string TipMoneda, string GenStock, double PCosto, double PVenta, double PMayorista, double PVolumen,
                            double StockMin, string Ubicacion, string Proveedor, string TSerie, string IsVehiculo,
                            string Descricpcion, byte[] Image, string Est, string PAraNac, string ParNab, string RucEmpresa,
                            string UserCreacion, string UserModi, string Almacen, Double CantidadProd, Double TotalProm, double PFOB, double PCIF, string VentaMin, string StockReal,
                            double CantPrecioVolum, string CodBarras, double PorVenta)
        {
            this.CodArt = CodArt;
            this.Nombre = Nombre;
            this.CodInternac = CodInternac;
            this.TipProducto = TipProducto;
            this.CodMarca = CodMarca;
            this.CodModelo = CodModelo;
            this.CodFamilia = CodFamilia;
            this.CodLinea = CodLinea;
            this.CodProced = CodProced;
            this.CodUnidad = CodUnidad;
            this.TipMoneda = TipMoneda;
            this.GenStock = GenStock;
            this.PCosto = PCosto;
            this.PVenta = PVenta;
            this.PMayorista = PMayorista;
            this.PVolumen = PVolumen;
            this.StockMin = StockMin;
            this.Ubicacion = Ubicacion;
            this.Proveedor = Proveedor;
            this.TSerie = TSerie;
            this.IsVehiculo = IsVehiculo;
            this.Descricpcion = Descricpcion;
            this.Image = Image;
            this.Est = Est;
            this.PAraNac = PAraNac;
            this.ParNab = ParNab;
            this.RucEmpresa = RucEmpresa;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.Almacen = Almacen;
            this.CantidadProd = CantidadProd;
            this.TotalProm = TotalProm;
            this.PFOB = PFOB;
            this.PCIF = PCIF;
            this.VentaMin = VentaMin;
            this.StockReal = StockReal;
            this.CantPrecioVolum = CantPrecioVolum;
            this.CodBarras = CodBarras;
            this.PorVenta = PorVenta;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProductoCrear('" +
                                            this.CodArt.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.CodInternac.ToString() + "','" +
                                            this.TipProducto.ToString() + "','" +
                                            this.CodMarca.ToString() + "','" +
                                            this.CodModelo.ToString() + "','" +
                                            this.CodFamilia.ToString() + "','" +
                                            this.CodLinea.ToString() + "','" +
                                            this.CodProced.ToString() + "','" +
                                            this.CodUnidad.ToString() + "','" +
                                            this.TipMoneda.ToString() + "','" +
                                            this.GenStock.ToString() + "'," +
                                            this.PCosto + "," +
                                            this.PVenta + "," +
                                            this.PMayorista + "," +
                                            this.PVolumen + "," +
                                            this.StockMin + ",'" +
                                            this.Ubicacion + "','" +
                                            this.Proveedor + "','" +
                                            this.TSerie + "','" +
                                            this.IsVehiculo + "','" +
                                            this.Descricpcion + "','" +
                                            //this.Image + "','" +
                                            this.Est + "','" +
                                            //this.PAraNac + "','" +
                                            //this.ParNab + "','" +
                                            this.RucEmpresa + "','" +
                                            this.UserCreacion.ToString() + "','" +
                                            this.Almacen.ToString() + "'," +
                                            this.PFOB + "," +
                                            this.PCIF + ",'" +
                                            this.VentaMin.ToString() + "','" +
                                            this.StockReal.ToString() + "', " + 
                                            this.CantPrecioVolum + " , " +
                                            this.PorVenta + ")");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean Crear104()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProductoCrear_104('" +
                                            this.CodArt.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.CodInternac.ToString() + "','" +
                                            this.TipProducto.ToString() + "','" +
                                            this.CodMarca.ToString() + "','" +
                                            this.CodModelo.ToString() + "','" +
                                            this.CodFamilia.ToString() + "','" +
                                            this.CodLinea.ToString() + "','" +
                                            this.CodProced.ToString() + "','" +
                                            this.CodUnidad.ToString() + "','" +
                                            this.TipMoneda.ToString() + "','" +
                                            this.GenStock.ToString() + "'," +
                                            this.PCosto + "," +
                                            this.PVenta + "," +
                                            this.PMayorista + "," +
                                            this.PVolumen + "," +
                                            this.StockMin + ",'" +
                                            //this.Ubicacion + "','" +
                                            this.Proveedor + "','" +
                                            this.TSerie + "','" +
                                            this.IsVehiculo + "','" +
                                            this.Descricpcion + "','" +
                                            //this.Image + "','" +
                                            this.Est + "','" +
                                            //this.PAraNac + "','" +
                                            //this.ParNab + "','" +
                                            this.RucEmpresa + "','" +
                                            this.UserCreacion.ToString() + "','" +
                                            this.Almacen.ToString() + "'," +
                                            this.PFOB + "," +
                                            this.PCIF + ",'" +
                                            this.VentaMin.ToString() + "','" +
                                            this.StockReal.ToString() + "', " +
                                            this.CantPrecioVolum + " )");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean Modificar()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProductoActualiza('" +
                                                this.CodArt.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.CodInternac.ToString() + "','" +
                                            this.TipProducto.ToString() + "','" +
                                            this.CodMarca.ToString() + "','" +
                                            this.CodModelo.ToString() + "','" +
                                            this.CodFamilia.ToString() + "','" +
                                            this.CodLinea.ToString() + "','" +
                                            this.CodProced.ToString() + "','" +
                                            this.CodUnidad.ToString() + "','" +
                                            this.TipMoneda.ToString() + "','" +
                                            this.GenStock.ToString() + "'," +
                                            this.PCosto + "," +
                                            this.PVenta + "," +
                                            this.PMayorista + "," +
                                            this.PVolumen + "," +
                                            this.StockMin + ",'" +
                                            this.Ubicacion + "','" +
                                            this.Proveedor + "','" +
                                            this.TSerie + "','" +
                                            this.IsVehiculo + "','" +
                                            this.Descricpcion + "','" +
                                            //this.Image + "','" +
                                            this.Est + "','" +
                                            //this.PAraNac + "','" +
                                            //this.ParNab + "','" +
                                            this.RucEmpresa + "','" +
                                            this.UserModi.ToString() + "','" +
                                            this.Almacen.ToString() + "'," +
                                            this.PFOB + "," +
                                            this.PCIF + ",'" +
                                            this.VentaMin.ToString() + "','" +
                                            this.StockReal.ToString() + "', " +
                                            this.CantPrecioVolum + " , " +
                                            this.PorVenta + ")");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean Modificar104()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProductoActualiza_104('" +
                                                this.CodArt.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.CodInternac.ToString() + "','" +
                                            this.TipProducto.ToString() + "','" +
                                            this.CodMarca.ToString() + "','" +
                                            this.CodModelo.ToString() + "','" +
                                            this.CodFamilia.ToString() + "','" +
                                            this.CodLinea.ToString() + "','" +
                                            this.CodProced.ToString() + "','" +
                                            this.CodUnidad.ToString() + "','" +
                                            this.TipMoneda.ToString() + "','" +
                                            this.GenStock.ToString() + "'," +
                                            this.PCosto + "," +
                                            this.PVenta + "," +
                                            this.PMayorista + "," +
                                            this.PVolumen + "," +
                                            this.StockMin + ",'" +
                                            //this.Ubicacion + "','" +
                                            this.Proveedor + "','" +
                                            this.TSerie + "','" +
                                            this.IsVehiculo + "','" +
                                            this.Descricpcion + "','" +
                                            //this.Image + "','" +
                                            //this.Est + "','" +
                                            //this.PAraNac + "','" +
                                            //this.ParNab + "','" +
                                            this.RucEmpresa + "','" +
                                            this.UserModi.ToString() + "','" +
                                            this.Almacen.ToString() + "'," +
                                            this.PFOB + "," +
                                            this.PCIF + ",'" +
                                            this.VentaMin.ToString() + "', " +
                                            this.CantPrecioVolum + " )");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean ValidarProducto(string vCodProducto, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpProductoBusCod('" + vCodProducto.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

        public Boolean BuscarImagenProducto(string vCodProducto, string vRucEmpresa, string vAlamcen)
        {
            Boolean res = false;


            DataSet datos = csql.dataset_cadena("Call SpProductoBusImagen('" + vCodProducto.ToString() + "' ,'" + vRucEmpresa.ToString() + "' ,'" + vAlamcen.ToString() + "') ");
                if(datos.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow fila in datos.Tables[0].Rows)
                {
                    byte[] Bitsdatos = new byte[0];
                    Bitsdatos = (byte[])fila[0];
                    this.Image = Bitsdatos;
                }

                res = true;
            }
            else
            {

            }
            return res;
        }
        public Boolean BuscarProducto(string vCodProducto, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpProductoBusCod('" + vCodProducto.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CodArt = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.CodInternac = fila[2].ToString();
                    this.TipProducto = fila[3].ToString();
                    this.CodMarca = fila[4].ToString();
                    this.CodModelo = fila[5].ToString();
                    this.CodFamilia = fila[6].ToString();
                    this.CodLinea = fila[7].ToString();
                    this.CodProced = fila[8].ToString();
                    this.CodUnidad = fila[9].ToString();
                    this.TipMoneda = fila[10].ToString();
                    this.GenStock = fila[11].ToString();
                    this.PCosto = double.Parse(fila[12].ToString());
                    this.PVenta = double.Parse(fila[13].ToString());
                    this.PMayorista = double.Parse(fila[14].ToString());
                    this.PVolumen = double.Parse(fila[15].ToString());
                    if (fila[16].ToString().Equals(null) || fila[16].ToString().Equals(""))
                    {
                        this.StockMin = 0;
                    }
                    else
                    {
                        this.StockMin = double.Parse(fila[16].ToString());
                    }
                    this.Ubicacion = fila[17].ToString();
                    this.Proveedor = fila[18].ToString();
                    this.TSerie = fila[19].ToString();
                    this.IsVehiculo = fila[20].ToString();
                    this.Descricpcion = fila[21].ToString();
                    byte[] Bitsdatos = new byte[0];
                    //Bitsdatos = (byte[])fila[22];
                    if (fila[22] != DBNull.Value)
                    {
                        Bitsdatos = (byte[])fila[22];
                        this.Image = Bitsdatos;
                    }
                    else
                    {
                        this.Image = null;
                    }
                    this.Est = fila[23].ToString();
                    this.PFOB = double.Parse(fila[24].ToString());
                    this.PCIF = double.Parse(fila[25].ToString());
                    this.VentaMin = fila[26].ToString();
                    this.PorVenta = double.Parse(fila[27].ToString());



                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Producto no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarProductoActivo(string vCodProducto, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpProductoBusCodActivo('" + vCodProducto.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CodArt = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.CodInternac = fila[2].ToString();
                    this.TipProducto = fila[3].ToString();
                    this.CodMarca = fila[4].ToString();
                    this.CodModelo = fila[5].ToString();
                    this.CodFamilia = fila[6].ToString();
                    this.CodLinea = fila[7].ToString();
                    this.CodProced = fila[8].ToString();
                    this.CodUnidad = fila[9].ToString();
                    this.TipMoneda = fila[10].ToString();
                    this.GenStock = fila[11].ToString();
                    this.PCosto = double.Parse(fila[12].ToString());
                    this.PVenta = double.Parse(fila[13].ToString());
                    this.PMayorista = double.Parse(fila[14].ToString());
                    this.PVolumen = double.Parse(fila[15].ToString());
                    if (fila[16].ToString().Equals(null) || fila[16].ToString().Equals(""))
                    {
                        this.StockMin = 0;
                    }
                    else
                    {
                        this.StockMin = double.Parse(fila[16].ToString());
                    }
                    this.Ubicacion = fila[17].ToString();
                    this.Proveedor = fila[18].ToString();
                    this.TSerie = fila[19].ToString();
                    this.IsVehiculo = fila[20].ToString();
                    this.Descricpcion = fila[21].ToString();
                    byte[] Bitsdatos = new byte[0];
                    if (fila[22] != DBNull.Value)
                    {
                        Bitsdatos = (byte[])fila[22];
                        this.Image = Bitsdatos;
                    }
                    else
                    {
                        this.Image = null;
                    }
                    //Bitsdatos = (byte[])fila[22];
                    
                    this.Est = fila[23].ToString();
                    this.PFOB = double.Parse(fila[24].ToString());
                    this.PCIF = double.Parse(fila[25].ToString());
                    this.VentaMin = fila[26].ToString();
                    this.CodBarras = fila[29].ToString();

                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Producto no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarProductoPorCodBarrasActivo(string vRucEmpresa, string vAlmacen, string vCodBarras)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpProductoCodBarrasActivo('" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vCodBarras.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            { 
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CodArt = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.CodInternac = fila[2].ToString();
                    this.TipProducto = fila[3].ToString();
                    this.CodMarca = fila[4].ToString();
                    this.CodModelo = fila[5].ToString();
                    this.CodFamilia = fila[6].ToString();
                    this.CodLinea = fila[7].ToString();
                    this.CodProced = fila[8].ToString();
                    this.CodUnidad = fila[9].ToString();
                    this.TipMoneda = fila[10].ToString();
                    this.GenStock = fila[11].ToString();
                    this.PCosto = double.Parse(fila[12].ToString());
                    this.PVenta = double.Parse(fila[13].ToString());
                    this.PMayorista = double.Parse(fila[14].ToString());
                    this.PVolumen = double.Parse(fila[15].ToString());
                    if (fila[16].ToString().Equals(null) || fila[16].ToString().Equals(""))
                    {
                        this.StockMin = 0;
                    }
                    else
                    {
                        this.StockMin = double.Parse(fila[16].ToString());
                    }
                    this.Ubicacion = fila[17].ToString();
                    this.Proveedor = fila[18].ToString();
                    this.TSerie = fila[19].ToString();
                    this.IsVehiculo = fila[20].ToString();
                    this.Descricpcion = fila[21].ToString();
                    byte[] Bitsdatos = new byte[0];
                    if (fila[22] != DBNull.Value)
                    {
                        Bitsdatos = (byte[])fila[22];
                        this.Image = Bitsdatos;
                    }
                    else
                    {
                        this.Image = null;
                    }
                    //Bitsdatos = (byte[])fila[22];

                    this.Est = fila[23].ToString();
                    this.PFOB = double.Parse(fila[24].ToString());
                    this.PCIF = double.Parse(fila[25].ToString());
                    this.VentaMin = fila[26].ToString();
                    /* if (fila[28].ToString().Equals(null) || fila[28].ToString().Equals(""))
                     {
                         this.CantPrecioVolum = 0;
                     }
                     else
                     {
                         this.CantPrecioVolum = double.Parse(fila[28].ToString());
                     }*/

                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Producto no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarStock(string vCodProducto, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpStArticuloBusGen('" + vCodProducto.ToString() + "','" + vAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.RucEmpresa = fila[0].ToString();
                    this.Almacen = fila[1].ToString();
                    this.CodArt = fila[2].ToString();
                    this.Stock = Double.Parse(fila[3].ToString());
                    res = true;
                }
            }
            else
            {
                res = false;
            }
            return res;
        }

        public Boolean BuscarStock_2(string vCodProducto)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpStArticuloBusGen2('" + vCodProducto.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Stock_CC = Double.Parse(fila[0].ToString());
                    this.Stock_Almacen = Double.Parse(fila[1].ToString());
                    res = true;
                }
            }
            else
            {
                res = false;
            }
            return res;
        }

        public Boolean ActualizaStockaCero(string vCodProducto, string vRucEmpresa, string vAlmacen, string vUser)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProductoStock0('" + vCodProducto.ToString() + "','" + vRucEmpresa.ToString() + "','" + vUser.ToString() + "','" + vAlmacen.ToString() + "')");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean Eliminar(string vCodPro, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProductoElimina('" + vCodPro.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean ActualizaCosto(string vCodProducto, double vCosto, string vRucEmpresa, string vAlmacen, string vUser)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Update tblarticulos set PFOB = " + vCosto + ", fecmodi = Now(), UserModi = '" + vUser.ToString() + "' where CodArt ='" + vCodProducto.ToString() + "' and RucEmpresa = '" + vRucEmpresa.ToString() + "' and Almacen ='" + vAlmacen.ToString() + "'");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean PromedioVenta(string vCodProducto, string vRucEmpresa, string vAlmacen, int vMes)
        {
            Boolean res = false;
            DataSet datos = csql.dataset_cadena("Call SpProductoPromedio('" + vCodProducto.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "'," + vMes + ")");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CantidadProd = Double.Parse(fila[0].ToString());
                    this.TotalProm = Double.Parse(fila[1].ToString());
                    res = true;
                }
            }
            else
            {
                res = false;
            }
            return res;
        }

        public Boolean PromedioVentaFecha(string vCodProducto, string vRucEmpresa, string vAlmacen, string fecha1, string fecha2)
        {
            Boolean res = false;
            DataSet datos = csql.dataset_cadena("Call SpProductoPromedioFecha('" + vCodProducto.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + fecha1.ToString() + "','" + fecha2.ToString() + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CantidadProd = Double.Parse(fila[0].ToString());
                    this.TotalProm = Double.Parse(fila[1].ToString());
                    res = true;
                }
            }
            else
            {
                res = false;
            }
            return res;
        }
    }
}