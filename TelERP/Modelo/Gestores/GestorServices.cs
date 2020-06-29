using TelERP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelERP.Modelo.Gestores
{
    class GestorServices
    {

        MySQL mySQL;

        public GestorServices()
        {
            mySQL = new MySQL();
        }

        public DataTable mostrarDatos()
        {
            DataTable dat = mySQL.readDataExiste("services");
            DataColumn c = dat.Columns["dias"];
            dat.Columns.Remove(c);
            c = dat.Columns["refturn"];
            dat.Columns.Remove(c);
            c = dat.Columns["descripcion"];
            dat.Columns.Remove(c);
            dat.Columns["idservice"].ColumnName = Strings.HeaderID;
            dat.Columns["nombre"].ColumnName = Strings.HeaderNombre;
            dat.Columns["precio"].ColumnName = Strings.HeaderPrecio;
            return dat;
        }

        public DataTable tablaCompleta()
        {
            return mySQL.readData("services");
        }

        public DataRow infoService(int id)
        {
            return mySQL.readRow("services", "idservice = " + id);
        }

        public void insertarService(int id, string name, decimal precio, int dias, int refturn, string descripcion)
        {
            string sql = "insert into services values("+id+",'"+name+"',"+precio+","+dias+","+refturn+",'"+descripcion+"',0)";
            mySQL.executeQuery(sql);
        }

        public void actualizaService(int id, string name, decimal precio, int dias, int refturn, string descripcion)
        {
            string sql = "update services set nombre = '" + name + "', precio = " + precio + ", dias = " + dias + ", refturn = " + refturn + ", descripcion = '" + descripcion + "' where idservice = " + id;
            mySQL.executeQuery(sql);
        }

        public void borrarService(int id)
        {
            string sql = "update services set deleted = 1 where idservice = " + id;
            mySQL.executeQuery(sql);
            sql = "delete from userservice where refservice = " + id;
            mySQL.executeQuery(sql);
        }

        public int getLastId()
        {
            int ret = 0;
            string sql = "select max(idservice) from services";
            string consulta = mySQL.singleData(sql);
            if (consulta.Length > 0) ret = int.Parse(consulta);
            return ret;
        }
    }
}
