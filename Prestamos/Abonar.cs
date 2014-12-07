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
        private short periodoPago;
        Conexion conn = new Conexion();
        
        // CONSTRUCTOR DE LA CLASE ABONAR QUE TIENE COMO PARAMETROS EL IDCLIENTE, IDFACTURA Y NOMBRE DEL CLIENTE 
        // Y SE UTILIZA PARA GARCAR SU HISTORIAL DEL PRESTAMO
        public Abonar(int _idCliente, int _idFactura, string nombre) 
        {
            InitializeComponent();

            this.Name = nombre;

            this.idCliente = _idCliente;

            this.idFactura = _idFactura;

            try
            {
                Conexion.ConectarBD.Open();

                SqlCommand comm = new SqlCommand("select prestamos.moraPrestamo from prestamos,facturacion where prestamos.idPrestamo=facturacion.idPrestamo and facturacion.idFactura=CONVERT(int," + this.idFactura + ")", Conexion.ConectarBD);

                SqlDataReader morasql = comm.ExecuteReader();

                morasql.Read();
                this.mora = morasql.GetInt32(0);

                Conexion.ConectarBD.Close();

                //-------------------------------------------------------------------------------------

                Conexion.ConectarBD.Open();
               
                SqlCommand com = new SqlCommand("select periodoPago from prestamos,facturacion where prestamos.idPrestamo=facturacion.idPrestamo and facturacion.idFactura=CONVERT(int," + this.idFactura + ")", Conexion.ConectarBD);

                SqlDataReader _periodoPago = com.ExecuteReader();
                
                _periodoPago.Read();
                switch (_periodoPago.GetString(0))
                {
                    case "QUINCENAL" :
                        this.periodoPago = 15;
                        break;
                    case "MENSUAL" :
                        this.periodoPago = 30;
                        break;
                    case "ANUAL" :
                        this.periodoPago = 365;
                        break;
                }


                Conexion.ConectarBD.Close();


                cargarDatos();


              
            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString());
             
            }
            

        }

        // METODO PARA QUE EL USUARIO ESCRIBA SOLO NUMEROS EN EL TEXTBOX
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox sen = (TextBox)sender;

            int aa;


            if (int.TryParse(sen.Text.Trim(), out aa) && aa > 0)
            {
                pagar.Enabled = true;
            }
            else
            {
                sen.Text = "";
                pagar.Enabled = false;
            }

        }

        // HACE TODO EL PROCESO DE PAGO DE LOS MESES (ABONO) AL PULSAR EL BOTON PAGAR
        private void pagar_Click(object sender, EventArgs e)
        {
            string fecha = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            int cuota = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString());
            string estado = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();

            this.cantidad = Convert.ToInt32(textBox1.Text.Trim());

            DateTime fechaActual = DateTime.Today;

            DateTime fechaAPagar = Convert.ToDateTime(fecha);
          

            MessageBox.Show(this.periodoPago.ToString());

            if (estado != "PAGADO")
            {
                if (fechaActual < fechaAPagar)
                {
                    if (this.cantidad < cuota)
                    {
                        MessageBox.Show("Falta Dinero" + this.cantidad + " " + cuota);
                        textBox1.Focus();
                    }
                    else if (this.cantidad == cuota)
                    {
                        MessageBox.Show("cantidad " + cantidad + " cuotas " + cuota);

                        conn.actualizar("intervalos", "estado", "'PAGADO'", "intervalos.idCliente=CONVERT(int," + this.idCliente + ") and intervalos.idFactura=CONVERT(int," + this.idFactura + ") and intervalos.intervaloFecha='" + fecha + "'");
                            
                        dataGridView1.DataSource = null;
                        
                        MessageBox.Show(Conexion.mensaje);
                        
                        cargarDatos();
                            
                        MessageBox.Show(Conexion.mensaje);
                    }
                    else
                    {
                        conn.actualizar("intervalos", "intervaloPago", "CONVERT(int," + (cuota + cantidad) + ")", "intervalos.idCliente=CONVERT(int," + this.idCliente + ") and intervalos.idFactura=CONVERT(int," + this.idFactura + ") and intervalos.intervaloFecha='" + fechaAPagar.ToString("yyyy-MM-dd") + "'");
                        conn.actualizar("intervalos", "intervaloPago", "CONVERT(int," + (cuota - (this.cantidad - cuota)) + ")", "intervalos.idCliente=CONVERT(int," + this.idCliente + ") and intervalos.idFactura=CONVERT(int," + this.idFactura + ") and intervalos.intervaloFecha='" + Convert.ToDateTime(fecha).AddDays(this.periodoPago).ToString("yyyy-MM-dd") + "'");
                        
                        dataGridView1.DataSource = null;

                        cargarDatos();
                    }

                }
                else
                {
                    int _mora = Convert.ToInt32(((Convert.ToSingle(this.mora) / 100) * Convert.ToSingle(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString())));

                    if(MessageBox.Show("Se le aplicara la mora + RD$"+_mora.ToString() + " ¿sedea pagarla?","Mora",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        conn.actualizar("intervalos", "estado", "'PAGADO'", "intervalos.idCliente=CONVERT(int," + this.idCliente + ") and intervalos.idFactura=CONVERT(int," + this.idFactura + ") and intervalos.intervaloFecha='" + fechaAPagar.ToString("yyyy-MM-dd") + "'");
                        conn.actualizar("intervalos", "intervaloPago", "CONVERT(int," + (cuota + _mora) +  ")", "intervalos.idCliente=CONVERT(int," + this.idCliente + ") and intervalos.idFactura=CONVERT(int," + this.idFactura + ") and intervalos.intervaloFecha='" + fechaAPagar.ToString("yyyy-MM-dd") + "'");
                    }
                    else
                    {
                        conn.actualizar("intervalos", "estado", "'PAGADO'", "intervalos.idCliente=CONVERT(int," + this.idCliente + ") and intervalos.idFactura=CONVERT(int," + this.idFactura + ") and intervalos.intervaloFecha='" + fechaAPagar.ToString("yyyy-MM-dd") + "'");
                        conn.actualizar("intervalos", "intervaloPago", "CONVERT(int," + (cuota + _mora) + ")", "intervalos.idCliente=CONVERT(int," + this.idCliente + ") and intervalos.idFactura=CONVERT(int," + this.idFactura + ") and intervalos.intervaloFecha='" + fechaAPagar.AddDays(this.periodoPago).ToString("yyyy-MM-dd") + "'");
                    }

                    MessageBox.Show(Conexion.mensaje);
                    
                    dataGridView1.DataSource = null;

                    cargarDatos();

                    textBox1.Focus();

                }
            }
            else
            {
                MessageBox.Show("Ya esta pago");
            }
                    

        }

        // CARGA LOS DATOS DE LA BASE DE DATOS Y LOS CARGA AL DATAGRIDVIEW
        private void cargarDatos()
        {
            Conexion.ConectarBD.Open();

            SqlDataAdapter dataA = new SqlDataAdapter("select intervalos.intervaloFecha 'fechas', intervalos.intervaloPago 'cuotas RD$', intervalos.estado from intervalos where intervalos.idFactura = " + idFactura, Conexion.ConectarBD);

            DataTable dataT = new DataTable();

            dataA.Fill(dataT);

            dataGridView1.DataSource = dataT;

            Conexion.ConectarBD.Close();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "PAGADO")
                {
                    textBox1.Enabled = false;
                    pagar.Enabled = false;
                } else 
                {
                    textBox1.Enabled = true;
                }
            }


            if (textBox1.Enabled == false)
            {
                MessageBox.Show("El cliente a pagado el prestamo");
            }

        }

    }
}
