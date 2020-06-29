using TelERP.Controlador;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TelERP.Modelo
{
    class User
    {
        private int iduser;
        private string nombre;
        private string apellidos;
        private string email;
        private int telefono;
        private byte[] photo;

        private GestorUsers gestor;

        public User()
        {
            this.gestor = new GestorUsers();
        }

        public User(string nombre, string apellidos, string email, int telefono)//un nuevo usuario creado dede la interfaz, tiene un id automatico
        {
            this.gestor = new GestorUsers();
            this.iduser = gestor.getLastId()+1;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.telefono = telefono;
            this.photo = null;
        }

        /// <summary>
        /// Method that returns the data which will be displayed in the table
        /// Metodo que devuelve los datos que seran mostrados en la tabla
        /// </summary>
        /// <returns>A DataTable with the data --- Un DataTable con los datos</returns>
        public DataTable mostrarDatos()
        {
            return gestor.mostrarDatos();
        }

        /// <summary>
        /// Method that returns all the data 
        /// Metodo que devuelve todos los datos
        /// </summary>
        /// <returns>A DataTable with the data --- Un DataTable con los datos</returns>
        public DataTable tablaCompleta()
        {
            return gestor.tablaCompleta();
        }

        /// <summary>
        /// Method that return the data of a single user
        /// Metodo que devuelve los datos de un unico usuario
        /// </summary>
        /// <param name="id">The id of the user --- El id del usuario</param>
        /// <returns>A DataRow with the user info, null if it didn't find nothing --- Una DataRow con los datos del usuario, null si no encuentra nada</returns>
        public DataRow singleUser(int id)
        {
            return gestor.infoUser(id);
        }

        public DataTable userWithService(int id)
        {
            return gestor.userWithService(id);
        }
        public DataTable usersDeuda()
        {
            return gestor.usersDeuda();
        }

        public decimal getDeuda(int id)
        {
            decimal ret = 0;
            ret = decimal.Parse(gestor.getDeuda(id));

            return ret;
        }

        /// <summary>
        /// Method that fill all the data of a specific user into the object
        /// Metodo que llena todos los datos de un usuario especifico dentro del objeto
        /// </summary>
        /// <param name="id">The id of the user --- El id del usuario</param>
        public void recoverUser(int id)
        {
            this.iduser = id;
            DataRow dr = gestor.infoUser(id);
            this.nombre = dr["nombre"].ToString();
            this.apellidos = dr["apellidos"].ToString();
            this.email = dr["email"].ToString();
            this.telefono = int.Parse(dr["telefono"].ToString());
            this.photo = gestor.getImage(id);
        }

        /// <summary>
        /// Method that insert the current object into de database
        /// Metodo que inserta el objeto actual en la base de datos
        /// </summary>
        public void insertarme()
        {
            gestor.insertarUser(iduser,nombre,apellidos,email,telefono);
        }


        public void actualizame()
        {
            gestor.actualizaUser(iduser, nombre, apellidos, email, telefono);
        }

        public void pagarDeuda(int id)
        {
            gestor.pagarDeuda(id);
        }

        internal void pagarTrabajos(int id)
        {
            gestor.pagarTrabajos(id);
        }

        public void borrame()
        {
            gestor.borrarUser(iduser);
        }

        public void borrar(int id)
        {
            gestor.borrarUser(id);
        }

        public void cambiarFoto()
        {
            string foto = BitConverter.ToString(this.Photo).Trim().Replace("-", "");
            gestor.cambiaFoto(this.iduser, foto);
        }

        /// <summary>
        /// Method that return the image of a specific user
        /// Metodo que devuelve la imagen de un usuario especifico
        /// </summary>
        /// <param name="id">The id of the user --- El id del usuario</param>
        /// <returns>A BitmapImage with the image, null if didn't find nothing --- Un BitmapImage con la imagen, null si no encuentra nada</returns>
        public BitmapImage getImage(int id)
        {
            byte[] byt = gestor.getImage(id);
            return crearImagen(byt);
        }

        /// <summary>
        /// Method that return the image of this user
        /// Metodo que devuelve la imagen de este usuario
        /// </summary>
        /// <returns>A BitmapImage with the image, null if didn't find nothing --- Un BitmapImage con la imagen, null si no encuentra nada</returns>
        public BitmapImage getImage()
        {
            return crearImagen(this.photo);
        }

        /// <summary>
        /// Method that create a BitmapImage from a byte array
        /// Metodo que crea una BitmapImage a partir de un array de bytes
        /// </summary>
        /// <param name="byt">Array with the info of the image --- Array con la informacion de la imagen</param>
        /// <returns>A BitmapImage with the image, null if can't make the image --- Un BitmapImage con la imagen, null si puede hacer la imagen</returns>
        private BitmapImage crearImagen(byte[] byt)
        {
            var image = new BitmapImage();
            if (byt != null)
            {
                using (var mem = new MemoryStream(byt))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
            }
            else image = null;

            return image;
        }

        public int Iduser { get => iduser; set => iduser = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Email { get => email; set => email = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public byte[] Photo { get => photo; set => photo = value; }

    }
}
