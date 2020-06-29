using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using TelERP.Modelo.Gestores;

namespace TelERP.Modelo
{
    class InvoiceItem
    {

        private int idinvoiceitem;
        private int refinvoice;
        private int tipo;//0=servicio || 1=trabajo
        private int referencia;

        private GestorInvoiceItem gestor;

        public InvoiceItem()
        {
            this.gestor = new GestorInvoiceItem();
        }

        public InvoiceItem(int refinvoice, int tipo, int referencia)
        {
            this.gestor = new GestorInvoiceItem();
            this.idinvoiceitem = gestor.getLastId() + 1;
            this.refinvoice = refinvoice;
            this.tipo = tipo;
            this.referencia = referencia;
        }

        /// <summary>
        /// Method that returns the data which will be displayed in the table
        /// Metodo que devuelve los datos que seran mostrados en la tabla
        /// </summary>
        /// <returns>A DataTable with the data --- Un DataTable con los datos</returns>
        public DataTable mostrarDatos(int id)
        {
            return gestor.mostrarDatos(id);
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
            gestor.insertarInvoiceItem(idinvoiceitem, refinvoice, tipo, referencia);
        }

        public void deleteInvoiceItem(int refinvoice, int referencia)
        {
            gestor.deleteInvoiceItem(refinvoice, referencia);
        }

        public int Idinvoiceitem { get => idinvoiceitem; set => idinvoiceitem = value; }
        public int Refinvoice { get => refinvoice; set => refinvoice = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        public int Referencia { get => referencia; set => referencia = value; }
    }
}
