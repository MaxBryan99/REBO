using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsCliente
    {
        public string RucDni;
        public string TipDoc;
        public string Nombre;
        public string Direccion;
        public string Telefono;
        public string Celular;
        public string Contacto;
        public string Email;
        public string DireccionEnvio;
        public string Region;
        public string Provincia;
        public string Distrito;
        public double LimCredito;
        public string CodVendedor;
        public string UsuarioCrea;
        public string UsuarioModi;
        public string Est;
        public string RucEmpresa;
        public string FecCreacion;
        public string UserCreacion;
        public string FecModi;
        public string UserModi;
        public int ValorCli;
        public string TipDocAnt;
        public string rucdniAnt;

        public ClsCliente()
        {
        }

        public ClsCliente(string RucDni, string TipDoc, string Nombre, string Direccion, string Telefono, string Celular,
                            string Contacto, string Email, string DireccionEnvio, string Region, string Provincia,
                            string Distrito, double LimCredito, string CodVendedor, string UsuarioCrea, string UsuarioModi,
                            string Est, string RucEmpresa, string FecCreacion, string UserCreacion, string FecModi, string UserModi, int ValorCli, string TipoDocAnt,
                            string rucdniAnt)
        {
            this.RucDni = RucDni;
            this.TipDoc = TipDoc;
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.Celular = Celular;
            this.Contacto = Contacto;
            this.Email = Email;
            this.DireccionEnvio = DireccionEnvio;
            this.Region = Region;
            this.Provincia = Provincia;
            this.Distrito = Distrito;
            this.LimCredito = LimCredito;
            this.CodVendedor = CodVendedor;
            this.UsuarioCrea = UsuarioCrea;
            this.UsuarioModi = UsuarioModi;
            this.Est = Est;
            this.RucEmpresa = RucEmpresa;
            this.ValorCli = ValorCli;
            this.FecCreacion = FecCreacion;
            this.UserCreacion = UserCreacion;
            this.FecModi = FecModi;
            this.UserModi = UserModi;
            this.TipDocAnt = TipoDocAnt;
            this.rucdniAnt = rucdniAnt;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpClienteCrear('" +
                                            this.RucDni.ToString() + "','" +
                                            this.TipDoc.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.Direccion.ToString() + "','" +
                                            this.Telefono.ToString() + "','" +
                                            this.Celular.ToString() + "','" +
                                            this.Contacto.ToString() + "','" +
                                            this.Email.ToString() + "','" +
                                            this.DireccionEnvio.ToString() + "','" +
                                            this.Region.ToString() + "','" +
                                            this.Provincia.ToString() + "','" +
                                            this.Distrito.ToString() + "'," +
                                            this.LimCredito + ",'" +
                                            this.CodVendedor + "','" +
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

            int resultado = csql.comando_cadena("Call SpClienteActualiza('" +
                                                this.RucDni.ToString() + "','" +
                                                this.TipDoc.ToString() + "','" +
                                                this.Nombre.ToString() + "','" +
                                                this.Direccion.ToString() + "','" +
                                                this.Telefono.ToString() + "','" +
                                                this.Celular.ToString() + "','" +
                                                this.Contacto.ToString() + "','" +
                                                this.Email.ToString() + "','" +
                                                this.DireccionEnvio.ToString() + "','" +
                                                this.Region.ToString() + "','" +
                                                this.Provincia.ToString() + "','" +
                                                this.Distrito.ToString() + "'," +
                                                this.LimCredito + ",'" +
                                                this.CodVendedor + "','" +
                                                this.UsuarioModi.ToString() + "','" +
                                                this.Est.ToString() + "','" +
                                                this.RucEmpresa.ToString() + "','" +
                                                this.TipDocAnt.ToString() + "','" +
                                                this.rucdniAnt.ToString() + "')");

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

        /*public Boolean ModificarSunat()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpClienteActualiza_Sunat('" +
                                                this.RucDni.ToString() + "','" +
                                                this.TipDoc.ToString() + "','" +
                                                this.Nombre.ToString() + "','" +
                                                this.Direccion.ToString() + "','" +
                                                this.Celular.ToString() + "','" +
                                                this.RucEmpresa.ToString() + "','" +
                                                this.TipDocAnt.ToString() + "','" +
                                                this.rucdniAnt.ToString() + "')");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }*/

        public Boolean BuscarCLiente(string vCodCliente, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpClienteBusCod('" + vCodCliente.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.RucDni = fila[0].ToString();
                    this.TipDoc = fila[1].ToString();
                    this.Nombre = fila[2].ToString();
                    this.Direccion = fila[3].ToString();
                    this.Telefono = fila[4].ToString();
                    this.Celular = fila[5].ToString();
                    this.Contacto = fila[6].ToString();
                    this.Email = fila[7].ToString();
                    this.DireccionEnvio = fila[8].ToString();
                    this.Region = fila[9].ToString();
                    this.Provincia = fila[10].ToString();
                    this.Distrito = fila[11].ToString();
                    this.LimCredito = double.Parse(fila[12].ToString());
                    this.CodVendedor = fila[13].ToString();
                    this.Est = fila[14].ToString();

                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean ValidarCliente(string vCodCliente, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpClienteBusCod('" + vCodCliente.ToString() + "','" + vRucEmpresa.ToString() + "')");

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

        public Boolean BuscarCLienteNum(string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpClienteBusCodCorr('" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.ValorCli = int.Parse(fila[0].ToString());
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarCLienteData(string vCodCliente, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpClienteBuscarData('" + vCodCliente.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.RucDni = fila[0].ToString();
                    this.TipDoc = fila[1].ToString();
                    this.Nombre = fila[2].ToString();
                    this.Direccion = fila[3].ToString();
                    this.Telefono = fila[4].ToString();
                    this.Celular = fila[5].ToString();
                    this.Contacto = fila[6].ToString();
                    this.Email = fila[7].ToString();
                    this.DireccionEnvio = fila[8].ToString();
                    this.Region = fila[9].ToString();
                    this.Provincia = fila[10].ToString();
                    this.Distrito = fila[11].ToString();
                    this.LimCredito = double.Parse(fila[12].ToString());
                    this.CodVendedor = fila[13].ToString();
                    this.Est = fila[14].ToString();
                    this.FecCreacion = fila[15].ToString();
                    this.UserCreacion = fila[16].ToString();
                    this.FecModi = fila[17].ToString();
                    this.UserModi = fila[18].ToString();
                    this.RucEmpresa = fila[19].ToString();

                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }
    }
}