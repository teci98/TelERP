using System;
using System.Collections.Generic;
using System.Data;
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

namespace TelERP.Vistas
{
    /// <summary>
    /// Lógica de interacción para SelectUserTrabajo.xaml
    /// </summary>
    public partial class SelectUserTrabajo : Window
    {
        DataTable dtu;

        public SelectUserTrabajo()
        {
            InitializeComponent();
            User u = new User();
            dtu = u.mostrarDatos();
            dgSelUser.ItemsSource = dtu.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
            int ind = dgSelUser.SelectedIndex;
            if (ind == -1)
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarUsuario);
            }
            else
            {
                DataRowView tmp = (DataRowView)dgSelUser.SelectedItem;
                DataRow drt = tmp.Row;
                int id = int.Parse(drt[0].ToString());
                NewModAssign nma = new NewModAssign(0, id);
                nma.ShowDialog();
            }
        }

        private void txtSearchUsers_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Length == 0)
            {
                dgSelUser.ItemsSource = dtu.DefaultView;
            }
            else
            {
                DataRow[] sel = dtu.Select("(" + Strings.HeaderID + " = '" + txt.Text + "') or (" + Strings.HeaderNombre + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderApellidos + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderEmail + " LIKE '%" + txt.Text + "%') or (" + Strings.HeaderTelefono + " LIKE '%" + txt.Text + "%')");
                DataTable dat = dtu.Clone();
                foreach (DataRow row in sel)
                {
                    dat.ImportRow(row);
                }
                dgSelUser.ItemsSource = dat.DefaultView;
            }
        }
    }
}
