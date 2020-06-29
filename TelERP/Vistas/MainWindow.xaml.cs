using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;
using TelERP.Modelo;
using TelERP.Modelo.Gestores;
using TelERP.Persistencia;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TLSharp.Core;
using TLSharp.Core.Utils;

namespace TelERP.Vistas
{
    /// <summary>
    /// Lógica de interacción para MainWindow1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Main variables
        private User u;
        private DataTable dtu;
        private Turn t;
        private Service s;
        private DataTable dts;
        private Assignment a;
        private DataTable dta;
        private Invoice i;
        private DataTable dti;

        //Temporal variables
        private int idusertmp = 0;

        //Telegram variables
        string hash;
        private int apiId = 1091925;
        private string apiHash = "77535d580519358bdf7ee92f481c06b0";
        public static TelegramClient client;
        public string telegram = Propiedades.getTelegram();

        public MainWindow()
        {

            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en");
            //CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Propiedades.getIdioma());
            InitializeComponent();

            //inicializar componentes gaficos iniciales
            gGeneral.Visibility = Visibility.Visible;
            var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0277bd");
            var brush = new SolidColorBrush(color);
            lvihome.Background = brush;

            lblFechaMain.Content = Strings.Fecha + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            
            //inicializar componentes del modelo
            u = new User();
            s = new Service();
            a = new Assignment();
            i = new Invoice();
            t = new Turn();
            dgAssignGeneral.ItemsSource = a.mostrarDatosPendientes().DefaultView;

            iniTelegram();
            revisarFacturas();
            updateUserDeuda();
        }

