using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using TelERP.Persistencia;

namespace TelERP.Modelo.Gestores
{
    class GestorInvoiceItem
    {

        MySQL mySQL;

        public GestorInvoiceItem()
        {
            mySQL = new MySQL();
        }

        public DataTable mostrarDatosAnt(int id)
        {
            DataTable dat = mySQL.readSQL("select ii.idinvoiceitem, s.nombre, s.precio from invoiceitem ii, services s where ii.refinvoice = "+id+" and ii.tipo = 0 and ii.referencia = s.idservice union select ii.idinvoiceitem, a.nombre, a.precio from invoiceitem ii, assignment a where ii.refinvoice = "+id+" and ii.tipo = 1 and ii.referencia = a.idassignment and a.pagado=0 and a.deleted=0");
            dat.Columns["idinvoiceitem"].ColumnName = Strings.HeaderID;
            dat.Columns["nombre"].ColumnName = Strings.HeaderMes;
            dat.Columns["precio"].ColumnName = Strings.HeaderAño;
            return dat;
        }
        
        public DataTable mostrarDatos(int id)
        {
            DataTable dat = mySQL.readSQL("select ii.idinvoiceitem, s.nombre, s.precio from invoiceitem ii, services s where ii.refinvoice = " + id + " and ii.tipo = 0 and ii.referencia = s.idservice union select ii.idinvoiceitem, a.nombre, a.precio from invoiceitem ii, assignment a where ii.refinvoice = " + id + " and ii.tipo = 1 and ii.referencia = a.idassignment and a.pagado=0 and a.deleted=0 union select ii.idinvoiceitem, ('" + Strings.Factura + " ' || i.mes || '/' || i.año), (select ifnull((select sum(a2.precio) from invoiceitem ii2, assignment a2 where ii2.tipo = 1 and ii2.referencia = a2.idassignment and ii2.refinvoice = i.idinvoice),0) + ifnull((select sum(s2.precio) from invoiceitem ii2, services s2 where ii2.tipo = 0 and ii2.referencia = s2.idservice and ii2.refinvoice = i.idinvoice),0)) precio from invoiceitem ii, invoices i where ii.refinvoice = " + id + " and ii.tipo = 2 and ii.referencia = i.idinvoice;");
            dat.Columns["idinvoiceitem"].ColumnName = Strings.HeaderID;
            dat.Columns["nombre"].ColumnName = Strings.HeaderMes;
            dat.Columns["precio"].ColumnName = Strings.HeaderAño;
            return dat;
        }

        public DataTable tablaCompleta()
        {
            return mySQL.readData("invoiceitem");
        }

        public void insertarInvoiceItem(int id, int refinvoice, int tipo, int referencia)
        {
            string sql = "insert into invoiceitem values(" + id + "," + refinvoice + "," + tipo + "," + referencia + ")";
            mySQL.executeQuery(sql);
        }
        
        public void deleteInvoiceItem(int refinvoice, int referencia)
        {
            string sql = "delete from invoiceitem where tipo = 0 and referencia = " + referencia + " and refinvoice = " + refinvoice;
            mySQL.executeQuery(sql);
        }

        public int getLastId()
        {
            int ret = 0;
            string sql = "select max(idinvoiceitem) from invoiceitem";
            string consulta = mySQL.singleData(sql);
            if (consulta.Length > 0) ret = int.Parse(consulta);
            return ret;
        }
    }
}
