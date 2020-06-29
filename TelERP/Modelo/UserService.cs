using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelERP.Modelo.Gestores;

namespace TelERP.Modelo
{
    class UserService
    {
        private int iduserservice;
        private string fechainicio;
        private int refuser;
        private int refservice;

        private GestorUserService gestor;

        public UserService()
        {
            this.gestor = new GestorUserService();
        }

        public UserService(string fechainicio, int refuser, int refservice)
        {
            this.gestor = new GestorUserService();
            this.iduserservice = gestor.getLastId()+1;
            this.fechainicio = fechainicio;
            this.refuser = refuser;
            this.refservice = refservice;
        }

        /// <summary>
        /// Method that returns the data which will be displayed in the table
        /// Metodo que devuelve los datos que seran mostrados en la tabla
        /// </summary>
        /// /// <param name="id">The id of the user --- El id del usuario</param>
        /// <returns>A DataTable with the data --- Un DataTable con los datos</returns>
        public DataTable mostrarDatos(int id)
        {
            return gestor.mostrarDatosPosteriores(id);
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

        public DataTable datosMensaje(int id)
        {
            return gestor.datosMensaje(id);
        }

        /// <summary>
        /// Method that insert the current object into de database
        /// Metodo que inserta el objeto actual en la base de datos
        /// </summary>
        public void insertarme()
        {
            gestor.insertarUserService(iduserservice, fechainicio, refuser, refservice);
        }

        public void borrar(int refuser, int refservice)
        {
            gestor.borrarUserService(refuser, refservice);
        }

        public int Iduserservice { get => iduserservice; set => iduserservice = value; }
        public string Fechainicio { get => fechainicio; set => fechainicio = value; }
        public int Refuser { get => refuser; set => refuser = value; }
        public int Refservice { get => refservice; set => refservice = value; }
    }
}
