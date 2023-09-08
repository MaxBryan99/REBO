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
    public partial class FrmCorteCalculado : Form
    {
        DataSet datos, datosIng, datosEg;
        double totVenta = 0;
        double totIng = 0;
        double totEg = 0;
        double totCalc = 0;
        double totEntre = 0;
        double totIngresos = 0;
        double totEgresos = 0;
        string IdApertura = "";

        private void FrmCorteCalculado_Load(object sender, EventArgs e)
        {
            label15.Text = FrmCorteCaja.nTotalCaja.ToString("###,##0.00").Trim();
            totEntre = Convert.ToDouble(label15.Text.ToString().Equals("") ? "0" : label15.Text.ToString().Trim());
            //Calculando ventas
            datos = csql.dataset("select IdCajaApert from tblaperturaturno where Estado = 'P'");
            if (datos.Tables[0].Rows.Count > 0)
            {
                IdApertura = datos.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                MessageBox.Show("Aperture otro turno para hacer corte de caja", "SISTEMA");
                return;
            }

            datos = csql.dataset("select IFNULL(sum(ve.Total),0) as TotVenta from tblventa ve where ve.TEst = 'C' AND ve.Est = 'A' AND ve.Cort = 'N' AND ve.IdCajaApert = '" + IdApertura + "'");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    totVenta = Convert.ToDouble(fila[0].ToString());
                }
            }

            label7.Text = totVenta.ToString("###,##0.00").Trim();

            //Calculando Ingresos
            datosIng = csql.dataset("select IFNULL(sum(ve.Monto),0) as TotIngreso from tblmovcaja ve where tipo = 'I' and ve.Cort = 0");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datosIng.Tables[0].Rows)
                {
                    totIng = Convert.ToDouble(fila[0].ToString());
                }
            }
            label8.Text = totIng.ToString("###,##0.00").Trim();

            //Calculando Egresos
            datosEg = csql.dataset("select IFNULL(sum(ve.Monto),0) as TotEgreso from tblmovcaja ve where tipo = 'E' and ve.Cort = 0");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datosEg.Tables[0].Rows)
                {
                    totEg = Convert.ToDouble(fila[0].ToString());
                }
            }
            label13.Text = totEg.ToString("###,##0.00").Trim();

            totIngresos = totVenta + totIng;

            totEgresos = totEg;

            totCalc = totIngresos - totEgresos;

            label17.Text = totIngresos.ToString("###,##0.00").Trim();

            label19.Text = totEgresos.ToString("###,##0.00").Trim();

            label21.Text = totCalc.ToString("###,##0.00").Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (totEntre == 0)
            {
                MessageBox.Show("No existen montos entregados, no se puede realizar corte de caja", "SISTEMA");
                return;
            }

            if (totIngresos == 0 && totEgresos == 0)
            {
                MessageBox.Show("No existen movimientos calculados, no se puede realizar corte de caja", "SISTEMA");
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procederá a procesar la información", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            string nId = "";
            DateTime fechaHoy = DateTime.Now;
            string fecha = fechaHoy.ToString("d");
            string fechaAnio = fecha.Substring(6, 4);
            string fechaMes = fecha.Substring(3, 2);
            string fechaDia = fecha.Substring(0, 2);
            string fecActual = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();
            string hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + DateTime.Now.Second.ToString("D2");

            nId = fecActual + hora;

            int resultado = csql.comando_cadena("Call SpCorteCajaActualiza('" + nId.ToString() + "'," +
                                                                           totVenta + "," +
                                                                           totIng + "," +
                                                                           totEg + "," +
                                                                           totEntre + ",'" + 
                                                                           IdApertura + "')");

            if (resultado > 0)
            {

                MessageBox.Show("Hubo errores, no se grabaron correctamente los datos", "SISTEMA");
                //this.Close();
            }
            else
            {
                MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                this.Close();
            }
        }

        public FrmCorteCalculado()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
