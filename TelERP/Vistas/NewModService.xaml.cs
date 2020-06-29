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
    /// Lógica de interacción para NewModService.xaml
    /// </summary>
    public partial class NewModService : Window
    {
        private int id;
        Service s;

        private int dias = 0;

        public NewModService(int n)
        {
            InitializeComponent();
            id = n;
            Turn t = new Turn();
            DataTable dt = t.mostrarDatos();
            List<string> turnos = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                turnos.Add(dr[0].ToString() + " - " + dr[1].ToString());
            }
            cbTurnServices.ItemsSource = turnos;

            if (n == 0)
            {
                lblNewModService.Content = Strings.NuevoServicio;
            }
            else
            {
                s = new Service();
                s.recoverService(id);
                lblNewModService.Content = Strings.ModServicio;
                txtNombreNewModService.Text = s.Nombre;
                txtPrecioNewModService.Text = s.Precio.ToString();
                txtDescripcionNewModService.Text = s.Descripcion;
                cbTurnServices.SelectedIndex = s.Refturn - 1;

                string valor = s.Dias.ToString();
                while (valor.Length < 7)
                {
                    valor = "0" + valor;
                }
                for (int j = 0; j < valor.Length; j++)
                {
                    if (valor[j] == '1')
                    {
                        switch (j)
                        {
                            case 0:
                                cbxLunesService.IsChecked = true;
                                break;
                            case 1:
                                cbxMartesService.IsChecked = true;
                                break;
                            case 2:
                                cbxMiercolesService.IsChecked = true;
                                break;
                            case 3:
                                cbxJuevesService.IsChecked = true;
                                break;
                            case 4:
                                cbxViernesService.IsChecked = true;
                                break;
                            case 5:
                                cbxSabadoService.IsChecked = true;
                                break;
                            case 6:
                                cbxDomingoService.IsChecked = true;
                                break;
                        }
                    }
                }
            }
        }

        private void txtPrecioNewModService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Enter) e.Handled = false;
            else if (e.Key == Key.OemComma)
            {
                if (!txtPrecioNewModService.Text.Contains(","))
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

        private void btnCancelarNewModService_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOkNewModService_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombreNewModService.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirNombre);
            }
            else if (txtPrecioNewModService.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirPrecio);
            }
            else if (txtDescripcionNewModService.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirDescripcion);
            }
            else if (cbTurnServices.SelectedIndex == -1)
            {
                CustomMessageBox.Show(Strings.MsgElegirTurnoService);
            }
            else if (dias == 0)
            {
                CustomMessageBox.Show(Strings.MsgDiasService);
            }
            else
            {
                try
                {
                    decimal d = decimal.Parse(txtPrecioNewModService.Text);
                    string[] sel = cbTurnServices.SelectedItem.ToString().Split('-');
                    Turn t = new Turn();
                    DataRow dr = t.singleTurn(sel[0].Trim(), sel[1].Trim());
                    if (id == 0)
                    {
                        s = new Service(txtNombreNewModService.Text, d, dias, int.Parse(dr[0].ToString()), txtDescripcionNewModService.Text);
                        s.insertarme();
                        CustomMessageBox.Show(Strings.ServiceCreado);
                    }
                    else
                    {
                        s.recoverService(id);
                        s.Nombre = txtNombreNewModService.Text;
                        s.Precio = d;
                        s.Dias = dias;
                        s.Refturn = int.Parse(dr[0].ToString());
                        s.Descripcion = txtDescripcionNewModService.Text;
                        s.actualizame();
                        CustomMessageBox.Show(Strings.ServiceModificado);
                        //enviar mensaje como que el servicio se ha modificado
                        //enviarMensaje();
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
            User utmp = new User();
            DataTable uws = utmp.userWithService(s.Idservice);
            List<User> lista = new List<User>();
            foreach(DataRow dr in uws.Rows)
            {
                utmp.recoverUser(int.Parse(dr["iduser"].ToString()));
                lista.Add(utmp);
            }


            if (MainWindow.client.IsUserAuthorized())
            {
                var result = await MainWindow.client.GetContactsAsync();

                foreach(User u in lista)
                {
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
                        Turn t = new Turn();
                        DataRow dr = t.singleTurn(s.Refturn);
                        StringBuilder mensaje = new StringBuilder();
                        mensaje.Append(Strings.TelegramServicioModificado + " \n");
                        mensaje.Append(" - " + Strings.Nombre + s.Nombre + "\n - " + Strings.Dias + diasSemana(s.Dias.ToString()) + "\n - " + Strings.Precio + s.Precio + "€\n - " + Strings.Turno + dr[0].ToString() + " - " + dr[1].ToString() + "\n - " + Strings.Descripcion + s.Descripcion);
                        await MainWindow.client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, mensaje.ToString());
                    }
                }
            }
        }

        private void cbxService_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            if (cbx.Name.Equals("cbxLunesService")) dias += 1000000;
            else if (cbx.Name.Equals("cbxMartesService")) dias += 100000;
            else if (cbx.Name.Equals("cbxMiercolesService")) dias += 10000;
            else if (cbx.Name.Equals("cbxJuevesService")) dias += 1000;
            else if (cbx.Name.Equals("cbxViernesService")) dias += 100;
            else if (cbx.Name.Equals("cbxSabadoService")) dias += 10;
            else if (cbx.Name.Equals("cbxDomingoService")) dias += 1;
        }

        private void cbxService_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            if (cbx.Name.Equals("cbxLunesService")) dias -= 1000000;
            else if (cbx.Name.Equals("cbxMartesService")) dias -= 100000;
            else if (cbx.Name.Equals("cbxMiercolesService")) dias -= 10000;
            else if (cbx.Name.Equals("cbxJuevesService")) dias -= 1000;
            else if (cbx.Name.Equals("cbxViernesService")) dias -= 100;
            else if (cbx.Name.Equals("cbxSabadoService")) dias -= 10;
            else if (cbx.Name.Equals("cbxDomingoService")) dias -= 1;
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
