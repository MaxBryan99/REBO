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
    public partial class FrmTurnos : Form
    {
        DataSet datos;
        public FrmTurnos()
        {
            InitializeComponent();
        }
        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Id";
            Grid1.Columns[1].HeaderText = "Turno";
            Grid1.Columns[2].HeaderText = "Descripción";
            Grid1.Columns[3].HeaderText = "Usuario";
            Grid1.Columns[4].HeaderText = "Fecha";
            Grid1.Columns[5].HeaderText = "Estado";
            Grid1.Columns[0].Visible = false;
            Grid1.Columns[1].Width = 80;
            Grid1.Columns[2].Width = 220;
            Grid1.Columns[3].Width = 80;
            Grid1.Columns[4].Width = 120;
            Grid1.Columns[5].Width = 65;

            Grid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void Buscarturnos()
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            datos = csql.dataset("Call SpTurnoConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTP1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void DTP2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmAddTurno FrmAddTurno = new FrmAddTurno();
            FrmAddTurno.WindowState = FormWindowState.Normal;
            FrmAddTurno.ShowDialog(this);
        }

        private void FrmTurnos_Load(object sender, EventArgs e)
        {
            Buscarturnos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buscarturnos();
        }
    }
}
