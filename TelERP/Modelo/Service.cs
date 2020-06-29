using TelERP.Modelo.Gestores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelERP.Modelo
{
    class Service
    {
        private int idservice;
        private string nombre;
        private decimal precio;
        private int dias;
        private int refturn;
        private string descripcion;

        private GestorServices gestor;

        public Service()
        {
            this.gestor = new GestorServices();
        }

        public Service(string nombre, decimal precio, int dias, int refturn, string descripcion)
        {
            this.gestor = new GestorServices();
            this.idservice = gestor.getLastId()+1;
            this.nombre = nombre;
            this.precio = precio;
            this.dias = dias;
            this.refturn = refturn;
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
        /// Method that returns all the data 
        /// Metodo que devuelve todos los datos
        /// </summary>
        /// <returns>A DataTable with the data --- Un DataTable con los datos</returns>
        public DataTable tablaCompleta()
        {
            return gestor.tablaCompleta();
        }

        /// <summary>
        /// Method that return the data of a single service
        /// Metodo que devuelve los datos de un unico servicio
        /// </summary>
        /// <param name="id">The id of the service --- El id del servicio</param>
        /// <returns>A DataRow with the service info, null if it didn't find nothing --- Una DataRow con los datos del servicio, null si no encuentra nada</returns>
        public DataRow singleService(int id)
        {
            return gestor.infoService(id);
        }

        /// <summary>
        /// Method that fill all the data of a specific service into the object
        /// Metodo que llena todos los datos de un servicio especifico dentro del objeto
        /// </summary>
        /// <param name="id">The id of the service --- El id del servicio</param>
        public void recoverService(int id)
        {
            this.idservice = id;
            DataRow dr = gestor.infoService(id);
            this.nombre = dr["nombre"].ToString();
            this.precio = decimal.Parse(dr["precio"].ToString());
            this.dias = int.Parse(dr["dias"].ToString());
            this.refturn = int.Parse(dr["refturn"].ToString());
            this.descripcion = dr["descripcion"].ToString();
        }

        /// <summary>
        /// Method that insert the current object into de database
        /// Metodo que inserta el objeto actual en la base de datos
        /// </summary>
        public void insertarme()
        {
            gestor.insertarService(idservice, nombre, precio, dias, refturn, descripcion);
        }


        public void actualizame()
        {
            gestor.actualizaService(idservice, nombre, precio, dias, refturn, descripcion);
        }

        public void borrame()
        {
            gestor.borrarService(idservice);
        }

        public int Idservice { get => idservice; set => idservice = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public int Dias { get => dias; set => dias = value; }
        public int Refturn { get => refturn; set => refturn = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
