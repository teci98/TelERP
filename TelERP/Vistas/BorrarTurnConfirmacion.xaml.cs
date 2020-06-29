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
    /// Lógica de interacción para BorrarTurnConfirmacion.xaml
    /// </summary>
    public partial class BorrarTurnConfirmacion : Window
    {

        private int id;
        private Turn t;

        public BorrarTurnConfirmacion(int n)
        {
            InitializeComponent();
            id = n;
            t = new Turn();
            DataTable dt = t.tablaCompleta();
            List<string> turnos = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr[0].ToString()) != id)
                {
                    turnos.Add(dr[1].ToString() + " - " + dr[2].ToString());
                }
            }
            cbTurno.ItemsSource = turnos;
        }

        private void btnBorrarTurnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnBorrarTurnOK_Click(object sender, RoutedEventArgs e)
        {
            if(cbTurno.SelectedIndex != -1)
            {
                string[] sel = cbTurno.SelectedItem.ToString().Split('-');
                DataRow dr = t.singleTurn(sel[0].Trim(), sel[1].Trim());
                t.updateServicesTurn(id,int.Parse(dr[0].ToString()));
                CustomMessageBox.Show(Strings.TurnoBorrado);
                Close();
            }
            else
            {
                CustomMessageBox.Show(Strings.MsgSeleccionarTurn);
            }
        }
    }
}
