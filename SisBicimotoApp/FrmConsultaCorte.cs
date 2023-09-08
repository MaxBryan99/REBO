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
    public partial class FrmConsultaCorte : Form
    {
        DataSet datos;

        public FrmConsultaCorte()
        {
            InitializeComponent();
        }

        private void BuscarCortes()
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            datos = csql.dataset("Call SpCorteCajaConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
            //SumaTotal();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Fecha";
            Grid1.Columns[1].HeaderText = "Ventas";
            Grid1.Columns[2].HeaderText = "Ingresos Caja";
            Grid1.Columns[3].HeaderText = "Egresos Caja";
            Grid1.Columns[4].HeaderText = "Total Ingresos";
            Grid1.Columns[5].HeaderText = "Total Egresos";
            Grid1.Columns[6].HeaderText = "Total Entregado";
            Grid1.Columns[7].HeaderText = "Total Calculado";
            Grid1.Columns[0].Width = 115;
            Grid1.Columns[1].Width = 62;
            Grid1.Columns[2].Width = 65;
            Grid1.Columns[3].Width = 65;
            Grid1.Columns[4].Width = 65;
            Grid1.Columns[5].Width = 65;
            Grid1.Columns[6].Width = 70;
            Grid1.Columns[7].Width = 70;

            Grid1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Grid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[1].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[2].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[3].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[4].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[5].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[6].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[7].DefaultCellStyle.Format = "###,##0.00";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmConsultaCorte_Load(object sender, EventArgs e)
        {
            BuscarCortes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuscarCortes();
        }
    }
}
