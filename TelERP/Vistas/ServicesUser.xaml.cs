using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TelERP.Modelo;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;

namespace TelERP.Vistas
{
    /// <summary>
    /// Lógica de interacción para ServicesUser.xaml
    /// </summary>
    public partial class ServicesUser : Window
    {
        private int id;
        private Service s;
        private Turn t;
        private UserService us;
        private DataTable serviciosAntes;

        public ServicesUser(int id)
        {
            InitializeComponent();
            this.id = id;
            s = new Service();
            t = new Turn();
            us = new UserService();
            DataTable dt = s.mostrarDatos();
            DataColumn dc = new DataColumn();
            dc.ColumnName = Strings.HeaderSeleccionar;
            dc.DataType = typeof(bool);
            dc.DefaultValue = false;
            dt.Columns.Add(dc);
            serviciosAntes = us.mostrarDatos(id);
            foreach(DataRow dr in serviciosAntes.Rows)
            {
                foreach(DataRow dr2 in dt.Rows)
                {
                    if (dr[Strings.HeaderID].ToString().Equals(dr2[Strings.HeaderID])) dr2[Strings.HeaderSeleccionar] = true;
                }
            }

            dgServicesUser.ItemsSource = dt.DefaultView;
        }

        private void dgServiceUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = dgServicesUser.SelectedIndex;
            DataRowView tmp = (DataRowView)dgServicesUser.SelectedItem;
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

        private void DataGridCell_MouseUp(object sender, MouseButtonEventArgs e)//funciona
        {
            DataGridCell cell = (DataGridCell)sender;
            if (cell.Column.Header.ToString().Equals(Strings.HeaderSeleccionar))
            {
                CheckBox cbx = (CheckBox)cell.Content;
                cbx.IsChecked = !cbx.IsChecked;
                int ind = dgServicesUser.SelectedIndex;
                DataRowView drv = (DataRowView)dgServicesUser.Items[ind];
                DataRow dr = drv.Row;
                bool comparar = (bool)dr[Strings.HeaderSeleccionar];
                if (comparar) dr[Strings.HeaderSeleccionar] = false;
                else dr[Strings.HeaderSeleccionar] = true;
            }
        }

        private void btnCancelarServicesUser_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox.Show(Strings.MsgCambiosCancelados);
            Close();
        }

        private void txtSearchServicesUser_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable dts = s.mostrarDatos();
            DataColumn dc = new DataColumn();
            dc.ColumnName = Strings.HeaderSeleccionar;
            dc.DataType = typeof(bool);
            dc.DefaultValue = false;
            dts.Columns.Add(dc);
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length == 0)
            {
                dgServicesUser.ItemsSource = dts.DefaultView;
            }
            else
            {
                DataRow[] sel = dts.Select("(" + Strings.HeaderID + " = '" + txt.Text + "') or (" + Strings.HeaderNombre + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderPrecio + " = '" + txt.Text + "')");
                DataTable dat = dts.Clone();
                foreach (DataRow row in sel)
                {
                    dat.ImportRow(row);
                }
                dgServicesUser.ItemsSource = dat.DefaultView;
            }
        }

        private void btnOkServicesUser_Click(object sender, RoutedEventArgs e)
        {
            SelectionMessageBox.Show(Strings.MsgRealizarCambiosPregunta);
            if (SelectionMessageBox.respuesta)
            {
                foreach(DataRowView drv in dgServicesUser.Items)
                {
                    DataRow dr = drv.Row;

                    if (dr[Strings.HeaderSeleccionar].ToString().Equals("True"))
                    {
                        if (serviciosAntes.Select(Strings.HeaderID +  " = " +dr[Strings.HeaderID]).Length == 0)
                        {
                            //se añade el servicio
                            String fecha = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            us = new UserService(fecha,id,int.Parse(dr[Strings.HeaderID].ToString()));
                            us.insertarme();
                            //si la invoice esta creada se deberia añadir el item
                            Invoice i = new Invoice();
                            i.recoverInvoice(id, DateTime.Now.Month, DateTime.Now.Year);
                            InvoiceItem ii = new InvoiceItem(i.Idinvoice, 0, int.Parse(dr[Strings.HeaderID].ToString()));
                            ii.insertarme();
                        }
                    }
                    else
                    {
                        if (serviciosAntes.Select(Strings.HeaderID + " = " + dr[Strings.HeaderID]).Length == 1)
                        {
                            us.borrar(id,int.Parse(dr[Strings.HeaderID].ToString()));
                            //al borrar el servicio habria que añadir una entrada en la invoice
                            Invoice i = new Invoice();
                            i.recoverInvoice(id, DateTime.Now.Month, DateTime.Now.Year);
                            InvoiceItem ii = new InvoiceItem();
                            ii.deleteInvoiceItem(i.Idinvoice, int.Parse(dr[Strings.HeaderID].ToString()));
                        }
                    }
                }
                //notificar al usuario de los cambios
                //enviarMensaje();
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgCambiosCancelados);
            }
            Close();
        }

        private async void enviarMensaje()
        {
            User u = new User();
            u.recoverUser(id);
            if (MainWindow.client.IsUserAuthorized())
            {
                var result = await MainWindow.client.GetContactsAsync();

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
                    var rrr = await MainWindow.client.SendRequestAsync<TLImportedContacts>(req);

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
                    StringBuilder mensaje = new StringBuilder();
                    mensaje.Append(Strings.TelegramServiciosModificado + " \n");
                    DataTable servicios = us.datosMensaje(u.Iduser);
                    foreach (DataRow dr in servicios.Rows)
                    {
                        mensaje.Append(" - " + dr["nombre"] + ", " + diasSemana(dr["dias"].ToString()) + ", " + dr["turno"] + "\n");
                    }
                    await MainWindow.client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, mensaje.ToString());
                }
            }
        }

        public string diasSemana(string valor)
        {
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

        
    }
}
