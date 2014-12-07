using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prestamos
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void busuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                bcontrasena.Focus();
            }
        }

        private void bcontrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (busuario.Text.Trim().Length > 3)
                {
                    if (bcontrasena.Text.Trim().Length > 3)
                    {
                        programaPrincipal pro = new programaPrincipal();
                        pro.Show();
                        this.Hide();
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
