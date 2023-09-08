using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsProveedor
    {
        public string Ruc;
        public string Nombre;
        public string DireccionFiz;
        public string DireccionPar;
        public string Ciudad;
        public string Telefono;
        public string Fax;
        public string Email;
        public string TipoMon1;
        public string TipoMon2;
        public string NumCuenta1;
        public string NumCuenta2;
        public string Banco1;
        public string Banco2;
        public string Contacto;
        public string TelfContacto;
        public string Referencia;
        public string UsuarioCrea;
        public string UsuarioModi;
        public string Est;
        public string RucEmpresa;

        public ClsProveedor()
        {
        }

        public ClsProveedor(string Ruc, string Nombre, string DireccionFiz, string DireccionPar, string Ciudad, string Telefono,
                            string Fax, string Email, string TipoMon1, string TipoMon2, string NumCuenta1, string NumCuenta2, string Banco1, string Banco2, string Contacto, string TelfContacto, string Referencia, string UsuarioCrea, string UsuarioModi, string Est, string RucEmpresa)
        {
            this.Ruc = Ruc;
            this.Nombre = Nombre;
            this.DireccionFiz = DireccionFiz;
            this.DireccionPar = DireccionPar;
            this.Ciudad = Ciudad;
            this.Telefono = Telefono;
            this.Fax = Fax;
            this.Email = Email;
            this.TipoMon1 = TipoMon1;
            this.TipoMon2 = TipoMon2;
            this.NumCuenta1 = NumCuenta1;
            this.NumCuenta2 = NumCuenta2;
            this.Banco1 = Banco1;
            this.Banco2 = Banco2;
            this.Contacto = Contacto;
            this.TelfContacto = TelfContacto;
            this.Referencia = Referencia;
            this.UsuarioCrea = UsuarioCrea;
            this.UsuarioModi = UsuarioModi;
            this.Est = Est;
            this.RucEmpresa = RucEmpresa;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProveedorCrear('" +
                                            this.Ruc.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.DireccionFiz.ToString() + "','" +
                                            this.DireccionPar.ToString() + "','" +
                                            this.Ciudad.ToString() + "','" +
                                            this.Telefono.ToString() + "','" +
                                            this.Fax.ToString() + "','" +
                                            this.Email.ToString() + "','" +
                                            this.TipoMon1.ToString() + "','" +
                                            this.TipoMon2.ToString() + "','" +
                                            this.NumCuenta1.ToString() + "','" +
                                            this.NumCuenta2.ToString() + "','" +
                                            this.Banco1.ToString() + "','" +
                                            this.Banco2.ToString() + "','" +
                                            this.Contacto.ToString() + "','" +
                                            this.TelfContacto.ToString() + "','" +
                                            this.Referencia.ToString() + "','" +
                                            this.UsuarioCrea.ToString() + "','" +
                                            this.Est.ToString() + "','" +
                                            this.RucEmpresa.ToString() + "')");

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

            int resultado = csql.comando_cadena("Call SpProveedorActualiza('" +
                                                this.Ruc.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.DireccionFiz.ToString() + "','" +
                                            this.DireccionPar.ToString() + "','" +
                                            this.Ciudad.ToString() + "','" +
                                            this.Telefono.ToString() + "','" +
                                            this.Fax.ToString() + "','" +
                                            this.Email.ToString() + "','" +
                                            this.TipoMon1.ToString() + "','" +
                                            this.TipoMon2.ToString() + "','" +
                                            this.NumCuenta1.ToString() + "','" +
                                            this.NumCuenta2.ToString() + "','" +
                                            this.Banco1.ToString() + "','" +
                                            this.Banco2.ToString() + "','" +
                                            this.Contacto.ToString() + "','" +
                                            this.TelfContacto.ToString() + "','" +
                                            this.Referencia.ToString() + "','" +
                                            this.UsuarioModi.ToString() + "','" +
                                            this.Est.ToString() + "','" +
                                            this.RucEmpresa.ToString() + "')");

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

        public Boolean BuscarProveedor(string vCodProv, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpProveedorBusCod('" + vCodProv.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Ruc = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.DireccionFiz = fila[2].ToString();
                    this.DireccionPar = fila[3].ToString();
                    this.Ciudad = fila[4].ToString();
                    this.Telefono = fila[5].ToString();
                    this.Fax = fila[6].ToString();
                    this.Email = fila[7].ToString();
                    this.TipoMon1 = fila[8].ToString();
                    this.TipoMon2 = fila[9].ToString();
                    this.NumCuenta1 = fila[10].ToString();
                    this.NumCuenta2 = fila[11].ToString();
                    this.Banco1 = fila[12].ToString();
                    this.Banco2 = fila[13].ToString();
                    this.Contacto = fila[14].ToString();
                    this.TelfContacto = fila[15].ToString();
                    this.Referencia = fila[16].ToString();
                    this.Est = fila[17].ToString();

                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Proveedor no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean ValidarProveedor(string vCodProveedor, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpProveedorBusCod('" + vCodProveedor.ToString() + "','" + vRucEmpresa.ToString() + "')");

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
    }
}