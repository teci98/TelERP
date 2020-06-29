using System;
using System.Collections.Generic;
using System.Data;
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

namespace TelERP.Vistas
{
    /// <summary>
    /// Lógica de interacción para UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        User u;
        UserService us;

        public UserInfo(int id)
        {
            InitializeComponent();
            u = new User();
            u.recoverUser(id);
            imgUserInfo.Source = u.getImage();
            lblNombreUserInfo.Content = Strings.Nombre + u.Nombre;
            lblApellidoUserInfo.Content = Strings.Apellidos + u.Apellidos;
            lblEmailUserInfo.Content = Strings.Email + u.Email;
            lblPhoneUserInfo.Content = Strings.Telefono + u.Telefono;
            lblTelegramUserInfo.Content = Strings.Telegram;
            lblDebtUserInfo.Content = Strings.Deuda + u.getDeuda(id) + " €";
            us = new UserService();
            dgServicesUserInfo.ItemsSource = us.mostrarDatos(u.Iduser).DefaultView;
        }

        private void btnCancelUserInfo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAddServiceUserInfo_Click(object sender, RoutedEventArgs e)
        {
            ServicesUser su = new ServicesUser(u.Iduser);
            su.ShowDialog();
            dgServicesUserInfo.ItemsSource = us.mostrarDatos(u.Iduser).DefaultView;
        }

        private void btnAddAssignUserInfo_Click(object sender, RoutedEventArgs e)
        {
            NewModAssign nma = new NewModAssign(0, u.Iduser);
            nma.ShowDialog();
            dgServicesUserInfo.ItemsSource = us.mostrarDatos(u.Iduser).DefaultView;
        }

        private void btnModAssignUserInfo_Click(object sender, RoutedEventArgs e)
        {
            int ind = dgServicesUserInfo.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarTrabajo);
            }
            else
            {
                DataRowView tmp = (DataRowView)dgServicesUserInfo.SelectedItem;
                DataRow drt = tmp.Row;
                string tipo = drt[0].ToString();
                if (tipo.Equals(Strings.TrabajoSolo))
                {
                    int id = int.Parse(drt[1].ToString());
                    NewModAssign nma = new NewModAssign(id,u.Iduser);
                    nma.ShowDialog();
                    dgServicesUserInfo.ItemsSource = us.mostrarDatos(u.Iduser).DefaultView;
                }
                else
                {
                    CustomMessageBox.Show(Strings.MsgModificarServiciosPrincipal);
                }
                
            }
        }

        private void btnDetallesUserInfo_Click(object sender, RoutedEventArgs e)
        {
            int ind = dgServicesUserInfo.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarItem);
            }
            else
            {
                DataRowView tmp = (DataRowView)dgServicesUserInfo.SelectedItem;
                DataRow drt = tmp.Row;
                string tipo = drt[0].ToString();
                int id = int.Parse(drt[1].ToString());
                DetallesItemUser dis;
                if (tipo.Equals(Strings.TrabajoSolo))
                {
                    dis = new DetallesItemUser(id, 1);
                }
                else
                {
                    dis = new DetallesItemUser(id, 0);
                }
                dis.ShowDialog();
            }
        }

        private void dgServicesUserInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int ind = dgServicesUserInfo.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarItem);
            }
            else
            {
                DataRowView tmp = (DataRowView)dgServicesUserInfo.SelectedItem;
                DataRow drt = tmp.Row;
                string tipo = drt[0].ToString();
                int id = int.Parse(drt[1].ToString());
                DetallesItemUser dis;
                if (tipo.Equals(Strings.TrabajoSolo))
                {
                    dis = new DetallesItemUser(id, 1);
                }
                else
                {
                    dis = new DetallesItemUser(id, 0);
                }
                dis.ShowDialog();
            }
        }
    }
}
