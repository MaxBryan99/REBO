using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddTurno : Form
    {
        private string Usuario = FrmLogin.x_login_usuario;
        ClsTurno ObjTurno = new ClsTurno();
        DataSet datos;
        string IdApertura = "";
        string cant = "";

        public FrmAddTurno()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddTurno_Load(object sender, EventArgs e)
        {
            label14.Text = FrmLogin.x_login_usuario;

            //Carga turnos
            DataSet Turno = csql.dataset_cadena("Call SpCargaTurnos()");

            if (Turno.Tables[0].Rows.Count > 0)
            {
                comboBox1.Items.Add("");
                foreach (DataRow fila in Turno.Tables[0].Rows)
                {
                    comboBox1.Items.Add(fila[0].ToString());
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Turno", "Sistema");
                comboBox1.Focus();
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            string IdUsuario = FrmLogin.x_codigo_usuario;
            string IdAper = "";

            DateTime fechaHoy = DateTime.Now;
            string fecha = fechaHoy.ToString("d");
            string fechaAnio = fecha.Substring(6, 4);
            string fechaMes = fecha.Substring(3, 2);
            string fechaDia = fecha.Substring(0, 2);
            string fecActual = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

            string idturno = "";
            string turno = comboBox1.SelectedItem.ToString();
            DataSet ds = csql.dataset_cadena("Call SpIdTurno('" + turno + "')");
            idturno = ds.Tables[0].Rows[0][0].ToString();

            ObjTurno.IdCajaApert = idturno.ToString() + fecActual.ToString();
            ObjTurno.IdTurno = idturno;
            ObjTurno.IdUser = IdUsuario;
            ObjTurno.Descripcion = textBox2.Text.ToString();
            ObjTurno.Fecha = fechaAnio.ToString() + "-" + fechaMes.ToString() + "-" + fechaDia.ToString();
            ObjTurno.UserCreacion = Usuario;

            IdAper = idturno.ToString() + fecActual.ToString();
            DataSet datos = csql.dataset("SELECT IdCajaApert FROM tblaperturaturno WHERE IdCajaApert = '" + IdAper + "'");

            if (datos.Tables[0].Rows.Count > 0)
            {
                IdApertura = datos.Tables[0].Rows[0][0].ToString();
                if (IdAper == IdApertura)
                {
                    MessageBox.Show("Turno " + turno + " ya está registrado", "SISTEMA");
                    return;
                }
            }

            datos = csql.dataset("SELECT COUNT(*) AS EstadoActivo FROM tblaperturaturno WHERE Estado = 'P'");
            cant = datos.Tables[0].Rows[0][0].ToString();
            if (int.Parse(cant) >= 1)
            {
                MessageBox.Show("No pueden aperturarce dos turnos al mismo tiempo", "SISTEMA");
                return;
            }

            if (ObjTurno.crear())
            {
                MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                textBox2.Clear();
                DTP1.Focus();
            }
            else
            {
                MessageBox.Show("No se registro correctamente el registro de turno", "SISTEMA");
                return;
            }
        }
    }
}
