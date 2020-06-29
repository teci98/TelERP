using System;
using System.Collections.Generic;
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

namespace TelERP.Vistas
{
    /// <summary>
    /// Lógica de interacción para MensajeUser.xaml
    /// </summary>
    public partial class MensajeUser : Window
    {
        private int id;

        public MensajeUser(int n)
        {
            InitializeComponent();
            id = n;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (texto.Text.Length == 0 || texto.Text.Equals(Strings.EscribirMensaje)) CustomMessageBox.Show(Strings.MsgEscribaMensaje);
            else
            {
                enviarMensaje();
                Close();
            }
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
                    await MainWindow.client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, texto.Text);
                    CustomMessageBox.Show(Strings.MensajeEnviado);
                }else CustomMessageBox.Show(Strings.MsgNoTelegram);
            }
        }

        private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (texto.Text.Equals(Strings.EscribirMensaje)) texto.Text = "";
        }
    }
}
