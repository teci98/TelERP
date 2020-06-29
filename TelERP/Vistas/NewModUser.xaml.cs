using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TLSharp.Core;

namespace TelERP.Vistas
{
    /// <summary>
    /// Lógica de interacción para NewModUser.xaml
    /// </summary>
    public partial class NewModUser : Window
    {
        private int id;
        User u;

        public NewModUser(int n)
        {
            InitializeComponent();
            id = n;
            if (n == 0)
            {
                lblNewModUser.Content = Strings.NuevoUsuario;
            }
            else
            {
                lblNewModUser.Content = Strings.ModUsuario;
                u = new User();
                u.recoverUser(id);
                txtNameNewUser.Text = u.Nombre;
                txtSurnameNewUser.Text = u.Apellidos;
                txtEmailNewUser.Text = u.Email;
                txtPhoneNewUser.Text = u.Telefono.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//confirmar
        {
            confirmar();
        }

        private void confirmar()
        {
            if (txtNameNewUser.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirNombre);
            }
            else if (txtSurnameNewUser.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirApellido);
            }
            else if (txtEmailNewUser.Text.Length == 0 || !validarEmail(txtEmailNewUser.Text))
            {
                CustomMessageBox.Show(Strings.MsgEscribirEmail);
            }
            else if (txtPhoneNewUser.Text.Length == 0)
            {
                CustomMessageBox.Show(Strings.MsgEscribirTelefono);
            }
            else
            {
                try
                {
                    int phone = int.Parse(txtPhoneNewUser.Text);

                    if (id == 0)
                    {
                        //insertar el usuario
                        u = new User(txtNameNewUser.Text, txtSurnameNewUser.Text, txtEmailNewUser.Text, phone);
                        u.insertarme();

                        //intentar obtener la foto de telegram
                        obtenerFoto();

                        SelectionMessageBox.Show(Strings.MsgSusbscribirUsuarioServicio);
                        if (SelectionMessageBox.respuesta)
                        {
                            //hacer algo para meter servicios
                            ServicesUser su = new ServicesUser(u.Iduser);
                            su.ShowDialog();
                        }
                        CustomMessageBox.Show(Strings.UsuarioCreado);
                        Close();
                    }
                    else
                    {
                        u.Nombre = txtNameNewUser.Text;
                        u.Apellidos = txtSurnameNewUser.Text;
                        u.Email = txtEmailNewUser.Text;
                        if (u.Telefono != phone)
                        {
                            u.Telefono = phone;
                        }
                        u.actualizame();
                        CustomMessageBox.Show(Strings.UsuarioModificado);
                        Close();
                    }

                }
                catch (FormatException ex)
                {
                    CustomMessageBox.Show(Strings.MsgEscribirTelefonoValido);
                }
            }
        }

        private async void obtenerFoto()
        {

            var result = await MainWindow.client.GetContactsAsync();

            var user = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.Phone == "34"+u.Telefono);
            bool mirarfoto = true;
            if (user == null)
            {
                //await MainWindow.client.ConnectAsync();
                var phoneContact = new TLInputPhoneContact() { Phone = "34" + u.Telefono, FirstName = u.Nombre, LastName = u.Apellidos };
                var contacts = new List<TLInputPhoneContact>() { phoneContact };
                var req = new TLRequestImportContacts() { Contacts = new TLVector<TLInputPhoneContact>(contacts) };
                var rrr = await MainWindow.client.SendRequestAsync<TLImportedContacts>(req);

                if(rrr.Imported.Count == 1)
                {
                    user = result.Users
                        .Where(x => x.GetType() == typeof(TLUser))
                        .Cast<TLUser>()
                        .FirstOrDefault(x => x.Phone == "34" + u.Telefono);
                }
                else
                {
                    mirarfoto = false;
                }

            }
            if (mirarfoto && user != null)
            {
                TLAbsUserProfilePhoto photo = user.Photo;
                TLUserProfilePhoto up = (TLUserProfilePhoto)photo;
                if (up != null && up.PhotoBig != null)
                {
                    TLFileLocation tf = (TLFileLocation)up.PhotoBig;
                    TLAbsInputFileLocation aifl = new TLInputFileLocation()
                    {
                        LocalId = tf.LocalId,
                        Secret = tf.Secret,
                        VolumeId = tf.VolumeId
                    };
                    TeleSharp.TL.Upload.TLFile buffer = await MainWindow.client.GetFile(aifl, 1024 * 1024);
                    if(buffer.Bytes != null)
                    {
                        u.Photo = buffer.Bytes;
                        u.cambiarFoto();
                    }
                    
                }
            }

        }

        private bool validarEmail(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//cancelar
        {
            Close();
        }

        private void txtNewUser_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.Key == Key.Enter)
            {
                if (txt.Name.Equals("txtNameNewUser")) txtSurnameNewUser.Focus();
                else if (txt.Name.Equals("txtSurnameNewUser")) txtEmailNewUser.Focus();
                else if (txt.Name.Equals("txtEmailNewUser")) txtPhoneNewUser.Focus();
                else confirmar();
            }

            if (txt.Name.Equals("txtPhoneNewUser") && !(e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)) e.Handled = true;
        }
    }
}
