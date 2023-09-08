using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsPersonal
    {
        public string Codigo;
        public string Nombre;
        public string Direccion;
        public string Telefono;
        public string Celular;
        public string Email;
        public string TipoDocumento;
        public string NroDocumento;
        public string Profesion;
        public string Cargo;
        public string Area;
        public double SueldoBruto;
        public string Comentario;
        public string FecIngreso;
        public string FecCese;
        public string Est;
        public string UserCreacion;
        public string UserModi;
        public string RucEmpresa;

        public ClsPersonal()
        {
        }

        public ClsPersonal(string Codigo, string Nombre, string Direccion, string Telefono, string Celular,
                            string Email, string TipoDocumento, string NroDocumento, string Profesion, string Cargo,
                            string Area, double SueldoBruto, string Comentario, string FecIngreso, string FecCese,
                            string UserCreacion, string UserModi, string Est, string RucEmpresa)
        {
            this.Codigo = Codigo;
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.Celular = Celular;
            this.Email = Email;
            this.TipoDocumento = TipoDocumento;
            this.NroDocumento = NroDocumento;
            this.Profesion = Profesion;
            this.Cargo = Cargo;
            this.Area = Area;
            this.SueldoBruto = SueldoBruto;
            this.Comentario = Comentario;
            this.FecIngreso = FecIngreso;
            this.FecCese = FecCese;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.Est = Est;
            this.RucEmpresa = RucEmpresa;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpPersonalCrear('" +
                                            this.Nombre.ToString() + "','" +
                                            this.Direccion.ToString() + "','" +
                                            this.Telefono.ToString() + "','" +
                                            this.Celular.ToString() + "','" +
                                            this.Email.ToString() + "','" +
                                            this.TipoDocumento.ToString() + "','" +
                                            this.NroDocumento.ToString() + "','" +
                                            this.Profesion.ToString() + "','" +
                                            this.Cargo.ToString() + "','" +
                                            this.Area.ToString() + "'," +
                                            this.SueldoBruto + ",'" +
                                            this.Comentario + "','" +
                                            this.FecIngreso + "','" +
                                            this.FecCese + "','" +
                                            this.UserCreacion.ToString() + "','" +
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

            int resultado = csql.comando_cadena("Call SpPersonalActualiza('" +
                                                this.Codigo.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.Direccion.ToString() + "','" +
                                            this.Telefono.ToString() + "','" +
                                            this.Celular.ToString() + "','" +
                                            this.Email.ToString() + "','" +
                                            this.TipoDocumento.ToString() + "','" +
                                            this.NroDocumento.ToString() + "','" +
                                            this.Profesion.ToString() + "','" +
                                            this.Cargo.ToString() + "','" +
                                            this.Area.ToString() + "'," +
                                            this.SueldoBruto + ",'" +
                                            this.Comentario + "','" +
                                            this.FecIngreso + "','" +
                                            this.FecCese + "','" +
                                            this.UserModi.ToString() + "','" +
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

        public Boolean BuscarPersonal(string vCodPersonal, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpPersonalBusCod('" + vCodPersonal.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.Direccion = fila[2].ToString();
                    this.Telefono = fila[3].ToString();
                    this.Celular = fila[4].ToString();
                    this.Email = fila[5].ToString();
                    this.TipoDocumento = fila[6].ToString();
                    this.NroDocumento = fila[7].ToString();
                    this.Profesion = fila[8].ToString();
                    this.Cargo = fila[9].ToString();
                    this.Area = fila[10].ToString();
                    this.SueldoBruto = double.Parse(fila[11].ToString().Equals("") ? "0" : fila[11].ToString().ToString().Trim());
                    this.FecIngreso = fila[12].ToString();
                    this.FecCese = fila[13].ToString();
                    this.Comentario = fila[14].ToString();
                    this.Est = fila[15].ToString();

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