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
    public partial class buscarCliente : Form
    {

        private programaPrincipal f;

        // CONSTRUCTOR DE LA CLASE buscarCliente EL CUAL RECIBE COMO PARAMETRO LA CLASE programaPrincipal 
        // PARA ACCEDER A TODOS SUS COMPONENTES
        public buscarCliente(programaPrincipal programaPrincipal)
        {
            InitializeComponent();

            this.f = programaPrincipal;
        }

        // METODO PARA BUSCAR EL CLIENTE Y SELECCIONARLO
        private void bcBusqueda_TextChanged(object sender, EventArgs e)
        {
            TextBox sen = (TextBox)sender;

            if (sen.Text.ToString().Trim() != "" && bcDataGridView.Rows.Count >= 1)
            {
                for (int i = 0; i < bcDataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (bcDataGridView.Rows[i].Cells[j].Value.ToString().Contains(sen.Text.ToString()))
                        {
                            bcDataGridView.Rows[i].Cells[j].Selected = true;
                        }
                    }
                }
            }
        }

        // OCURRE CUANDO SE LE DA DOBLE CLICK AL DATAGRIDVIEW PARA INSERTAR LOS DATOS DEL CLIENTE SELECCIONADO
        private void bcDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bcDataGridView.Rows.Count > 0)
            {
                this.f.clienteId = Convert.ToInt16(bcDataGridView.Rows[bcDataGridView.CurrentRow.Index].Cells[0].Value.ToString());
                this.f.nfnombre.Text = bcDataGridView.Rows[bcDataGridView.CurrentRow.Index].Cells[1].Value.ToString();
                this.f.nfcedula.Text = bcDataGridView.Rows[bcDataGridView.CurrentRow.Index].Cells[2].Value.ToString();
                this.Dispose();
            }

        }

        // METODO QUE CARGA TODOS LOS CLIENTES DE LA BASE DE DATOS AL DATAGRIDVIEW
        private void buscarCliente_Load(object sender, EventArgs e)
        {
            llenarDataGridView._llenarDataGridView(bcDataGridView, "select * from clientes");
        }

        // BOTON CANCELAR DEL CONTENEDOR BUSCAR CLIENTE
        private void eccancelar_Click(object sender, EventArgs e)
        {
            if (bcDataGridView.Rows.Count > 0)
            {
                this.f.clienteId = Convert.ToInt16(bcDataGridView.Rows[bcDataGridView.CurrentRow.Index].Cells[0].Value.ToString());
                this.f.nfnombre.Text = bcDataGridView.Rows[bcDataGridView.CurrentRow.Index].Cells[1].Value.ToString();
                this.f.nfcedula.Text = bcDataGridView.Rows[bcDataGridView.CurrentRow.Index].Cells[2].Value.ToString();
                this.Dispose();
            }
            
        }
    }
}
