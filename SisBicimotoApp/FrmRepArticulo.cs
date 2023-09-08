using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisBicimotoApp.Lib;
using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;

namespace SisBicimotoApp
{
    public partial class FrmRepArticulo : Form
    {
        string rucEmpresa = FrmLogin.x_RucEmpresa;
        string razonEmpresa = FrmLogin.x_NomEmpresa;
        string nomAlmacen = FrmLogin.x_NomAlmacen;
        string codAlmacen = FrmLogin.x_CodAlmacen;

        ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        public FrmRepArticulo()
        {
            InitializeComponent();
        }

        private void FrmRepArticulo_Load(object sender, EventArgs e)
        {
            string tipPresDoc = "1";
            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "CodAlmacen";
            comboBox1.DataSource = datosAlm.Tables[0];
            comboBox1.Text = nomAlmacen.ToString();

            //Carga Familia
            string codCatFam = "008";
            DataSet datosFam = csql.dataset_cadena("Call SpCargarDetCat('" + codCatFam + "','" + tipPresDoc + "')");
            if (datosFam.Tables[0].Rows.Count > 0)
            {
                comboBox2.Items.Add("");
                foreach (DataRow fila in datosFam.Tables[0].Rows)
                {
                    comboBox2.Items.Add(fila[1].ToString());
                }
            }

            //Carga turnos
            DataSet Turno = csql.dataset_cadena("Call SpCargaTurnos()");

            if (Turno.Tables[0].Rows.Count > 0)
            {
                comboBox6.Items.Add("");
                foreach (DataRow fila in Turno.Tables[0].Rows)
                {
                    comboBox6.Items.Add(fila[0].ToString());
                }
            }

            //Carga Marca
            string codCatMarca = "010";
            DataSet datosMarca = csql.dataset_cadena("Call SpCargarDetCat('" + codCatMarca + "','" + tipPresDoc + "')");

            if (datosMarca.Tables[0].Rows.Count > 0)
            {
                comboBox4.Items.Add("");
                foreach (DataRow fila in datosMarca.Tables[0].Rows)
                {
                    comboBox4.Items.Add(fila[1].ToString());
                }
            }

            //Carga Unidad
            string codCatUnid = "013";
            DataSet datosUnd = csql.dataset_cadena("Call SpCargarDetCat('" + codCatUnid + "','" + tipPresDoc + "')");

            if (datosUnd.Tables[0].Rows.Count > 0)
            {
                comboBox5.Items.Add("");
                foreach (DataRow fila in datosUnd.Tables[0].Rows)
                {
                    comboBox5.Items.Add(fila[1].ToString());
                }
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Carga Linea
            string vParam = "1";
            ObjDetCatalogo.BuscarDetCatalogoDes("008", comboBox2.Text, vParam);
            string codFamilia = comboBox2.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            DataSet datosLin = csql.dataset("Call SpLineaBusFam('" + codFamilia.ToString() + "','" + rucEmpresa.ToString() + "')");
            comboBox3.DisplayMember = "Descripcion";
            comboBox3.ValueMember = "Codigo";
            comboBox3.DataSource = datosLin.Tables[0];
            comboBox3.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlConsulta;
            string sqlConsulta2;
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            CrystalDecisions.CrystalReports.Engine.ReportDocument reporte = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            if (checkBox1.Checked == true)
            {
                if (checkBox3.Checked == true)
                {
                    reporte.Load(@"Reportes\RptArticulosPrecioCosto.rpt");
                } else
                {
                    reporte.Load(@"Reportes\RptArticulosPrecio.rpt");
                }

            } else
            {
                if (checkBox4.Checked == true)
                {
                    reporte.Load(@"Reportes\RptArticulosStock.rpt");
                }
                if (checkBox2.Checked == true)
                {
                    reporte.Load(@"Reportes\RptArticulosCant.rpt");
                }

            }


    sqlConsulta = "select art.CodArt,art.Nombre,art.CodInternac," +
                           "(select Descripcion from tbldetcatalogo where CodCatalogo = '012' and CodDetCat = art.TipProducto) TipProducto," +
                           "(select Descripcion from tbldetcatalogo where CodCatalogo = '010' and CodDetCat = art.CodMarca) Marca," +
                           "(select Descripcion from tbldetcatalogo where CodCatalogo = '011' and CodDetCat = art.CodModelo) Modelo," +
                           "(Select Descripcion from tbldetcatalogo where CodCatalogo = '008' and CodDetCat = art.CodFamilia) Familia," +
                           "(select lin.Descripcion from tbllinea lin where lin.Codigo = art.CodLinea and lin.CodFamilia = art.CodFamilia) Linea," +
                           "(select DescCorta from tbldetcatalogo where CodCatalogo = '009' and CodDetCat = art.CodProced) Proced," +
                           "(select Descripcion from tbldetcatalogo where CodCatalogo = '013' and CodDetCat = art.CodUnidad) CodUnidad," +
                           "(select Descripcion from tbldetcatalogo where CodCatalogo = '001' and CodDetCat = art.TipMoneda) TipMoneda," +
                           "art.GenStock, art.PCosto, art.PVenta, art.PMayorista, art.PVolumen," +
                           "art.StockMin," +
                           "art.Ubicacion, art.Proveedor, art.TSerie, art.IsVehiculo, art.Descricpcion," +
                           "art.Est, art.RucEmpresa, art.Almacen, st.Stock " +
                           "FROM tblarticulos art, tblstarticulo st WHERE st.CodArti = art.CodArt and st.Ruc = art.RucEmpresa and st.CodAlm = art.Almacen and art.est = 'A'";

            sqlConsulta2 = "select dv.Codigo as CodArt, ar.Nombre,ve.Fecha, sum(dv.Cantidad) as TotalVendido, " +
                            "(Select Descripcion from tbldetcatalogo where CodCatalogo = '008' and CodDetCat = ar.CodFamilia) Familia, " +
                            "(select lin.Descripcion from tbllinea lin where lin.Codigo = ar.CodLinea and lin.CodFamilia = ar.CodFamilia) Linea, " +
                            "(select Turno from tblturno t, tblaperturaturno ap where t.IdTurno = ap.IdTurno and ap.IdCajaApert = ve.IdCajaApert) Turno " +
                            "from tblventa ve  " +
                            "INNER JOIN tbldetventa dv on dv.IdVenta = ve.IdVenta " +
                            "inner join tblarticulos ar on ar.CodArt = dv.Codigo "+
                            "inner join tblaperturaturno tur on tur.IdCajaApert = ve.IdCajaApert " +
                            "inner join tblturno t on t.IdTurno = tur.IdTurno " +
                            "where ve.Est not IN('E', 'N') and dv.Codigo = CodArt " +
                            "and CONCAT(ve.Fecha, ' ',SUBSTR(ve.FecCreacion, 12, 8)) BETWEEN CONCAT(SUBSTR('"+ vFecha1.ToString() + "',7,4),'-',SUBSTR('" + vFecha1.ToString() + "', 4, 2),'-',SUBSTR('" + vFecha1.ToString() + "', 1, 2),' 00:00:00') " +
                            "and CONCAT(SUBSTR('" + vFecha2.ToString() + "',7,4),'-',SUBSTR('" + vFecha2.ToString() + "', 4, 2),'-',SUBSTR('" + vFecha2.ToString() + "', 1, 2),' 23:59:59') " +
		                    "and ve.Almacen = '001' and ve.Empresa = '20610355154'";

            if (!comboBox2.Text.Equals(""))
            {
                string vParam = "1";
                ObjDetCatalogo.BuscarDetCatalogoDes("008", comboBox2.Text, vParam);
                string codFamilia = comboBox2.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();

                sqlConsulta = sqlConsulta + " and art.CodFamilia = '" + codFamilia + "'";
                sqlConsulta2 = sqlConsulta2 + " and ar.CodFamilia = '" + codFamilia + "'";
            }

            if (!comboBox3.Text.Equals(""))
            {
                sqlConsulta = sqlConsulta + " and art.CodLinea = '" + comboBox3.SelectedValue.ToString() + "'";
                sqlConsulta2 = sqlConsulta2 + " and ar.CodLinea = '" + comboBox3.SelectedValue.ToString() + "'";
            }

            if (!comboBox4.Text.Equals(""))
            {
                string vParam = "1";
                ObjDetCatalogo.BuscarDetCatalogoDes("010", comboBox4.Text, vParam);
                string codMarca = comboBox4.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();

                sqlConsulta = sqlConsulta + " and art.CodMarca = '" + codMarca + "'";
            }

            if (!comboBox5.Text.Equals(""))
            {
                string vParam = "1";
                ObjDetCatalogo.BuscarDetCatalogoDes("013", comboBox5.Text, vParam);
                string codUnidad = comboBox5.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();

                sqlConsulta = sqlConsulta + " and art.CodUnidad = '" + codUnidad + "' ";
            }

            if (!comboBox6.Text.Equals(""))
            {
                string Turno = comboBox6.Text.ToString();
                sqlConsulta2 = sqlConsulta2 + " and t.Turno  = '"+ Turno + "' and tur.Estado = 'C' ";
            }

            if (checkBox2.Checked == true)
            {
                sqlConsulta2 = sqlConsulta2 + "group by ar.Nombre order by TotalVendido desc limit 100 ";

                DataSet datos2 = csql.dataset_cadena(sqlConsulta2);

                reporte.SetDataSource(datos2.Tables[0]);

                reporte.SetParameterValue("RucEmpresa", rucEmpresa.ToString().Trim());
                reporte.SetParameterValue("RazonEmpresa", razonEmpresa.ToString().Trim());
                reporte.SetParameterValue("Almacen", comboBox1.Text.ToString().Trim());
                reporte.SetParameterValue("Fecha Desde", vFecha1);
                reporte.SetParameterValue("Fecha Hasta", vFecha2);
                reporte.SetParameterValue("Turno", comboBox6.Text.ToString().Trim());
            }
            else
            {
                string vAlmacen = comboBox1.SelectedValue.ToString();

                sqlConsulta = sqlConsulta + " and art.RucEmpresa = " + rucEmpresa.ToString().Trim() + " and art.Almacen = " + vAlmacen.ToString().Trim() + " order by art.Nombre";

                DataSet datos = csql.dataset_cadena(sqlConsulta);

                reporte.SetDataSource(datos.Tables[0]);

                reporte.SetParameterValue("RucEmpresa", rucEmpresa.ToString().Trim());
                reporte.SetParameterValue("RazonEmpresa", razonEmpresa.ToString().Trim());
                reporte.SetParameterValue("Almacen", comboBox1.Text.ToString().Trim());
            }

            FrmRptArticulos frmReporte = new FrmRptArticulos();
            frmReporte.PreparaReporte(reporte);
            frmReporte.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox3.Enabled = true;
                checkBox4.Checked = false;
            }
            else
            {
                checkBox3.Checked = false;
                checkBox3.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                DTP1.Enabled = true;
                DTP2.Enabled = true;
                comboBox6.Enabled = true;
            }
            else
            {
                DTP1.Enabled = false;
                DTP2.Enabled = false;
                comboBox6.Enabled = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
                checkBox3.Enabled = false;
            } else
            {
                
            }
        }
    }
}
