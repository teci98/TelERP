using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TelERP.Modelo.Gestores;

namespace TelERP.Modelo
{
    class Assignment
    {
        private int idassignment;
        private string nombre;
        private int refuser;
        private string fecha;
        private decimal precio;
        private string descripcion;

        private GestorAssignment gestor;

        public Assignment()
        {
            this.gestor = new GestorAssignment();
        }

        public Assignment(string nombre, int refuser, string fecha, decimal precio, string descripcion)
        {
            this.gestor = new GestorAssignment();
            this.idassignment = gestor.getLastId() + 1;
            this.nombre = nombre;
            this.refuser = refuser;
            this.fecha = fecha;
            this.precio = precio;
            this.descripcion = descripcion;
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
        /// Method that returns the data which will be displayed in the table and the date is after now
        /// Metodo que devuelve los datos que seran mostrados en la tabla y la fecha es posterior a ahora
        /// </summary>
        /// <returns>A DataTable with the data --- Un DataTable con los datos</returns>
        public DataTable mostrarDatosPendientes()
        {
            return gestor.mostrarDatosPendientes();
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

        /// <summary>
        /// Method that insert the current object into de database
        /// Metodo que inserta el objeto actual en la base de datos
        /// </summary>
        public void insertarme()
        {
            gestor.insertarAssignment(idassignment, nombre, refuser, fecha, precio, descripcion);
        }

        /// <summary>
        /// Method that return the data of a single user
        /// Metodo que devuelve los datos de un unico usuario
        /// </summary>
        /// <param name="id">The id of the user --- El id del usuario</param>
        /// <returns>A DataRow with the user info, null if it didn't find nothing --- Una DataRow con los datos del usuario, null si no encuentra nada</returns>
        public DataRow singleAssignment(int id)
        {
            return gestor.infoAssignment(id);
        }

        /// <summary>
        /// Method that fill all the data of a specific assignment into the object
        /// Metodo que llena todos los datos de un trabajo especifico dentro del objeto
        /// </summary>
        /// <param name="id">The id of the assignment --- El id del trabajo</param>
        public void recoverAssignment(int id)
        {
            this.idassignment = id;
            DataRow dr = gestor.infoAssignment(id);
            this.nombre = dr["nombre"].ToString();
            this.precio = decimal.Parse(dr["precio"].ToString());
            this.refuser = int.Parse(dr["refuser"].ToString());
            this.fecha = dr["fecha"].ToString();
            this.descripcion = dr["descripcion"].ToString();
        }

        public void actualizame()
        {
            gestor.actualizaAssignment(idassignment, nombre, precio, refuser, fecha, descripcion);
        }

        public void pagar(int id)
        {
            gestor.pagarAssignment(id);
        }

        public void borrar(int id)
        {
            gestor.borrarAssignment(id);
        }

        public int Idassignment { get => idassignment; set => idassignment = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Refuser { get => refuser; set => refuser = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
