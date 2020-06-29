using TelERP.Modelo.Gestores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelERP.Modelo
{
    class Turn
    {
        private int idturn;
        private string horainicio;
        private string horafin;

        private Gestores.GestorTurns gestor;

        public Turn()
        {
            this.gestor = new GestorTurns();
        }

        public Turn(string horainicio, string horafin)
        {
            this.gestor = new GestorTurns();
            this.idturn = gestor.getLastId() + 1;
            this.horainicio = horainicio;
            this.horafin = horafin;
        }

        /// <summary>
        /// Method that returns the data which will be displayed in the table
        /// Metodo que devuelve los datos que seran mostrados en la tabla
        /// </summary>
        /// <returns>A DataTable with the data --- Un DataTable con los datos</returns>
        public DataTable mostrarDatos()
        {
            return gestor.mostrarDatos();
        }

        /// <summary>
        /// Method that returns all the data 
        /// Metodo que devuelve todos los datos
        /// </summary>
        /// <returns>A DataTable with the data --- Un DataTable con los datos</returns>
        public DataTable tablaCompleta()
        {
            return gestor.tablaCompleta();
        }

        public void updateServicesTurn(int idv, int idn)
        {
            gestor.updateServicesTurn(idv, idn);
        }

        /// <summary>
        /// Method that return the data of a single turn
        /// Metodo que devuelve los datos de un unico turno
        /// </summary>
        /// <param name="id">The id of the turn --- El id del turno</param>
        /// <returns>A DataRow with the turn info, null if it didn't find nothing --- Una DataRow con los datos del turno, null si no encuentra nada</returns>
        public DataRow singleTurn(int id)
        {
            return gestor.infoTurn(id);
        }

        /// <summary>
        /// Method that return the data of a single turn
        /// Metodo que devuelve los datos de un unico turno
        /// </summary>
        /// <param name="id">The id of the turn --- El id del turno</param>
        /// <returns>A DataRow with the turn info, null if it didn't find nothing --- Una DataRow con los datos del turno, null si no encuentra nada</returns>
        public DataRow singleTurn(string horainicio, string horafin)
        {
            return gestor.infoTurn(horainicio, horafin);
        }

        public bool tieneServicio(int id)
        {
            return gestor.tieneServicio(id);
        }

        /// <summary>
        /// Method that insert the current object into de database
        /// Metodo que inserta el objeto actual en la base de datos
        /// </summary>
        public void insertarme()
        {
            DataTable dt = gestor.mostrarDatos();
            _ = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if(dr[0].ToString().Equals(horainicio) && dr[1].ToString().Equals(horafin)) throw new Exception(Strings.MsgTurnoExiste);
            }
            gestor.insertarTurn(idturn, horainicio, horafin);
        }

        public void borrame()
        {
            gestor.borrarTurn(idturn);
        }

        public int Idturn { get => idturn; set => idturn = value; }
        public string Horainicio { get => horainicio; set => horainicio = value; }
        public string Horafin { get => horafin; set => horafin = value; }
    }
}
