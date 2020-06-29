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

namespace TelERP.Vistas
{
    /// <summary>
    /// Lógica de interacción para CustomMessageBox.xaml
    /// </summary>
    public partial class SelectionMessageBox : Window
    {

        public static bool respuesta = false;

        public SelectionMessageBox(string msg)
        {
            InitializeComponent();
            tbmensaje.Text = msg;
        }

        public static bool? Show(string msg)
        {
            SelectionMessageBox cmb = new SelectionMessageBox(msg);
            return cmb.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//cancelar
        {
            respuesta = false;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//confirmar
        {
            respuesta = true;
            Close();
        }
    }
}
