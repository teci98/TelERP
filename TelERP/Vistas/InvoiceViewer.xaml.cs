using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TelERP.Modelo;
using TelERP.Modelo.Gestores;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TLSharp.Core.Utils;

namespace TelERP.Vistas
{
    /// <summary>
    /// Lógica de interacción para InvoiceViewer.xaml
    /// </summary>
    public partial class InvoiceViewer : Window
    {
        Invoice i;
        private int tipo;

        public InvoiceViewer(int id, int n)
        {
            InitializeComponent();
            i = new Invoice();
            i.recoverInvoice(id);
            tipo = n;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            

            User u = new User();

            DataRow dr = u.singleUser(i.Refuser);

            DataTable user = new DataTable();
            user.Columns.Add("iduser",Type.GetType("System.Int32"));
            user.Columns.Add("nombre",Type.GetType("System.String"));
            user.Columns.Add("apellidos",Type.GetType("System.String"));
            user.Columns.Add("email",Type.GetType("System.String"));
            user.Columns.Add("telefono",Type.GetType("System.Int32"));
            DataRow drf = user.NewRow();
            drf[0] = dr[0];
            drf[1] = dr[1];
            drf[2] = dr[2];
            drf[3] = dr[3];
            drf[4] = dr[4];
            user.Rows.Add(drf);


            dr = i.singleInvoice(i.Idinvoice);
            DataTable invo = new DataTable();
            invo.Columns.Add("idinvoice", Type.GetType("System.Int32"));
            invo.Columns.Add("mes", Type.GetType("System.Int32"));
            invo.Columns.Add("año", Type.GetType("System.Int32"));
            drf = invo.NewRow();
            drf[0] = dr[0];
            drf[1] = dr[2];
            drf[2] = dr[3];
            invo.Rows.Add(drf);

            InvoiceItem ii = new InvoiceItem();
            DataTable items = ii.mostrarDatos(i.Idinvoice);
            items.Columns[0].ColumnName = "idinvoiceitem";
            items.Columns[1].ColumnName = "nombre";
            items.Columns[2].ColumnName = "precio";


            DataTable emp = new DataTable();
            emp.Columns.Add("nombre", Type.GetType("System.String"));
            emp.Columns.Add("direccion", Type.GetType("System.String"));
            emp.Columns.Add("telefono", Type.GetType("System.String"));
            emp.Columns.Add("email", Type.GetType("System.String"));
            emp.Columns.Add("cif", Type.GetType("System.String"));
            drf = emp.NewRow();
            drf[0] = Propiedades.getNombreEmpresa();
            drf[1] = Propiedades.getDireccion();
            drf[2] = Propiedades.getTelefono();
            drf[3] = Propiedades.getEmail();
            drf[4] = Propiedades.getCif();
            emp.Rows.Add(drf);

            if(tipo == 0)
            {
                CrystalReport1 report = new CrystalReport1();
                report.Database.Tables["items"].SetDataSource(items);
                report.Database.Tables["usuario"].SetDataSource(user);
                report.Database.Tables["invoice"].SetDataSource(invo);
                report.Database.Tables["empresa"].SetDataSource(emp);
                CReportViewer.ViewerCore.ReportSource = report;
            }
            else
            {
                CrystalReport2 report = new CrystalReport2();
                report.Database.Tables["items"].SetDataSource(items);
                report.Database.Tables["usuario"].SetDataSource(user);
                report.Database.Tables["invoice"].SetDataSource(invo);
                report.Database.Tables["empresa"].SetDataSource(emp);
                CReportViewer.ViewerCore.ReportSource = report;
            }
        }

    }
}
