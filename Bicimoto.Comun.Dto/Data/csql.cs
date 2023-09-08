using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Bicimoto.Comun.Dto.Data
{
    internal class csql
    {
        public static string cadena_coneccion = "";

        /* Conexion Original SQL
        public static System.Data.SqlClient.SqlConnection coneccion = new SqlConnection();
        public static System.Data.SqlClient.SqlCommand comando;
        public static System.Data.SqlClient.SqlDataAdapter datos; */

        //Conexion Mysql
        public static MySql.Data.MySqlClient.MySqlConnection coneccion = new MySqlConnection();

        public static MySql.Data.MySqlClient.MySqlCommand comando;
        public static MySql.Data.MySqlClient.MySqlDataAdapter datos;

        public static int conectar()
        {
            if (coneccion.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    coneccion.ConnectionString = cadena_coneccion;
                    coneccion.Open();
                    return 0;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error al Conectar a la Base de Datos" + " - " + ex.Message.ToString());
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        public static int desconectar()
        {
            if (coneccion.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    coneccion.Close();
                    return 0;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Hubo problemas al desconctarse de la Base de Datos" + " - " + ex.Message.ToString());
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        public static int estado()
        {
            if (coneccion.State == System.Data.ConnectionState.Open)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int verificar_coneccion()
        {
            if (estado() == 1)
            {
                int intento = conectar();
                if (intento == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }

        public static int comando_cadena(string cadena)
        {
            if (verificar_coneccion() == 0)
            {
                try
                {
                    conectar();
                    comando = new MySqlCommand(cadena, coneccion);
                    comando.ExecuteNonQuery();
                    desconectar();
                    return 0;
                }
                catch (System.Exception ex)
                {
                    desconectar();
                    MessageBox.Show("Error al ejecutar comando: " + cadena + " -> " + ex.GetType().ToString() + " - " + ex.Message.ToString());
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        public static DataSet dataset_cadena(string sql)
        {
            DataSet datas = new DataSet();
            if (verificar_coneccion() == 0)
            {
                try
                {
                    conectar();
                    comando = new MySqlCommand(sql, coneccion);
                    datos = new MySqlDataAdapter(comando);
                    datos.Fill(datas);
                    desconectar();
                }
                catch (System.Exception ex)
                {
                    desconectar();
                    MessageBox.Show("Error al Traer los datos para: " + sql + " -> " + ex.GetType().ToString() + " - " + ex.Message.ToString());
                }
            }
            return datas;
        }

        public static DataSet dataset(string sp, System.Object[] parametros)
        {
            DataSet datas = new DataSet();
            if (verificar_coneccion() == 0)
            {
                try
                {
                    conectar();

                    comando = new MySqlCommand(sp, coneccion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    //System.Data.SqlClient.SqlCommandBuilder.DeriveParameters(comando);
                    MySql.Data.MySqlClient.MySqlCommandBuilder.DeriveParameters(comando);
                    comando.Connection = coneccion;
                    //datos = new System.Data.SqlClient.SqlDataAdapter(comando);
                    datos = new MySql.Data.MySqlClient.MySqlDataAdapter(comando);
                    cargarparametros(datos.SelectCommand, parametros);
                    datos.SelectCommand = comando;
                    datos.Fill(datas);
                    desconectar();
                }
                catch (System.Exception ex)
                {
                    desconectar();
                    MessageBox.Show("Error al Traer los datos para: " + sp + " -> " + ex.GetType().ToString() + " - " + ex.Message.ToString());
                }
            }
            return datas;
        }

        public static DataSet dataset(string sp)
        {
            DataSet datos = dataset_cadena(sp);
            return datos;
        }

        public static int ejecutarcomando(string sp)
        {
            int resul = comando_cadena(sp);
            return resul;
        }

        public static int ejecutarcomando(string sp, System.Object[] parametros)
        {
            if (verificar_coneccion() == 0)
            {
                try
                {
                    conectar();
                    comando = new MySqlCommand(sp, coneccion);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    //System.Data.SqlClient.SqlCommandBuilder.DeriveParameters(comando);
                    MySql.Data.MySqlClient.MySqlCommandBuilder.DeriveParameters(comando);
                    comando.Connection = coneccion;
                    cargarparametros(comando, parametros);
                    comando.ExecuteNonQuery();
                    desconectar();
                    return 0;
                }
                catch (System.Exception ex)
                {
                    desconectar();
                    MessageBox.Show("Error al Ejecutar: " + sp + " -> " + ex.GetType().ToString() + " - " + ex.Message.ToString());
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        public static void cargarparametros(MySql.Data.MySqlClient.MySqlCommand coma, System.Object[] parametros)
        {
            int contador = coma.Parameters.Count;
            for (int i = 1; i < contador; i++)
            {
                //System.Data.SqlClient.SqlParameter P = (System.Data.SqlClient.SqlParameter)coma.Parameters[i];
                MySql.Data.MySqlClient.MySqlParameter P = (MySql.Data.MySqlClient.MySqlParameter)coma.Parameters[i];
                if (i <= parametros.Length)
                {
                    P.Value = parametros[i - 1];
                }
                else
                {
                    P.Value = null;
                }
            }
        }
    }
}