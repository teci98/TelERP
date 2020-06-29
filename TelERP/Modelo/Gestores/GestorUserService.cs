using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelERP.Persistencia;

namespace TelERP.Modelo.Gestores
{
    class GestorUserService
    {

        MySQL mySQL;

        public GestorUserService()
        {
            mySQL = new MySQL();
        }

        public DataTable mostrarDatos(int id)
        {
            DataTable dat = mySQL.readSQL("select '"+Strings.ServicioSolo+"' "+Strings.Tipo+" ,s.idservice, s.nombre, s.precio from userservice us, services s where us.refuser = " + id + " and us.refservice = s.idservice union select '" + Strings.TrabajoSolo + "' " + Strings.Tipo + " ,a.idassignment, a.nombre, a.precio from assignment a where a.refuser = " + id);
            dat.Columns["idservice"].ColumnName = Strings.HeaderID;
            dat.Columns["nombre"].ColumnName = Strings.HeaderNombre;
            dat.Columns["precio"].ColumnName = Strings.HeaderPrecio;
            return dat;
        }
        
        public DataTable mostrarDatosPosteriores(int id)
        {
            DataTable dat = mySQL.readSQL("select '"+Strings.ServicioSolo+"' "+Strings.Tipo+" ,s.idservice, s.nombre, s.precio, 'fecha' " + Strings.HeaderFecha + " from userservice us, services s where us.refuser = " + id + " and us.refservice = s.idservice union select '" + Strings.TrabajoSolo + "' " + Strings.Tipo + " ,a.idassignment, a.nombre, a.precio, a.fecha from assignment a where a.refuser = " + id);
            dat.Columns["idservice"].ColumnName = Strings.HeaderID;
            dat.Columns["nombre"].ColumnName = Strings.HeaderNombre;
            dat.Columns["precio"].ColumnName = Strings.HeaderPrecio;
            List<DataRow> lista = new List<DataRow>();
            foreach (DataRow dr in dat.Rows)
            {
                if (dr[Strings.Tipo].Equals(Strings.TrabajoSolo))
                {
                    string tmp = dr[Strings.HeaderFecha].ToString();
                    DateTime date = DateTime.Parse(tmp);
                    if (date < DateTime.Now) lista.Add(dr);
                }
                
            }
            foreach (DataRow dr in lista)
            {
                dat.Rows.Remove(dr);
            }
            DataColumn c = dat.Columns[Strings.HeaderFecha];
            dat.Columns.Remove(c);
            return dat;
        }



        public DataTable datosMensaje(int id)
        {
            DataTable dat = mySQL.readSQL("select s.nombre, s.dias, (t.horainicio || ' - ' || t.horafin) turno from userservice us, services s, turns t where us.refuser = " + id + " and us.refservice = s.idservice and s.refturn = t.idturn");
            return dat;
        }

        public DataTable tablaCompleta()
        {
            return mySQL.readData("userservice");
        }

        public void insertarUserService(int id, string fecha, int refuser, int refservice)
        {
            string sql = "insert into userservice values(" + id + ",'" + fecha + "'," + refuser + "," + refservice + ")";
            mySQL.executeQuery(sql);
        }

        public void borrarUserService(int refuser, int refservice)
        {
            string sql = "delete from userservice where refuser = " + refuser + " and refservice = "+refservice;
            mySQL.executeQuery(sql);
        }

        public int getLastId()
        {
            int ret = 0;
            string sql = "select max(iduserservice) from userservice";
            string consulta = mySQL.singleData(sql);
            if (consulta.Length > 0) ret = int.Parse(consulta);
            return ret;
        }
    }
}
