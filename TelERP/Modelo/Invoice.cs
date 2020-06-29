using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelERP.Modelo.Gestores;

namespace TelERP.Modelo
{
    class Invoice
    {
        private int idinvoice;
        private int refuser;
        private int mes;
        private int año;
        private int pagada;

        private GestorInvoices gestor;

        public Invoice()
        {
            this.gestor = new GestorInvoices();
        }

        public Invoice(int refuser, int mes, int año, int pagada)
        {
            this.gestor = new GestorInvoices();
            this.idinvoice = gestor.getLastId() + 1;
            this.refuser = refuser;
            this.mes = mes;
            this.año = año;
            this.pagada = pagada;
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
        /// Method that returns the data which will be displayed in the table and isn't paid yet    
        /// Metodo que devuelve los datos que seran mostrados en la tabla y no estan pagadas todavia
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
        /// Method that return the data of a single invoice
        /// Metodo que devuelve los datos de una unica factura
        /// </summary>
        /// <param name="id">The id of the invoice --- El id de la factura</param>
        /// <returns>A DataRow with the invoice info, null if it didn't find nothing --- Una DataRow con los datos de la factura, null si no encuentra nada</returns>
        public DataRow singleInvoice(int id)
        {
            return gestor.infoInvoice(id);
        }

        public DataTable noPagadas(int id)
        {
            return gestor.noPagadas(id);
        }

        public void recoverInvoice(int id)
        {
            this.idinvoice = id;
            DataRow dr = gestor.infoInvoice(id);
            this.Refuser = int.Parse(dr["refuser"].ToString());
            this.Mes = int.Parse(dr["mes"].ToString());
            this.Año = int.Parse(dr["año"].ToString());
            this.Pagada = int.Parse(dr["pagada"].ToString());
        }

        public void recoverInvoice(int iduser, int mes, int año)
        {
            DataRow dr = gestor.infoInvoice(iduser, mes, año);
            if (dr != null)
            {
                this.idinvoice = int.Parse(dr["idinvoice"].ToString());
                this.Refuser = int.Parse(dr["refuser"].ToString());
                this.Mes = int.Parse(dr["mes"].ToString());
                this.Año = int.Parse(dr["año"].ToString());
                this.Pagada = int.Parse(dr["pagada"].ToString());
            }
            else
            {
                this.idinvoice = gestor.getLastId() + 1;
                this.Refuser = iduser;
                this.Mes = mes;
                this.Año = año;
                this.Pagada = 0;

                this.insertarme();
            }
        }

        public int ultimoMes()
        {
            return gestor.ultimoMes();
        }

        /// <summary>
        /// Method that insert the current object into de database
        /// Metodo que inserta el objeto actual en la base de datos
        /// </summary>
        public void insertarme()
        {
            gestor.insertarInvoice(idinvoice, refuser, mes, año, pagada);
        }

        public void marcarPagada(int id)
        {
            gestor.marcarPagada(id);
        }

        internal void revisarCero(int id)
        {
            recoverInvoice(id, DateTime.Now.Month, DateTime.Now.Year);
            InvoiceItem ii = new InvoiceItem();
            DataTable dat = ii.mostrarDatos(idinvoice);
            if (dat.Rows.Count == 0)
            {
                gestor.borrar(idinvoice);
            }
        }

        public int Idinvoice { get => idinvoice; set => idinvoice = value; }
        public int Refuser { get => refuser; set => refuser = value; }
        public int Mes { get => mes; set => mes = value; }
        public int Año { get => año; set => año = value; }
        public int Pagada { get => pagada; set => pagada = value; }

        
    }
}
