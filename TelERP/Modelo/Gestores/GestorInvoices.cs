using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using TelERP.Persistencia;
using TeleSharp.TL;

namespace TelERP.Modelo.Gestores
{
    class GestorInvoices
    {

        MySQL mySQL;

        public GestorInvoices()
        {
            mySQL = new MySQL();
        }

        public DataTable mostrarDatos()
        {
            DataTable dat = mySQL.readSQL("select i.idinvoice, (u.nombre || ' ' || u.apellidos) " + Strings.HeaderUsuario+", i.mes, i.año from users u, invoices i where u.iduser = i.refuser");
            dat.Columns["idinvoice"].ColumnName = Strings.HeaderID;
            dat.Columns["mes"].ColumnName = Strings.HeaderMes;
            dat.Columns["año"].ColumnName = Strings.HeaderAño;
            return dat;
        }

        public DataTable mostrarDatosPendientes()
        {
            DataTable dat = mySQL.readSQL("select i.idinvoice, (u.nombre || ' ' || u.apellidos) " + Strings.HeaderUsuario + ", i.mes, i.año from users u, invoices i where u.iduser = i.refuser and pagada = 0");
            dat.Columns["idinvoice"].ColumnName = Strings.HeaderID;
            dat.Columns["mes"].ColumnName = Strings.HeaderMes;
            dat.Columns["año"].ColumnName = Strings.HeaderAño;
            return dat;
        }

        public DataTable noPagadas(int id)
        {
            DataTable dat = mySQL.readSQL("select * from invoices where refuser = " + id + " and pagada = 0");
            dat.Columns["idinvoice"].ColumnName = Strings.HeaderID;
            dat.Columns["mes"].ColumnName = Strings.HeaderMes;
            dat.Columns["año"].ColumnName = Strings.HeaderAño;
            return dat;
        }

        public DataTable tablaCompleta()
        {
            return mySQL.readData("invoices");
        }

        public DataRow infoInvoice(int id)
        {
            return mySQL.readRow("invoices", "idinvoice = " + id);
        }

        public DataRow infoInvoice(int iduser, int mes, int año)
        {
            return mySQL.readRow("invoices", "refuser = " + iduser + " and mes = " + mes + " and año = " + año);
        }

        public void insertarInvoice(int id, int refuser, int mes, int año, int pagada)
        {
            string sql = "insert into invoices values(" + id + "," + refuser + "," + mes + "," + año + "," + pagada + ")";
            mySQL.executeQuery(sql);
        }

        public void marcarPagada(int id)
        {
            string sql = "update invoices set pagada = 1 where idinvoice = " + id;
            mySQL.executeQuery(sql);
            sql = "delete from invoiceitem where tipo = 2 and referencia = " + id;
            mySQL.executeQuery(sql);
            DataTable dat = mySQL.readSQL("select referencia from invoiceitem ii where tipo = 1 and ii.refinvoice = " + id);
            foreach(DataRow dr in dat.Rows)
            {
                sql = "update assignment set pagado = 1 where idassignment = " + dr[0];
                mySQL.executeQuery(sql);
            }
        }

        public int ultimoMes()
        {
            int ret = 0;
            string sql = "select max(mes) from invoices";
            string consulta = mySQL.singleData(sql);
            if (consulta.Length > 0) ret = int.Parse(consulta);
            return ret;
        }

        public int getLastId()
        {
            int ret = DateTime.Now.Year * 100000;
            string sql = "select max(idinvoice) from invoices";
            string consulta = mySQL.singleData(sql);
            if (consulta.Length > 0 && consulta.Substring(0, 4).Equals(DateTime.Now.Year.ToString())) ret = int.Parse(consulta);    
            return ret;
        }

        internal void borrar(int idinvoice)
        {
            string sql = "delete from invoices where idinvoice = " + idinvoice;
            mySQL.executeQuery(sql);
        }
    }
}
