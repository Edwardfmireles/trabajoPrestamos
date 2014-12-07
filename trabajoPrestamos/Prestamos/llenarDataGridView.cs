using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Prestamos
{
    public static class llenarDataGridView
    {

        public static void _llenarDataGridView(DataGridView _dataGridView, string query)
        {

            DataGridView dataGrid = _dataGridView;

            Conexion conn = new Conexion();

            Conexion.ConectarBD.Open();


           // SqlConnection c = new SqlConnection("Data Source=EDWARD-D;Initial Catalog=Prestamista;Integrated Security=True");

            //Data Source=EDWARD-D;Initial Catalog=Prestamista;Integrated Security=True

            

            DataTable _dataTable = new DataTable();
            
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query,Conexion.ConectarBD);


            dataAdapter.Fill(_dataTable);
            
            dataGrid.DataSource = _dataTable;
            

            Conexion.ConectarBD.Close();
            





        }



    }
}
