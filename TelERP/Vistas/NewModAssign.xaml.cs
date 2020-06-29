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
    /// Lógica de interacción para NewModAssign.xaml
    /// </summary>
    public partial class NewModAssign : Window
    {

        private int id;
        private int iduser;
        private Assignment a;

        public NewModAssign(int id, int user)
        {
            InitializeComponent();
            this.id = id;
            this.iduser = user;
            if (id == 0)
            {
                lblNewModAssign.Content = Strings.NuevoTrabajo;
            }
            else
            {
                lblNewModAssign.Content = Strings.ModTrabajo;
                a = new Assignment();
                a.recoverAssignment(id);
                txtNewModAssignNombre.Text = a.Nombre;
                txtNewModAssignPrecio.Text = a.Precio.ToString();
                txtNewModAssignDescripcion.Text = a.Descripcion;
                string[] fe = a.Fecha.Split(' ');
                string[] date = fe[0].Split('/');
                string[] time = fe[1].Split(':');
                DateTime fecha = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
                dpkNewModAssign.SelectedDate = fecha;
                lblNewModAssignHora.Content = fe[1];
            }
        }

        private void btnNewModAssignCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNewModAssignOk_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewModAssignNombre.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirNombre);
            }
            else if (txtNewModAssignPrecio.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirPrecio);
            }
            else if (txtNewModAssignDescripcion.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirDescripcion);
            }
            else if (dpkNewModAssign.SelectedDate == null)
            {
                CustomMessageBox.Show(Strings.MsgElegirFecha);
            }else if (dpkNewModAssign.SelectedDate < DateTime.Now)
            {
                CustomMessageBox.Show(Strings.MsgElegirFechaMayor);
            }
            else
            {
                try
                {
                    decimal precio = decimal.Parse(txtNewModAssignPrecio.Text);
                    if(id == 0)
                    {
                        a = new Assignment(txtNewModAssignNombre.Text, iduser, dpkNewModAssign.SelectedDate.ToString(), precio, txtNewModAssignDescripcion.Text);
                        a.insertarme();
                        CustomMessageBox.Show(Strings.MsgAssignmentInsertado);
                        //meterlo en la invoice
                        string[] fecs = a.Fecha.Split('/');
                        int mestmp = int.Parse(fecs[1].ToString());
                        if (mestmp == DateTime.Now.Month)
                        {
                            Invoice i = new Invoice();
                            try
                            {
                                i.recoverInvoice(iduser, DateTime.Now.Month, DateTime.Now.Year);
                            }
                            catch
                            {
                                i = new Invoice(iduser, DateTime.Now.Month, DateTime.Now.Year,0);
                                i.insertarme();
                            }
                            
                            InvoiceItem ii = new InvoiceItem(i.Idinvoice, 1, a.Idassignment);
                            ii.insertarme();
                        }
                        //notificarlo al usuario
                        //enviarMensaje();
                    }
                    else
                    {
                        a.Nombre = txtNewModAssignNombre.Text;
                        a.Fecha = dpkNewModAssign.SelectedDate.ToString();
                        a.Precio = precio;
                        a.Descripcion = txtNewModAssignDescripcion.Text;
                        a.actualizame();
                        CustomMessageBox.Show(Strings.MsgAssignmentModificado);
                    }
                    Close();
                }
                catch
                {
                    CustomMessageBox.Show(Strings.MsgPrecioMal);
                }
            }
        }

        private async void enviarMensaje()
        {
            User u = new User();
            u.recoverUser(iduser);
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
                    mensaje.Append(Strings.TelegramTrabajoCreado + " \n");
                    mensaje.Append(" - " + Strings.Nombre + a.Nombre + "\n - " + Strings.Fecha + a.Fecha + "\n - " + Strings.Precio + a.Precio + "€\n - " + Strings.Descripcion + a.Descripcion);
                    await MainWindow.client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, mensaje.ToString());
                }
            }
        }

        private void txtNewModAssignPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Enter) e.Handled = false;
            else if (e.Key == Key.OemComma)
            {
                if (!txtNewModAssignPrecio.Text.Contains(","))
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
                e.Handled = true;
                CustomMessageBox.Show(Strings.MsgSoloNumerosPrecio);
            }
        }

        private void dpkNewModAssign_KeyDown(object sender, KeyEventArgs e)
        {
            DatePicker txt = (DatePicker)sender;
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Enter || e.Key == Key.Space) e.Handled = false;
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
                if (e.Key == Key.Tab) txtNewModAssignPrecio.Focus();
                else
                {
                    e.Handled = true;
                    CustomMessageBox.Show(Strings.MsgSoloNumerosTurn);
                }

            }
        }

        private void dpkNewModAssign_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string str = dpkNewModAssign.SelectedDate.ToString();
                string[] strs = str.Split(' ');
                lblNewModAssignHora.Content = strs[1];
            }
            catch
            {

            }
        }
    }
}
