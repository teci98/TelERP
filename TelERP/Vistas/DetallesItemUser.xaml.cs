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
    /// Lógica de interacción para DetallesItemUser.xaml
    /// </summary>
    public partial class DetallesItemUser : Window
    {
        public DetallesItemUser(int id, int tipo)
        {
            InitializeComponent();
            if(tipo == 0)//servicio
            {
                Service s = new Service();
                Turn t = new Turn();
                s.recoverService(id);
                lblDetallesNombre.Content = Strings.Nombre + s.Nombre;
                lblDetallesDiasUser.Content = Strings.Dias + diasSemana(s.Dias.ToString());
                DataRow dr = t.singleTurn(s.Refturn);
                lblDetallesTurnoFecha.Content = Strings.Turno + dr[1] + " - " + dr[2];
                lblDetallesPrecio.Content = Strings.Precio + s.Precio;
                tbDetallesDescripcion.Text = s.Descripcion;
            }else
            {
                Assignment a = new Assignment();
                User u = new User();
                a.recoverAssignment(id);
                lblDetallesNombre.Content = Strings.Nombre + a.Nombre;
                u.recoverUser(a.Refuser);
                lblDetallesDiasUser.Content = Strings.Usuario + u.Nombre + " " + u.Apellidos;
                lblDetallesTurnoFecha.Content = Strings.Fecha + a.Fecha;
                lblDetallesPrecio.Content = Strings.Precio + a.Precio;
                tbDetallesDescripcion.Text = a.Descripcion;
            }
        }

        private void btnCerrarDetalles_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
    }
}
