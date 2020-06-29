using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelERP.Persistencia;

namespace TelERP.Modelo.Gestores
{
    class Propiedades
    {
        static MySQL mySQL = new MySQL();

        public static void setNombreEmpresa(string n)
        {
            string sql = "update props set valor = '" + n + "' where nombre = 'nombreempresa'";
            mySQL.executeQuery(sql);
        }

        public static string getNombreEmpresa()
        {
            string sql = "select valor from props where nombre = 'nombreempresa'";
            return mySQL.singleData(sql);
        }

        public static void setCif(string n)
        {
            string sql = "update props set valor = '" + n + "' where nombre = 'cif'";
            mySQL.executeQuery(sql);
        }

        public static string getCif()
        {
            string sql = "select valor from props where nombre = 'cif'";
            return mySQL.singleData(sql);
        }

        public static void setDireccion(string n)
        {
            string sql = "update props set valor = '" + n + "' where nombre = 'direccion'";
            mySQL.executeQuery(sql);
        }

        public static string getDireccion()
        {
            string sql = "select valor from props where nombre = 'direccion'";
            return mySQL.singleData(sql);
        }

        public static void setTelefono(string n)
        {
            string sql = "update props set valor = '" + n + "' where nombre = 'telefono'";
            mySQL.executeQuery(sql);
        }

        public static string getTelefono()
        {
            string sql = "select valor from props where nombre = 'telefono'";
            return mySQL.singleData(sql);
        }

        public static void setEmail(string n)
        {
            string sql = "update props set valor = '" + n + "' where nombre = 'email'";
            mySQL.executeQuery(sql);
        }

        public static string getEmail()
        {
            string sql = "select valor from props where nombre = 'email'";
            return mySQL.singleData(sql);
        }

        public static void setTelegram(string n)
        {
            string sql = "update props set valor = '" + n + "' where nombre = 'telegram'";
            mySQL.executeQuery(sql);
        }

        public static string getTelegram()
        {
            string sql = "select valor from props where nombre = 'telegram'";
            return mySQL.singleData(sql);
        }

        public static void setIdioma(string n)
        {
            string sql = "update props set valor = '" + n + "' where nombre = 'idioma'";
            mySQL.executeQuery(sql);
        }

        public static string getIdioma()
        {
            string sql = "select valor from props where nombre = 'idioma'";
            return mySQL.singleData(sql);
        }
    }
}
