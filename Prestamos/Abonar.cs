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
        public Abonar(int idCliente, int idFactura, string nombre) 
        {
            InitializeComponent();

            this.Name = nombre;

            Conexion conn = new Conexion();
            try
            {
                Conexion.ConectarBD.Open();

                SqlDataAdapter dataA = new SqlDataAdapter("select intervalos.intervaloFecha 'fechas', intervalos.intervaloPago 'cuotas RD$', intervalos.estado from intervalos where intervalos.idFactura = " + idFactura, Conexion.ConectarBD);

                DataTable dataT = new DataTable();

                dataA.Fill(dataT);

                dataGridView1.DataSource = dataT;

            }
            catch (SqlException e)
            {

                MessageBox.Show(e.ToString());
             
            }
            

        }


    }
}
