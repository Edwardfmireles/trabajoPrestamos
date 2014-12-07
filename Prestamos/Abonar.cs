using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Prestamos
{
    public partial class Abonar : Form
    {

        private int cantidad;
        private int mora;
        private int idCliente;
        private int idFactura;
        Conexion conn = new Conexion();
        
        public Abonar(int _idCliente, int _idFactura, string nombre) 
        {
            InitializeComponent();

            this.Name = nombre;

            this.idCliente = _idCliente;

            this.idFactura = _idFactura;
            
            try
            {
                Conexion.ConectarBD.Open();

                SqlCommand comm = new SqlCommand("select prestamos.moraPrestamo from prestamos,facturacion where prestamos.idPrestamo=facturacion.idPrestamo and facturacion.idFactura=CONVERT(int," + idFactura + ")", Conexion.ConectarBD);

                SqlDataReader morasql = comm.ExecuteReader();

                morasql.Read();
                this.mora = morasql.GetInt32(0);
                Conexion.ConectarBD.Close();


                cargarDatos();


              
            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString());
             
            }
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox sen = (TextBox)sender;

            if (int.TryParse(sen.Text.Trim(), out this.cantidad) && this.cantidad > 0)
            {
                pagar.Enabled = true;
            }
            else
            {
                sen.Text = "";
                pagar.Enabled = false;
            }
        }

        private void pagar_Click(object sender, EventArgs e)
        {
            string fecha = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            int cuota = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString());
            string estado = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

            DateTime fechaActual = DateTime.Today;

            DateTime fechaAPagar = Convert.ToDateTime(fecha);

            if (fechaActual < fechaAPagar )
            {

                MessageBox.Show(this.mora.ToString());
            }
            else
            {
                MessageBox.Show("Fecha a pagar Mayor");
            }

           // MessageBox.Show(fechaActual.ToString() +" " + fechaAPagar.ToString());
            

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "NO PAGO")
                {
                    if (fechaActual < fechaAPagar)
                    {


                        if (this.cantidad < cuota)
                        {
                            MessageBox.Show("Falta Dinero");
                            textBox1.Focus();
                            break;
                        } 
                        else if(this.cantidad == cuota ) 
                        {
                            try
                            {
                                Conexion.ConectarBD.Open();

                                if(conn.actualizar("intervalos","estado", "'PAGADO'", "intervalos.idCliente=CONVERT(int," + this.idCliente + "), and intervalos.idFactura=CONVERT(int," + this.idFactura +"), and intervalos.intervaloFecha='" + fecha +"')") == true) 
                                {
                                    dataGridView1.DataSource = null;

                                    cargarDatos();
                                }


                            }
                            catch (SqlException se)
                            {
                                MessageBox.Show(se.ToString());
                            }
                        }
                        else
                        {
                            
                        }




                    }


                    

                }
                else
                {
                    textBox1.Text = "";
                    pagar.Enabled = false;
                }
            }

        }


        private void cargarDatos()
        {
            Conexion.ConectarBD.Open();

            SqlDataAdapter dataA = new SqlDataAdapter("select intervalos.intervaloFecha 'fechas', intervalos.intervaloPago 'cuotas RD$', intervalos.estado from intervalos where intervalos.idFactura = " + idFactura, Conexion.ConectarBD);

            DataTable dataT = new DataTable();

            dataA.Fill(dataT);

            dataGridView1.DataSource = dataT;

            Conexion.ConectarBD.Close();
        }


    }
}
