using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisBicimotoApp.Clases
{
    class ClsTurno
    {
        public string IdCajaApert;
        public string IdTurno;
        public string IdUser;
        public string Descripcion;
        public string UserCreacion;
        public string UserModif;
        public string Fecha;

        public ClsTurno()
        {

        }

        public ClsTurno(string IdCajaApert, string IdTurno, string IdUser, string Descripcion, string UserCreacion, string UserModif, string Fecha)
        {
            this.IdCajaApert = IdCajaApert;
            this.IdTurno = IdTurno;
            this.IdUser = IdUser;
            this.Descripcion = Descripcion;
            this.UserCreacion = UserCreacion;
            this.UserModif = UserModif;
            this.Fecha = Fecha;
        }

        public Boolean crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpTurnoCrear('" + this.IdCajaApert.ToString() + "','" +
                                                                           this.IdTurno.ToString() + "','" +
                                                                           this.IdUser.ToString() + "','" +
                                                                           this.Descripcion + "','" +
                                                                           this.Fecha.ToString() + "','" +
                                                                           this.UserCreacion + "')");

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
