using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Prestamos
{
    public partial class login : Form
    {
        // INICIA TODOS LOS COMPONENTES DEL FORMULARIO LOGIN
        public login()
        {
            InitializeComponent();
        }

        // EVENTO KEYPRESS - HACE QUE CUANDO SE PRESIONE LA TECLA ENTER LO ENVIE AL SIGUIENTE TEXTBOX
        private void busuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                bcontrasena.Focus();
            }
        }

        // EVENTO KEYPRESS - HACE QUE CUANDO SE PRESIONE LA TECLA ENTER CONSULTE LA BASE DE DATOS
        // PARA COMPROBAR SI EXISTE EL NOMBRE Y CONTRASEÑA DEL USUARIO
        private void bcontrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (busuario.Text.Trim().Length > 2)
                {
                    if (bcontrasena.Text.Trim().Length > 2)
                    {

                        Conexion conn = new Conexion();

                        Conexion.ConectarBD.Open();
                        try 
	                    {	        
		                    SqlCommand com = new SqlCommand("select count(*) from empleados where nombre='" + busuario.Text.Trim() + "' and contrasena='" + bcontrasena.Text.Trim() + "'", Conexion.ConectarBD);

                            if(Convert.ToInt32(com.ExecuteScalar()) == 1) {
                                programaPrincipal pro = new programaPrincipal();
                                pro.Show();
                                this.Hide();
                            }
                            
	                    }
	                    catch (Exception)
	                    {

	                    }
                        finally
                        {
                            Conexion.ConectarBD.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe de escribir mas de 3 carácteres en el campo \"Contraseña\"");
                        bcontrasena.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Debe de escribir un usuario");
                    busuario.Focus();
                }
            }
        }
    }
}