        /* 
         * METODOS GENERALES DEL MAIN 
         */

        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)listview.SelectedItem;
            ListViewItem l;
            var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#00b0ff");
            var brush = new SolidColorBrush(color);
            foreach (Object o in listview.Items)
            {
                if (o.GetType().Equals(lvi.GetType()))
                {
                    l = (ListViewItem)o;
                    l.Background = brush;
                }

            }
            color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0277bd");
            brush = new SolidColorBrush(color);
            lvi.Background = brush;

            tgbtn.IsChecked = false;

            gGeneral.Visibility = Visibility.Collapsed;
            gUsuarios.Visibility = Visibility.Collapsed;
            gServices.Visibility = Visibility.Collapsed;
            gTrabajos.Visibility = Visibility.Collapsed;
            gInvoices.Visibility = Visibility.Collapsed;
            gAjustes.Visibility = Visibility.Collapsed;

            switch (lvi.Name)
            {
                case "lvihome"://boton home
                    gGeneral.Visibility = Visibility.Visible;
                    updateUserDeuda();
                    dgAssignGeneral.ItemsSource = a.mostrarDatosPendientes().DefaultView;
                    break;
                case "lviusers"://boton usuarios
                    gUsuarios.Visibility = Visibility.Visible;
                    updateDGUsers();
                    break;
                case "lviservices"://boton services
                    gServices.Visibility = Visibility.Visible;
                    updateDGServices();
                    break;
                case "lviassign"://boton trabajos
                    gTrabajos.Visibility = Visibility.Visible;
                    updateDGAssignments();
                    break;
                case "lviinvoices"://boton facturas
                    gInvoices.Visibility = Visibility.Visible;
                    dti = i.mostrarDatos();
                    dgInvoices.ItemsSource = dti.DefaultView;
                    break;
                case "lviajustes"://boton ajustes
                    if (Propiedades.getIdioma().Equals("es")) cbIdioma.SelectedIndex = 0;
                    else if (Propiedades.getIdioma().Equals("en")) cbIdioma.SelectedIndex = 1;
                    else cbIdioma.SelectedIndex = 2;
                    gAjustes.Visibility = Visibility.Visible;
                    cargarEmpresa();
                    dgTurns.ItemsSource = t.mostrarDatos().DefaultView;
                    break;
            }

        }

        private async void iniTelegram()
        {
            if (telegram.Equals("si"))
            {
                try
                {
                    client = new TelegramClient(apiId, apiHash);
                    await client.ConnectAsync();
                }
                catch
                {
                    //CustomMessageBox.Show("Fallo de telegram");
                    Thread.Sleep(100);
                    iniTelegram();
                }
            }
            else
            {
                btnMsgUsers.IsEnabled = false;
                btnEnviarFactura.IsEnabled = false;
                btnEnviarNota.IsEnabled = false;
                CustomMessageBox.Show(Strings.MsgNoTelegramConectado);
            }
        }

        public void updateUserDeuda()
        {
            DataTable dat = u.usersDeuda();
            dat.Columns.Add(Strings.HeaderTotal, Type.GetType("System.Decimal"));
            dat.Columns[Strings.HeaderTotal].DefaultValue = 0;
            dat.Columns[Strings.HeaderTotal].Expression = Strings.HeaderTrabajos + " + " + Strings.HeaderServicios;
            dgDeudasGeneral.ItemsSource = dat.DefaultView;
        }

        private void btnPagarDeudaGeneral_Click(object sender, RoutedEventArgs e)
        {
            ArrayList lista = new ArrayList(dgDeudasGeneral.SelectedItems);
            if (lista.Count > 0)
            {
                SelectionMessageBox.Show(Strings.MsgSeguroDeuda);
                if (SelectionMessageBox.respuesta)
                {
                    foreach (DataRowView tmp in lista)
                    {
                        DataRow drt = tmp.Row;
                        int id = int.Parse(drt[0].ToString());
                        //pagar deuda (marcar los trabajos y las invoices como pagadas)
                        u.pagarDeuda(id);
                    }
                    updateUserDeuda();
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarUsuario);
            }
        }

        private void btnPagarTrabajosGeneral_Click(object sender, RoutedEventArgs e)
        {
            ArrayList lista = new ArrayList(dgDeudasGeneral.SelectedItems);
            if (lista.Count > 0)
            {
                SelectionMessageBox.Show(Strings.MsgSeguroDeuda);
                if (SelectionMessageBox.respuesta)
                {
                    foreach (DataRowView tmp in lista)
                    {
                        DataRow drt = tmp.Row;
                        int id = int.Parse(drt[0].ToString());
                        //pagar deuda (marcar los trabajos como pagados)
                        u.pagarTrabajos(id);
                        i.revisarCero(id);
                    }
                    updateUserDeuda();
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarUsuario);
            }
        }

        /* 
         * METODOS GENERALES DE USUARIOS
         */

        /// <summary>
        /// Method that controls when the item selected in the table users change
        /// Metodo que controla cuando cambiamos el usuario seleccionado en la tabla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = dgUsers.SelectedIndex;
            DataRowView tmp = (DataRowView)dgUsers.SelectedItem;
            if (tmp != null)
            {
                DataRow drt = tmp.Row;
                idusertmp = int.Parse(drt[0].ToString());
                DataRow dr = u.singleUser(idusertmp);
                lblNombreUsers.Content = Strings.Nombre + dr["nombre"];
                lblApellidoUsers.Content = Strings.Apellidos + dr["apellidos"];
                lblEmailUsers.Content = Strings.Email + dr["email"];
                lblPhoneUsers.Content = Strings.Telefono + dr["telefono"];
                BitmapImage im = u.getImage(idusertmp);
                if (im != null) imgUsers.Source = im;
            }

        }
        /// <summary>
        /// Create new user button click
        /// boton de crear nuevo usuario pulsado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            NewModUser nmu = new NewModUser(0);
            nmu.ShowDialog();
            updateDGUsers();
        }

        private void btnInfoUsers_Click(object sender, RoutedEventArgs e)
        {
            if (idusertmp == 0)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarUsuario);
            }
            else
            {
                UserInfo usif = new UserInfo(idusertmp);
                usif.ShowDialog();
            }
        }

        private void dgUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView tmp = (DataRowView)dgUsers.SelectedItem;
            DataRow drt = tmp.Row;
            int id = int.Parse(drt[0].ToString());
            UserInfo usif = new UserInfo(id);
            usif.ShowDialog();
            updateDGUsers();
        }

        private void btnModifyUser_Click(object sender, RoutedEventArgs e)
        {
            int ind = dgUsers.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarUsuario);
            }
            else
            {
                DataRowView tmp = (DataRowView)dgUsers.SelectedItem;
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                NewModUser nmu = new NewModUser(id);
                nmu.ShowDialog();
                updateDGUsers();
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            //tener mucho cuidado al borrar usuarios ya que hay conexiones directas con facturas que deberian mantenerse
            //una posible solucion es crear entradas de registro sin conexion
            int ind = dgUsers.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarUsuario);
            }
            else
            {
                SelectionMessageBox.Show(Strings.MsgBorrarUsuario);
                if (SelectionMessageBox.respuesta)
                {
                    DataRowView tmp = (DataRowView)dgUsers.SelectedItem;
                    DataRow drt = tmp.Row;
                    int id = int.Parse(drt[0].ToString());
                    u.borrar(id);
                    updateDGUsers();
                }
            }
        }

        private void btnMsgUsers_Click(object sender, RoutedEventArgs e)
        {
            int ind = dgUsers.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarUsuario);
            }
            else if (Propiedades.getTelegram().Equals("no")) CustomMessageBox.Show(Strings.MsgTelegramAntes);
            else
            {
                DataRowView tmp = (DataRowView)dgUsers.SelectedItem;
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                MensajeUser mu = new MensajeUser(id);
                mu.ShowDialog();
            }
        }

        private void btnAssignUser_Click(object sender, RoutedEventArgs e)
        {
            if (idusertmp == 0)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarUsuario);
            }
            else
            {
                NewModAssign nma = new NewModAssign(0, idusertmp);
                nma.ShowDialog();
                updateDGUsers();
            }
        }

        private void txtSearchUsers_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length == 0)
            {
                dgUsers.ItemsSource = dtu.DefaultView;
            }
            else
            {
                DataRow[] sel = dtu.Select("(" + Strings.HeaderID + " = '" + txt.Text + "') or (" + Strings.HeaderNombre + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderApellidos + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderEmail + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderTelefono + " LIKE '%" + txt.Text + "%')");
                DataTable dat = dtu.Clone();
                foreach (DataRow row in sel)
                {
                    dat.ImportRow(row);
                }
                dgUsers.ItemsSource = dat.DefaultView;
            }
        }

        private void updateDGUsers()
        {
            dtu = u.mostrarDatos();
            dgUsers.ItemsSource = dtu.DefaultView;
            txtSearchUsers.Text = "";
        }

        /* 
         * METODOS GENERALES DE SERVICIOS
         */

        private void btnNewServices_Click(object sender, RoutedEventArgs e)
        {
            NewModService nms = new NewModService(0);
            nms.ShowDialog();
            updateDGServices();
        }

        private void dgServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = dgServices.SelectedIndex;
            DataRowView tmp = (DataRowView)dgServices.SelectedItem;
            if (tmp != null)
            {
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                DataRow dr = s.singleService(id);
                lblNombreServices.Content = Strings.Servicio + dr["nombre"];
                lblPriceServices.Content = Strings.Precio + dr["precio"];
                lblDaysServices.Content = Strings.Dias + diasSemana(dr["dias"].ToString());
                t = new Turn();
                DataRow tdr = t.singleTurn(int.Parse(dr["refturn"].ToString()));
                lblTurnServices.Content = Strings.Turno + tdr["horainicio"] + " - " + tdr["horafin"];
                txbDescriptionServices.Text = dr["descripcion"].ToString();
            }
        }

        private void btnModifyServices_Click(object sender, RoutedEventArgs e)
        {
            int ind = dgServices.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarServicio);
            }
            else
            {
                DataRowView tmp = (DataRowView)dgServices.SelectedItem;
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                NewModService nms = new NewModService(id);
                nms.ShowDialog();
                updateDGServices();
            }
        }

        private void btnDeleteServices_Click(object sender, RoutedEventArgs e)
        {
            DataRowView tmp = (DataRowView)dgServices.SelectedItem;
            if (tmp != null)
            {
                SelectionMessageBox.Show(Strings.MsgSeguroBorrarServicio);
                if (SelectionMessageBox.respuesta)
                {
                    DataRow drt = tmp.Row;
                    int id = int.Parse(drt[0].ToString());
                    s.recoverService(id);
                    s.borrame();
                    updateDGServices();
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarTrabajo);
            }
        }

        private void txtSearchServices_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length == 0)
            {
                dgServices.ItemsSource = dts.DefaultView;
            }
            else
            {
                DataRow[] sel = dts.Select("(" + Strings.HeaderID + " = '" + txt.Text + "') or (" + Strings.HeaderNombre + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderPrecio + " = '" + txt.Text + "')");
                DataTable dat = dts.Clone();
                foreach (DataRow row in sel)
                {
                    dat.ImportRow(row);
                }
                dgServices.ItemsSource = dat.DefaultView;
            }
        }

        private void updateDGServices()
        {
            dts = s.mostrarDatos();
            dgServices.ItemsSource = dts.DefaultView;
            txtSearchServices.Text = "";
        }

        /* 
         * METODOS GENERALES DE ASSIGNMENTS
         */

        private void dgAssignments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView tmp = (DataRowView)dgAssignments.SelectedItem;
            if (tmp != null)
            {
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                DataRow dr = a.singleAssignment(id);
                lblNombreAssignments.Content = Strings.Nombre + dr["nombre"];
                lblUserAssignments.Content = Strings.Usuario + drt[2];
                lblDeadlineAssignments.Content = Strings.Fecha + dr["fecha"];
                lblPriceAssignments.Content = Strings.Precio + dr["precio"];
                txbDescrptionAssignments.Text = dr["descripcion"].ToString();
            }
        }

        private void btnNewAssignments_Click(object sender, RoutedEventArgs e)
        {
            SelectUserTrabajo sut = new SelectUserTrabajo();
            sut.ShowDialog();
            updateDGAssignments();
        }
        private void btnModifyAssignments_Click(object sender, RoutedEventArgs e)
        {
            DataRowView tmp = (DataRowView)dgAssignments.SelectedItem;
            if (tmp != null)
            {
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                DataRow dr = a.singleAssignment(id);
                NewModAssign nma = new NewModAssign(id, int.Parse(dr["refuser"].ToString()));
                nma.ShowDialog();
                updateDGAssignments();
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarTrabajo);
            }
        }

        private void btnDeleteAssignments_Click(object sender, RoutedEventArgs e)
        {
            DataRowView tmp = (DataRowView)dgAssignments.SelectedItem;
            if (tmp != null)
            {
                SelectionMessageBox.Show(Strings.MsgSeguroBorrarTrabajo);
                if (SelectionMessageBox.respuesta)
                {
                    DataRow drt = tmp.Row;
                    int id = int.Parse(drt[0].ToString());
                    a.borrar(id);
                    updateDGAssignments();
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarTrabajo);
            }
        }

        private void btnPagarAssignments_Click(object sender, RoutedEventArgs e)
        {

            ArrayList lista = new ArrayList(dgAssignments.SelectedItems);
            if (lista.Count > 0)
            {
                SelectionMessageBox.Show(Strings.MsgSeguroPagarTrabajo);
                if (SelectionMessageBox.respuesta)
                {
                    foreach (DataRowView tmp in lista)
                    {
                        DataRow drt = tmp.Row;
                        int id = int.Parse(drt[0].ToString());
                        a.pagar(id);
                    }
                    updateDGAssignments();
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarTrabajo);
            }
        }


        private void txtSearchAssignments_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length == 0)
            {
                dgAssignments.ItemsSource = dta.DefaultView;
            }
            else
            {
                DataRow[] sel = dta.Select("(" + Strings.HeaderID + " = '" + txt.Text + "') or (" + Strings.HeaderNombre + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderPrecio + " = '" + txt.Text + "')");
                DataTable dat = dta.Clone();
                foreach (DataRow row in sel)
                {
                    dat.ImportRow(row);
                }
                dgAssignments.ItemsSource = dat.DefaultView;
            }
        }

        private void updateDGAssignments()
        {
            dta = a.mostrarDatos();
            dgAssignments.ItemsSource = dta.DefaultView;
            txtSearchAssignments.Text = "";
        }

        /* 
        * METODOS GENERALES DE INVOICES
        */

        private void revisarFacturas()
        {
            if (i.ultimoMes() < DateTime.Now.Month)
            {
                Thread t = new Thread(new ThreadStart(generarFacturasTodos));
                t.Start();
            }
        }

        private void generarFacturasTodos()
        {
            int mes = DateTime.Now.Month;
            int año = DateTime.Now.Year;

            DataTable datosUsuarios = u.tablaCompleta();
            foreach (DataRow dr in datosUsuarios.Rows)
            {
                int id = int.Parse(dr[0].ToString());
                UserService ustmp = new UserService();
                DataTable items = ustmp.mostrarDatos(id);
                DataTable nopagadas = i.noPagadas(id);
                if (items.Rows.Count != 0)
                {
                    //generamos la factura
                    i = new Invoice(id, mes, año, 0);
                    i.insertarme();
                    foreach (DataRow item in items.Rows)
                    {
                        int tipo = 1;
                        if (item[Strings.Tipo].ToString().Equals(Strings.ServicioSolo))
                        {
                            tipo = 0;
                        }
                        InvoiceItem ii = new InvoiceItem(i.Idinvoice, tipo, int.Parse(item[Strings.HeaderID].ToString()));
                        if (tipo == 1)
                        {
                            Assignment atmp = new Assignment();
                            atmp.recoverAssignment(int.Parse(item[Strings.HeaderID].ToString()));
                            string[] fecs = atmp.Fecha.Split('/');
                            int mestmp = int.Parse(fecs[1].ToString());
                            if (mestmp == mes) ii.insertarme();

                        }
                        else ii.insertarme();
                    }
                    //hacer la logica que vaya hacia atras con las facturas que no se han pagado todavia
                    //¿meter los elementos de la factura o la referencia a la misma?
                    foreach(DataRow np in nopagadas.Rows)
                    {
                        InvoiceItem ii = new InvoiceItem(i.Idinvoice, 2, int.Parse(np[0].ToString()));
                        ii.insertarme();
                    }

                }
            }
            dti = i.mostrarDatos();
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                dgInvoices.ItemsSource = dti.DefaultView;
            }));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//ver detalles invoices
        {
            DataRowView tmp = (DataRowView)dgInvoices.SelectedItem;
            if (tmp != null)
            {
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                InvoiceViewer iv = new InvoiceViewer(id,0);
                iv.ShowDialog();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//enviar nota
        {
            ArrayList lista = new ArrayList(dgInvoices.SelectedItems);
            if (lista.Count > 0)
            {
                SelectionMessageBox.Show(Strings.MsgSeguroEnviar);
                if (SelectionMessageBox.respuesta)
                {
                    foreach (DataRowView tmp in lista)
                    {
                        DataRow drt = tmp.Row;
                        int id = int.Parse(drt[0].ToString());
                        //i.marcarPagada(id);
                        //enviar por telegram
                        enviarPDF(id, 1);
                    }
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarInvoice);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//marcar factura como pagada
        {
            ArrayList lista = new ArrayList(dgInvoices.SelectedItems);
            if (lista.Count > 0)
            {
                SelectionMessageBox.Show(Strings.MsgSeguroPagar);
                if (SelectionMessageBox.respuesta)
                {
                    foreach (DataRowView tmp in lista)
                    {
                        DataRow drt = tmp.Row;
                        int id = int.Parse(drt[0].ToString());
                        i.marcarPagada(id);
                    }
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarInvoice);
            }
        }


        private void dgInvoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView tmp = (DataRowView)dgInvoices.SelectedItem;
            if (tmp != null)
            {
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                InvoiceViewer iv = new InvoiceViewer(id, 0);
                iv.ShowDialog();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//marcar invoice como pagada y enviar factura
        {
            ArrayList lista = new ArrayList(dgInvoices.SelectedItems);
            if (lista.Count > 0)
            {
                SelectionMessageBox.Show(Strings.MsgSeguroEnviar);
                if (SelectionMessageBox.respuesta)
                {
                    foreach(DataRowView tmp in lista)
                    {
                        DataRow drt = tmp.Row;
                        int id = int.Parse(drt[0].ToString());
                        //i.marcarPagada(id);
                        //enviar por telegram
                        enviarPDF(id, 0);
                    }
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarInvoice);
            }
        }

        private void enviarPDF(int id, int tipo)
        {
            i.recoverInvoice(id);

            DataRow dr = u.singleUser(i.Refuser);
            u.recoverUser(i.Refuser);

            DataTable user = new DataTable();
            user.Columns.Add("iduser", Type.GetType("System.Int32"));
            user.Columns.Add("nombre", Type.GetType("System.String"));
            user.Columns.Add("apellidos", Type.GetType("System.String"));
            user.Columns.Add("email", Type.GetType("System.String"));
            user.Columns.Add("telefono", Type.GetType("System.Int32"));
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

            if (tipo == 0)
            {
                CrystalReport1 report = new CrystalReport1();
                report.Database.Tables["items"].SetDataSource(items);
                report.Database.Tables["usuario"].SetDataSource(user);
                report.Database.Tables["invoice"].SetDataSource(invo);
                report.Database.Tables["empresa"].SetDataSource(emp);
                Stream tmp = report.ExportToStream(ExportFormatType.PortableDocFormat);
                enviarMensaje(tmp);
            }
            else
            {
                CrystalReport2 report = new CrystalReport2();
                report.Database.Tables["items"].SetDataSource(items);
                report.Database.Tables["usuario"].SetDataSource(user);
                report.Database.Tables["invoice"].SetDataSource(invo);
                report.Database.Tables["empresa"].SetDataSource(emp);
                Stream tmp = report.ExportToStream(ExportFormatType.PortableDocFormat);
                enviarMensaje(tmp);
            }

            
        }

        private async void enviarMensaje(Stream tmp)
        {
            if (client.IsUserAuthorized())
            {
                var result = await client.GetContactsAsync();

                var user = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.Phone == "34" + u.Telefono);
                bool enviar = true;
                if (user == null)
                {
                    var phoneContact = new TLInputPhoneContact() { Phone = "34" + u.Telefono, FirstName = u.Nombre, LastName = u.Apellidos };
                    var contacts = new List<TLInputPhoneContact>() { phoneContact };
                    var req = new TLRequestImportContacts() { Contacts = new TLVector<TLInputPhoneContact>(contacts) };
                    var rrr = await client.SendRequestAsync<TLImportedContacts>(req);

                    if (rrr.Imported.Count == 1)
                    {
                        user = result.Users
                            .Where(x => x.GetType() == typeof(TLUser))
                            .Cast<TLUser>()
                            .FirstOrDefault(x => x.Phone == "34" + u.Telefono);
                    }
                    else
                    {
                        enviar = false;
                    }
                }
                if (enviar && user != null)
                {
                    var fileResult = await client.UploadFile(Strings.Factura + i.Mes + "/" + i.Año, new StreamReader(tmp));

                    TLDocumentAttributeFilename name = new TLDocumentAttributeFilename();
                    name.FileName = Strings.Factura + i.Mes + "/" + i.Año + ".pdf";
                    await client.SendUploadedDocument(new TLInputPeerUser() { UserId = user.Id }, fileResult, "", "application/pdf", new TLVector<TLAbsDocumentAttribute>() { name });
                }
            }
        }

        private void cbFacturasNoPagadas_Checked(object sender, RoutedEventArgs e)
        {
            dti = i.mostrarDatosPendientes();
            dgInvoices.ItemsSource = dti.DefaultView;
        }

        private void cbFacturasNoPagadas_Unchecked(object sender, RoutedEventArgs e)
        {
            dti = i.mostrarDatos();
            dgInvoices.ItemsSource = dti.DefaultView;
        }

        private void txtSearchInvoices_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length == 0)
            {
                dgInvoices.ItemsSource = dti.DefaultView;
            }
            else
            {
                DataRow[] sel = dti.Select("(" + Strings.HeaderID + " = '" + txt.Text + "') or (" + Strings.HeaderUsuario + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderAño + " = '" + txt.Text + "') or (" + Strings.HeaderMes + " = '" + txt.Text + "')");
                DataTable dat = dti.Clone();
                foreach (DataRow row in sel)
                {
                    dat.ImportRow(row);
                }
                dgInvoices.ItemsSource = dat.DefaultView;
            }
        }


        /* 
         * METODOS GENERALES DE AJUSTES
         */

        private void btnAñadirTurn_Click(object sender, RoutedEventArgs e)
        {
            insertarTurn();
        }

        private void btnBorrarTurn_Click(object sender, RoutedEventArgs e)
        {
            int ind = dgTurns.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarTurn);
            }
            else
            {
                DataRowView tmp = (DataRowView)dgTurns.SelectedItem;
                DataRow drt = tmp.Row;
                //obtener el turno que esta seleccionado a partir de las horas, preguntar si quiere cambiar los servicios que tengan ese turno y luego borrarlo
                DataRow dr = t.singleTurn(drt[0].ToString(), drt[1].ToString());
                bool esta = t.tieneServicio(int.Parse(dr["idturn"].ToString()));
                if (esta)
                {
                    BorrarTurnConfirmacion btc = new BorrarTurnConfirmacion(int.Parse(dr["idturn"].ToString()));
                    btc.ShowDialog();
                    dgTurns.ItemsSource = t.mostrarDatos().DefaultView;
                }
                else
                {
                    t.Idturn = int.Parse(dr["idturn"].ToString());
                    t.borrame();
                    CustomMessageBox.Show(Strings.MsgTurnoBorrado);
                }

            }
        }

        private void insertarTurn()
        {
            if (validarTime(txtInicioTurn.Text) && validarTime(txtFinTurn.Text))
            {
                t = new Turn(txtInicioTurn.Text, txtFinTurn.Text);
                try
                {
                    t.insertarme();
                    dgTurns.ItemsSource = t.mostrarDatos().DefaultView;
                    txtInicioTurn.Text = "";
                    txtFinTurn.Text = "";
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show(ex.Message);
                }
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgHorasMal);
            }
        }

        private bool validarTime(string time)
        {
            String expresion;
            bool ret = false;
            expresion = "^\\d{2}[:]\\d{2}$";
            if (Regex.IsMatch(time, expresion))
            {
                if (Regex.Replace(time, expresion, String.Empty).Length == 0)
                {

                    string[] str = time.Split(':');
                    try
                    {
                        int hora = int.Parse(str[0]);
                        int min = int.Parse(str[1]);
                        if (hora >= 0 && hora <= 23)
                        {
                            if (min >= 0 && min <= 59)
                            {
                                ret = true;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }


            return ret;
        }

        private void controlHoras_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length == 2 && e.Key != Key.Delete && !txt.Text.Substring(1).Equals(":"))
            {
                txt.AppendText(":");
                txt.Select(3, 3);
            }
            if (txtInicioTurn.Text.Length == 5)
            {
                txtFinTurn.Focus();
            }
            if (e.Key == Key.Enter)
            {
                if (txt.Name.Equals("txtFinTurn"))
                {
                    if (txt.Text.Length == 4)
                    {
                        txt.Text = "0" + txt.Text;
                    }
                    insertarTurn();
                }
                else txtFinTurn.Focus();
            }

        }

        private void controlHoras_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Enter) e.Handled = false;
            else if (e.Key == Key.OemPeriod || e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                if (!txt.Text.Contains(":"))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (txt.Name.Equals("txtInicioTurn") && e.Key == Key.Tab) txtFinTurn.Focus();
                else if (txt.Name.Equals("txtFinTurn") && e.Key == Key.Tab) btnAñadirTurn.Focus();
                else
                {
                    e.Handled = true;
                    CustomMessageBox.Show(Strings.MsgSoloNumerosTurn);
                }

            }
        }

        private void controlHoras_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length == 4)
            {
                txt.Text = "0" + txt.Text;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)//idioma
        {

            if(cbIdioma.SelectedIndex == 0)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es");
                if (!Propiedades.getIdioma().Equals("es"))
                {
                    Propiedades.setIdioma("es");
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    Close();
                }
            }
            else if (cbIdioma.SelectedIndex == 1)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                if (!Propiedades.getIdioma().Equals("en"))
                {
                    Propiedades.setIdioma("en");
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    Close();
                }
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
                if (!Propiedades.getIdioma().Equals("fr"))
                {
                    Propiedades.setIdioma("fr");
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    Close();
                }
            }
        }

        public void cargarEmpresa()
        {
            txtnombreempresa.Text = Propiedades.getNombreEmpresa();
            txtcif.Text = Propiedades.getCif();
            txtdireccion.Text = Propiedades.getDireccion();
            txttelefono.Text = Propiedades.getTelefono();
            txtemail.Text = Propiedades.getEmail();
        }

        private void txtnombreempresa_LostFocus(object sender, RoutedEventArgs e)
        {
            Propiedades.setNombreEmpresa(txtnombreempresa.Text);
        }

        private void txtcif_LostFocus(object sender, RoutedEventArgs e)
        {
            Propiedades.setCif(txtcif.Text);
        }

        private void txtdireccion_LostFocus(object sender, RoutedEventArgs e)
        {
            Propiedades.setDireccion(txtdireccion.Text);
        }

        private void txttelefono_LostFocus(object sender, RoutedEventArgs e)
        {
            Propiedades.setTelefono(txttelefono.Text);
        }

        private void txtemail_LostFocus(object sender, RoutedEventArgs e)
        {
            Propiedades.setEmail(txtemail.Text);
        }

        private void txtTelefonoTelegram_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab || e.Key == Key.Enter) e.Handled = false;
            else
            {
                e.Handled = true;
                CustomMessageBox.Show(Strings.MsgSoloNumerosTelegram);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//para pedir el codigo
        {
            if (txtTelefonoTelegram.Text.Length > 0)
            {
                pillarHash();
                btnconectar.IsEnabled = true;
            }
            else CustomMessageBox.Show(Strings.MsgEscribirTelefono);
        }

        private async void pillarHash()
        {
            hash = await client.SendCodeRequestAsync(34 + txtTelefonoTelegram.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//para conectar telegram
        {
            var user = client.MakeAuthAsync(34 + txtTelefonoTelegram.Text, hash, txtCodigoTelegram.Text);
            if (client.IsUserAuthorized())
            {
                telegram = "si";
                Propiedades.setTelegram("si");
                btnMsgUsers.IsEnabled = true;
                btnEnviarFactura.IsEnabled = true;
                btnEnviarNota.IsEnabled = true;
                iniTelegram();
                CustomMessageBox.Show(Strings.MsgTelegramConectado);
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgTelegramNoConectado);
            }
        }

        /*
         * Metodos de graficos
         */

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tgbtn.IsChecked = false;
        }

        private void tgbtn_Checked(object sender, RoutedEventArgs e)
        {
            gGeneral.Opacity = 0.5;
            gUsuarios.Opacity = 0.5;
            gServices.Opacity = 0.5;
            gTrabajos.Opacity = 0.5;
            gInvoices.Opacity = 0.5;
            gAjustes.Opacity = 0.5;

            tt_desplegar.Visibility = Visibility.Collapsed;
            tt_home.Visibility = Visibility.Collapsed;
            tt_users.Visibility = Visibility.Collapsed;
            tt_service.Visibility = Visibility.Collapsed;
            tt_assign.Visibility = Visibility.Collapsed;
            tt_invoice.Visibility = Visibility.Collapsed;
            tt_settings.Visibility = Visibility.Collapsed;
        }

        private void tgbtn_Unchecked(object sender, RoutedEventArgs e)
        {
            gGeneral.Opacity = 1;
            gUsuarios.Opacity = 1;
            gServices.Opacity = 1;
            gTrabajos.Opacity = 1;
            gInvoices.Opacity = 1;
            gAjustes.Opacity = 1;

            tt_desplegar.Visibility = Visibility.Visible;
            tt_home.Visibility = Visibility.Visible;
            tt_users.Visibility = Visibility.Visible;
            tt_service.Visibility = Visibility.Visible;
            tt_assign.Visibility = Visibility.Visible;
            tt_invoice.Visibility = Visibility.Visible;
            tt_settings.Visibility = Visibility.Visible;
        }

        private void Principal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tgbtn.IsChecked = false;
        }

        public string diasSemana(string valor)
        {
            while (valor.Length < 7)
            {
                valor = "0" + valor;
            }
            string ret = "";
            for (int j = 0; j < valor.Length; j++)
            {
                if (valor[j] == '1')
                {
                    if (ret.Equals(""))
                    {
                        switch (j)
                        {
                            case 0:
                                ret = ret + Strings.LunesCorto;
                                break;
                            case 1:
                                ret = ret + Strings.MartesCorto;
                                break;
                            case 2:
                                ret = ret + Strings.MiercolesCorto;
                                break;
                            case 3:
                                ret = ret + Strings.JuevesCorto;
                                break;
                            case 4:
                                ret = ret + Strings.ViernesCorto;
                                break;
                            case 5:
                                ret = ret + Strings.SabadoCorto;
                                break;
                            case 6:
                                ret = ret + Strings.DomingoCorto;
                                break;
                        }
                    }
                    else
                    {
                        switch (j)
                        {
                            case 0:
                                ret = ret + " - " + Strings.LunesCorto;
                                break;
                            case 1:
                                ret = ret + " - " + Strings.MartesCorto;
                                break;
                            case 2:
                                ret = ret + " - " + Strings.MiercolesCorto;
                                break;
                            case 3:
                                ret = ret + " - " + Strings.JuevesCorto;
                                break;
                            case 4:
                                ret = ret + " - " + Strings.ViernesCorto;
                                break;
                            case 5:
                                ret = ret + " - " + Strings.SabadoCorto;
                                break;
                            case 6:
                                ret = ret + " - " + Strings.DomingoCorto;
                                break;
                        }
                    }
                }
            }
            return ret;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//datos de prueba
        {
            MySQL.datosPrueba();
            revisarFacturas();
        }
    }
}
