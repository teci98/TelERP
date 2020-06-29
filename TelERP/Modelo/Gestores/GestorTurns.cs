using TelERP.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelERP.Modelo.Gestores
{
    class GestorTurns
    {

        MySQL mySQL;

        public GestorTurns()
        {
            mySQL = new MySQL();
        }

        public DataTable mostrarDatos()
        {
            DataTable dat = mySQL.readData("turns");
            DataColumn c = dat.Columns["idturn"];
            dat.Columns.Remove(c);
            dat.Columns["horainicio"].ColumnName = Strings.HeaderInicio;
            dat.Columns["horafin"].ColumnName = Strings.HeaderFin;
            return dat;
        }

        public DataTable tablaCompleta()
        {
            return mySQL.readData("turns");
        }

        public DataRow infoTurn(int id)
        {
            return mySQL.readRow("turns", "idturn = " + id);
        }
        public DataRow infoTurn(string horainicio, string horafin)
        {
            return mySQL.readRow("turns", "horainicio = '" + horainicio + "' and horafin = '" + horafin + "'");
        }

        public void insertarTurn(int id, string horainicio, string horafin)
        {
            string sql = "insert into turns values("+id+",'"+horainicio+ "','" + horafin + "')";
            mySQL.executeQuery(sql);
        }

        public bool tieneServicio(int id)
        {
            bool ret = false;
            string sql = "select count(*) from services where refturn = " + id;
            string consulta = mySQL.singleData(sql);
            if (int.Parse(consulta) > 0) ret = true;
            return ret;
        }

        public void updateServicesTurn(int idv, int idn)
        {
            string sql = "update services set refturn = " + idn + " where refturn = " + idv;
            mySQL.executeQuery(sql);
        }

        public void borrarTurn(int id)
        {
            string sql = "delete from turns where idturn = " + id;
            mySQL.executeQuery(sql);
        }

        public int getLastId()
        {
            int ret = 0;
            string sql = "select max(idturn) from turns";
            string consulta = mySQL.singleData(sql);
            if (consulta.Length > 0) ret = int.Parse(consulta);
            return ret;
        }

    }
}
