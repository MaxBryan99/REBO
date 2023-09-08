using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace SisBicimotoApp.Lib
{
    internal class ImageHelper
    {
        //const string MySqlConnecionString = "Server=localhost; Database=test; Username=root; Password=XXXX;";

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            //MemoryStream ms = new MemoryStream(byteArrayIn);
            //return Image.FromStream(ms);

            if (byteArrayIn == null || byteArrayIn.Length == 0)
            {
                return (null);
            }

            return (Image.FromStream(new MemoryStream(byteArrayIn)));
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ObtenerImagenNoDisponible()
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream file = assembly.GetManifestResourceStream("Img.NoDisponible.jpg");
            return Image.FromStream(file);
        }

        public static Image CargarImagen(string codArti)
        {
            using (MySqlConnection conn = GetNewConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT Image FROM TblArticulos WHERE CodArt = @codarti";
                    cmd.Parameters.AddWithValue("@codarti", codArti);
                    byte[] imgArr = (byte[])cmd.ExecuteScalar();
                    imgArr = (byte[])cmd.ExecuteScalar();
                    using (var stream = new MemoryStream(imgArr))
                    {
                        Image img = Image.FromStream(stream);
                        return img;
                    }
                }
            }
        }

        public static void GuardarImagen(Image imagen, string CodArt, string numRuc)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (imagen == null)
                {
                    //imagen.Save(ms, ImageFormat.Jpeg);
                    // byte[] imgArr = ms.ToArray();
                    using (MySqlConnection conn = GetNewConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "UPDATE tblarticulos set Image = null  where codart = @CodArt and rucempresa = @numruc";
                            cmd.Parameters.AddWithValue("@CodArt", CodArt);
                            cmd.Parameters.AddWithValue("@numruc", numRuc);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    imagen.Save(ms, ImageFormat.Jpeg);
                    byte[] imgArr = ms.ToArray();
                    using (MySqlConnection conn = GetNewConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "UPDATE tblarticulos set Image = @imgArr where codart = @CodArt and rucempresa = @numruc";
                            cmd.Parameters.AddWithValue("@CodArt", CodArt);
                            cmd.Parameters.AddWithValue("@numruc", numRuc);
                            cmd.Parameters.AddWithValue("@imgArr", imgArr);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private static MySqlConnection GetNewConnection()
        {
            //const string MySqlConnecionString = "Server=" + FrmLogin.XServidor + "; Database= " + FrmLogin.XDB + "; Username=" + FrmLogin.XUser + "; Password=" + FrmLogin.XPassword + ";";

            var conn = new MySqlConnection("Server=" + FrmLogin.XServidor + "; Database= " + FrmLogin.XDB + "; Username=" + FrmLogin.XUser + "; Password=" + FrmLogin.XPassword + ";SslMode= " + FrmLogin.x_SslMode + ";");
            conn.Open();
            return conn;
        }

        internal static void GuardarImagen(object image, string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}