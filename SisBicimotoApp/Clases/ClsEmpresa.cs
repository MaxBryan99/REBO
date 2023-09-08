using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    //Clase que conecta con la tala TblEmpresa
    internal class ClsEmpresa
    {
        //Atributos - Campos de la Tbala TblEmpresa
        public string Ruc;

        public string Razon;
        public string NombreLegal;
        public string Direccion;
        public string Ubicacion;
        public string Telefono;
        public string Telefono2;
        public string Celular;
        public string Predeterminar;
        public string Region;
        public string Provincia;
        public string Distrito;
        public string Ubigeo;
        public string Representante;
        public string Urbanizacion;
        public string Email;
        public string UsuarioCrea;
        public string UsuarioModi;
        public string Est;

        //Constructor de la clase
        public ClsEmpresa()
        {
        }

        //Constructor general de carga de datos en los campos
        public ClsEmpresa(string Ruc, string Razon, string NombreLegal, string Direccion, string Ubicacion, string Telefono, string Telefono2, string Celular,
                            string Predeterminar, string Region, string Provincia, string Distrito, string Ubigeo, string Representante,
                            string Urbanizacion, string Email, string UsuarioCrea, string UsuarioModi, string Est)
        {
            this.Ruc = Ruc;
            this.Razon = Razon;
            this.NombreLegal = NombreLegal;
            this.Direccion = Direccion;
            this.Ubicacion = Ubicacion;
            this.Telefono = Telefono;
            this.Telefono2 = Telefono2;
            this.Celular = Celular;
            this.Predeterminar = Predeterminar;
            this.Region = Region;
            this.Provincia = Provincia;
            this.Distrito = Distrito;
            this.Ubigeo = Ubigeo;
            this.Representante = Representante;
            this.Urbanizacion = Urbanizacion;
            this.Email = Email;
            this.UsuarioCrea = UsuarioCrea;
            this.UsuarioModi = UsuarioModi;
            this.Est = Est;
        }

        //Metodo Buscar Empresa predeterminada
        public Boolean BuscarPredeterminado()
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpEmpresaBusPred()");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Ruc = fila[0].ToString();
                    this.Razon = fila[1].ToString();
                    this.Predeterminar = fila[2].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        //Metodo de buscar empresa por RUC
        public Boolean BuscarRuc(string vRuc)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpEmpresaBusRuc('" + vRuc.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Ruc = fila[0].ToString();
                    this.Razon = fila[1].ToString();
                    this.NombreLegal = fila[2].ToString();
                    this.Direccion = fila[3].ToString();
                    this.Ubicacion = fila[4].ToString();
                    this.Telefono = fila[5].ToString();
                    this.Celular = fila[6].ToString();
                    this.Predeterminar = fila[7].ToString();
                    this.Region = fila[8].ToString();
                    this.Provincia = fila[9].ToString();
                    this.Distrito = fila[10].ToString();
                    this.Ubigeo = fila[11].ToString();
                    this.Representante = fila[12].ToString();
                    this.Urbanizacion = fila[13].ToString();
                    this.Email = fila[14].ToString();
                    this.Telefono2 = fila[15].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        //Metodo para crear un registro (grabar)
        public Boolean Crear()
        {
            Boolean res = false;

            //Llamar al procedimiento almacenado de crear Empresa
            int resultado = csql.comando_cadena("Call SpEmpresaCrear('" +
                                            this.Ruc.ToString() + "','" +
                                            this.Razon.ToString() + "','" +
                                            this.NombreLegal.ToString() + "','" +
                                            this.Direccion.ToString() + "','" +
                                            this.Ubicacion.ToString() + "','" +
                                            this.Telefono.ToString() + "','" +
                                            this.Celular.ToString() + "','" +
                                            this.Region.ToString() + "','" +
                                            this.Provincia.ToString() + "','" +
                                            this.Distrito.ToString() + "','" +
                                            this.Ubigeo.ToString() + "','" +
                                            this.Urbanizacion.ToString() + "','" +
                                            this.Email.ToString() + "','" +
                                            this.Representante.ToString() + "','" +
                                            this.UsuarioCrea.ToString() + "')");

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

            //Llamar al procedimiento almacenado de crear Empresa
            int resultado = csql.comando_cadena("Call SpEmpresaActualiza('" +
                                            this.Ruc.ToString() + "','" +
                                            this.Razon.ToString() + "','" +
                                            this.NombreLegal.ToString() + "','" +
                                            this.Direccion.ToString() + "','" +
                                            this.Ubicacion.ToString() + "','" +
                                            this.Telefono.ToString() + "','" +
                                            this.Celular.ToString() + "','" +
                                            this.Region.ToString() + "','" +
                                            this.Provincia.ToString() + "','" +
                                            this.Distrito.ToString() + "','" +
                                            this.Ubigeo.ToString() + "','" +
                                            this.Urbanizacion.ToString() + "','" +
                                            this.Email.ToString() + "','" +
                                            this.Representante.ToString() + "','" +
                                            this.UsuarioModi.ToString() + "')");

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
    }
}