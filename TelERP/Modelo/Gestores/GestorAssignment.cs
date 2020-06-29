using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TelERP.Persistencia;

namespace TelERP.Modelo.Gestores
{
    class GestorAssignment
    {

        MySQL mySQL;

        public GestorAssignment()
        {
            mySQL = new MySQL();
        }

        public DataTable mostrarDatos()
        {
            DataTable dat = mySQL.readSQL("select a.idassignment, a.nombre, (u.nombre || ' ' || u.apellidos) " + Strings.HeaderUsuario + ", a.fecha from assignment a, users u where u.iduser = a.refuser and a.deleted=0");
            dat.Columns["idassignment"].ColumnName = Strings.HeaderID;
            dat.Columns["nombre"].ColumnName = Strings.HeaderNombre;
            dat.Columns["fecha"].ColumnName = Strings.HeaderFecha;
            return dat;
        }

        public DataTable mostrarDatosPendientes()
        {
            DataTable dat = mySQL.readSQL("select a.idassignment, a.nombre, (u.nombre || ' ' || u.apellidos) " + Strings.HeaderUsuario + ", a.fecha from assignment a, users u where u.iduser = a.refuser and a.deleted=0");
            dat.Columns["idassignment"].ColumnName = Strings.HeaderID;
            dat.Columns["nombre"].ColumnName = Strings.HeaderNombre;
            dat.Columns["fecha"].ColumnName = Strings.HeaderFecha;
            List<DataRow> lista = new List<DataRow>();
            foreach(DataRow dr in dat.Rows)
            {
                string tmp = dr[Strings.HeaderFecha].ToString();
                DateTime date = DateTime.Parse(tmp);
                if (date < DateTime.Now) lista.Add(dr);
            }
            foreach(DataRow dr in lista)
            {
                dat.Rows.Remove(dr);
            }
            return dat;
        }

        public DataTable tablaCompleta()
        {
            return mySQL.readData("assignment");
        }

        public DataRow infoAssignment(int id)
        {
            return mySQL.readRow("assignment", "idassignment = " + id);
        }

        public void insertarAssignment(int id, string nombre, int refuser, string fecha, decimal precio, string descripcion)
        {
            string sql = "insert into assignment values(" + id + ",'" + nombre + "'," + refuser + ",'" + fecha + "'," + precio + ",'" + descripcion + "',0,0)";
            mySQL.executeQuery(sql);
        }

        public void actualizaAssignment(int id, string nombre, decimal precio, int refuser, string fecha, string descripcion)
        {
            string sql = "update assignment set nombre = '" + nombre + "', precio = " + precio + ", refuser = " + refuser + ", fecha = '" + fecha + "', descripcion = '" + descripcion + "' where idassignment = " + id;
            mySQL.executeQuery(sql);
        }

        public void borrarAssignment(int id)
        {
            string sql = "update assignment set deleted = 1 where idassignment = " + id;
            mySQL.executeQuery(sql);
        }

        public void pagarAssignment(int id)
        {
            string sql = "update assignment set pagado = 1 where idassignment = " + id;
            mySQL.executeQuery(sql);
        }

        public int getLastId()
        {
            int ret = 0;
            string sql = "select max(idassignment) from assignment";
            string consulta = mySQL.singleData(sql);
            if (consulta.Length > 0) ret = int.Parse(consulta);
            return ret;
        }
    }
}
