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
    public partial class programaPrincipal : Form
    {

        private abilitarDessabilitarBotones adb;
        private int quinsenalMensualAnual;
        private int calculoquincenasMensualidadAnual;
        private int meses;
        private List<DateTime> fechas = new List<DateTime>();
        private DateTime[] fechasArray;
        private int cuotas;
        private DateTime dFechaInicial = DateTime.Now;
        private int facturaNumero = 0;
        private string periodoPago;
        public int clienteId;

        public programaPrincipal()
        {
            InitializeComponent();
            adb = new abilitarDessabilitarBotones(this);
            generarNuevaFactura();
        }

        private void programaPrincipal_Load(object sender, EventArgs e)
        {

            dropregistrarClientes.Visible = false;
            dropeliminarcliente.Visible = false;
            groupactualizarcliente.Visible = false;
            groupabono.Visible = false;
            groupnuevafactura.Visible = false;
        }

        private void generarNuevaFactura()
        {

            nffecha.Text = Convert.ToString(dFechaInicial.Day + "/" + dFechaInicial.Month + "/" + dFechaInicial.Year);
            nffechainicial.Text = nffecha.Text;

            Conexion conn = new Conexion();

            this.facturaNumero = (conn.obtenerUltimoId("prestamos.idPrestamo", "prestamos", "") + 1);

            if (this.facturaNumero < 9)
            {
                this.nfnumerofactura.Text = "00000" + this.facturaNumero.ToString();
            }
            else if (this.facturaNumero < 100)
            {
                this.nfnumerofactura.Text = "0000" + this.facturaNumero.ToString();
            }
            else if (this.facturaNumero < 9999)
            {
                this.nfnumerofactura.Text = "000" + this.facturaNumero.ToString();
            }
            else if (this.facturaNumero < 99999)
            {
                this.nfnumerofactura.Text = "0" + this.facturaNumero.ToString();
            }
            else
            {
                this.nfnumerofactura.Text = this.facturaNumero.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void nuevoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dropregistrarClientes.Visible = true;
            dropeliminarcliente.Visible = false;
            groupactualizarcliente.Visible = false;
            groupabono.Visible = false;
            groupnuevafactura.Visible = false;

            this.ClientSize = new System.Drawing.Size(636, dropregistrarClientes.Height + 20);
        }

        private void eliminarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dropeliminarcliente.Visible = true;
            dropregistrarClientes.Visible = false;
            groupactualizarcliente.Visible = false;
            groupabono.Visible = false;
            groupnuevafactura.Visible = false;
            llenarDataGridView._llenarDataGridView(dataGridEliminarCliente, "select * from clientes");
            this.ClientSize = new System.Drawing.Size(583, dropeliminarcliente.Height + 20);

        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dropregistrarClientes.Visible = false;
            dropeliminarcliente.Visible = false;
            groupactualizarcliente.Visible = true;
            groupabono.Visible = false;
            groupnuevafactura.Visible = false;

            this.ClientSize = new System.Drawing.Size(606, groupactualizarcliente.Height + 20);
            
            if (acDataGridView.Rows.Count > 0)
            {
                acDataGridView.ClearSelection();
                acDataGridView.Rows[0].Cells[0].Selected = false;
            }

            llenarDataGridView._llenarDataGridView(acDataGridView, "select * from clientes");
            this.adb.limpiarActualizarCliente();
        }

        private void nuevaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dropregistrarClientes.Visible = false;
            dropeliminarcliente.Visible = false;
            groupactualizarcliente.Visible = false;
            groupabono.Visible = false;
            groupnuevafactura.Visible = true;
            this.nfbuscarcliente.Focus();
            this.ClientSize = new System.Drawing.Size(751, groupnuevafactura.Height + 20);


        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dropregistrarClientes.Visible = false;
            dropeliminarcliente.Visible = false;
            groupactualizarcliente.Visible = false;
            groupabono.Visible = true;
            groupnuevafactura.Visible = false;

            this.ClientSize = new System.Drawing.Size(751, groupabono.Height + 20);

            llenarDataGridView._llenarDataGridView(dataGridabono, "SELECT clientes.idCliente 'id Cliente', prestamos.idPrestamo 'id Prestamo', nombre, cedula, monto, cuotas 'cuotas RD$', interes 'interes %', periodoPago 'Periodo de Pago', moraPrestamo 'mora RD$', fechaInicial 'fecha inicial', fechaFinal 'fecha final' FROM dbo.clientes,dbo.facturacion, prestamos where clientes.idCliente=facturacion.idCliente and facturacion.idPrestamo=prestamos.idPrestamo ORDER BY prestamos.fechaInicial ASC");
        }

        private void nfbuscarcliente_Click(object sender, EventArgs e)
        {
            buscarCliente bc = new buscarCliente(this);
            bc.ShowDialog();
        }

        private void nfnombre_TextChanged(object sender, EventArgs e)
        {
            if (nfnombre.Text.Length > 0)
            {
                nfmonto.Enabled = true;
                nfperiodopago.Enabled = true;
                nfmeses.Enabled = true;
                nfinteres.Enabled = true;
                nfmora.Enabled = true;
                nfCalcularMonto.Enabled = true;
            }
        }

        private void nfmonto_TextChanged(object sender, EventArgs e)
        {
            TextBox sen = (TextBox)sender;


            if (!int.TryParse(sen.Text.ToString().Trim(), out this.meses) && this.meses < 1)
            {
                sen.Text = "";
                nfcuotas.Text = "";

            }


            nfCalcularMonto.Visible = true;
            nfMontoTotal.Text = "";
            nfcuotas.Text = "";
        }

        private void nfperiodopago_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (nfperiodopago.SelectedIndex)
            {
                case 0:
                    this.quinsenalMensualAnual = 15; // 15 días
                    nfcambiomeses.Text = "Quinsenas";
                    nfmeses.Text = "";
                    this.periodoPago = "QUINCENAL";
                    nfmeses.Focus();
                    break;
                case 1:
                    this.quinsenalMensualAnual = 30; // 30 días
                    nfcambiomeses.Text = "Meses";
                    nfmeses.Text = "";
                    this.periodoPago = "MENSUAL";
                    nfmeses.Focus();
                    break;
                case 2:
                    this.quinsenalMensualAnual = 365; // 365 días
                    nfcambiomeses.Text = "Año(s)";
                    nfmeses.Text = "";
                    this.periodoPago = "ANUAL";
                    nfmeses.Focus();
                    break;
            }
        }

        private void nfmeses_TextChanged(object sender, EventArgs e)
        {
            TextBox sen = (TextBox)sender;

            if (!int.TryParse(sen.Text.ToString().Trim(), out this.meses) && this.meses < 1 || this.meses > 99 || this.meses == 0)
            {
                sen.Text = "";
                nffechafinal.Text = "";
                this.fechasArray = new DateTime[0];
            }
            else
            {

                nfcuotas.Text = "";
                generarfechas(this.meses);

            }


        }

        private void nfinteres_TextChanged(object sender, EventArgs e)
        {
            TextBox sen = (TextBox)sender;

            if (!int.TryParse(sen.Text.ToString().Trim(), out this.meses) && this.meses < 1 || this.meses > 50 || this.meses == 0)
            {
                sen.Text = "";

            }


            nfcuotas.Text = "";
            nfCalcularMonto.Visible = true;
            nfMontoTotal.Text = "";
        }

        private void nfmora_TextChanged(object sender, EventArgs e)
        {
            TextBox sen = (TextBox)sender;

            if (!int.TryParse(sen.Text.ToString().Trim(), out this.meses) && this.meses < 1)
            {
                sen.Text = "";
            }

            nfCalcularMonto.Visible = true;
            nfMontoTotal.Text = "";
        }


        private void nfCalcularMonto_Click(object sender, EventArgs e)
        {
            if (validarCamposVacios() == true)
            {
                nfCalcularMonto.Visible = false;
                genearCuotasYTotal();
                nffacturar.Enabled = true;
            }
        }


        private void nffacturar_Click(object sender, EventArgs e)
        {
            if (validarCamposVacios() == true)
            {

                Conexion conn = new Conexion();
                string cadena = "intervalos(idCliente,intervaloFecha,intervaloPago)" +
                                    "( CONVERT(int," + clienteId + "), CONVERT(DATETIME," + this.fechasArray[fechas.Count - 1].ToString("yyyy-MM-dd") + "), CONVERT(int," + this.cuotas + ") )";

                // MessageBox.Show(cadena);
                if (conn.insertar("prestamos(monto,interes,cuotas,periodoPago,moraPrestamo,fechaInicial,fechaFinal)",
                                            "( CONVERT(int," + nfmonto.Text.Trim() +
                                            "), CONVERT(int," + nfinteres.Text.Trim() + "), " +
                                            "CONVERT(int," + this.cuotas + "), '" +
                                            this.periodoPago +
                                            "', CONVERT(int," + nfmora.Text.Trim() + "), SYSDATETIME(),'" +
                                            this.fechasArray[fechas.Count - 1].ToString("yyyy-MM-dd") + "' )", "") == true)
                {


                    if (conn.insertar("facturacion (idCliente, idPrestamo)",
                                   "(CONVERT(int," + clienteId + "), CONVERT(int," + facturaNumero + "))", ""))
                    {
                        for (int i = 0; i < fechas.Count; i++)
                        {
                            if (conn.insertar("intervalos(idCliente,idFactura,intervaloFecha,intervaloPago)",
                                    "( CONVERT(int," + clienteId + "), CONVERT(int," + facturaNumero + "), '" + fechasArray[i].ToString("yyyy-MM-dd") + "', CONVERT(int," + this.cuotas + ") )", "") == false)
                            {
                                MessageBox.Show("No se agrego el intervalo " + i + " fecha " + fechas[i].ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                MessageBox.Show(Conexion.mensaje);
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("No se hizo la Factura");
                    }
                }
                else
                {
                    MessageBox.Show(Conexion.mensaje);
                }


                MessageBox.Show("Factura Realizada");
                this.adb.deshabilitarLimpiarnuevaFactura();
                generarNuevaFactura();

            }
        }

        private void nfcancelar_Click(object sender, EventArgs e)
        {
            groupnuevafactura.Visible = false;
            adb.deshabilitarLimpiarnuevaFactura();
            nfperiodopago.SelectedIndex = -1;

            this.ClientSize = new System.Drawing.Size(751, 261);
        }

        private bool validarCamposVacios()
        {
            if (nfmonto.Text.Length > 0)
            {
                if (this.quinsenalMensualAnual == 15 || this.quinsenalMensualAnual == 30 || this.quinsenalMensualAnual == 365)
                {
                    if (nfmeses.Text.Length > 0)
                    {
                        if (nfinteres.Text.Length > 0)
                        {
                            if (nfmora.Text.Length > 0)
                            {
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Debe de ingresar la mora");
                                nfmora.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe de ingresar el interés");
                            nfinteres.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe de especificar el tiempo");
                        nfmeses.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Debe de Elegir un periodo de pago");
                    nfperiodopago.Focus();
                }
            }
            else
            {
                MessageBox.Show("Debe de ingresar un monto");
                nfmonto.Focus();
            }

            return false;

        }

        private void generarfechas(int parse)
        {
            this.fechasArray = new DateTime[parse];


            int j = 0;


            fechas.Clear();

            if (this.quinsenalMensualAnual == 15)
            {
                this.calculoquincenasMensualidadAnual = 15 * parse;


                for (int i = 1; i <= parse; i++)
                {
                    fechas.Add(DateTime.Today.AddDays(i * 15));
                }


                foreach (var item in fechas)
                {
                    //MessageBox.Show(Convert.ToString(item.Day + "/" + item.Month + "/" + item.Year));


                    this.fechasArray[j] = Convert.ToDateTime(item);
                    j++;
                }


                nffechafinal.Text = Convert.ToString(this.fechasArray[fechas.Count - 1].Day + "/" + this.fechasArray[fechas.Count - 1].Month + "/" + this.fechasArray[fechas.Count - 1].Year);



            }
            else if (this.quinsenalMensualAnual == 30)
            {
                this.calculoquincenasMensualidadAnual = 30 * parse;

                for (int i = 1; i <= parse; i++)
                {
                    fechas.Add(DateTime.Today.AddDays(i * 30));
                }

                foreach (var item in fechas)
                {
                    //MessageBox.Show(Convert.ToString(item.Day + "/" + item.Month + "/" + item.Year));


                    this.fechasArray[j] = Convert.ToDateTime(item);
                    j++;
                }


                nffechafinal.Text = Convert.ToString(this.fechasArray[fechas.Count - 1].Day + "/" + this.fechasArray[fechas.Count - 1].Month + "/" + this.fechasArray[fechas.Count - 1].Year);

            }
            else if (this.quinsenalMensualAnual == 365)
            {
                this.calculoquincenasMensualidadAnual = 12 * parse;

                for (int i = 1; i <= parse; i++)
                {
                    fechas.Add(DateTime.Today.AddDays(i * 365));
                }

                foreach (var item in fechas)
                {
                    this.fechasArray[j] = Convert.ToDateTime(item);
                    j++;
                }


                nffechafinal.Text = Convert.ToString(this.fechasArray[fechas.Count - 1].Day + "/" + this.fechasArray[fechas.Count - 1].Month + "/" + this.fechasArray[fechas.Count - 1].Year);
            }
        }

        public void genearCuotasYTotal()
        {

            float monto = float.Parse(nfmonto.Text);
            float interes = (float.Parse(nfinteres.Text) / 100) * monto;
            float montoT = monto + interes;
            float cuota = montoT / float.Parse(nfmeses.Text);

            this.cuotas = Convert.ToInt32(Math.Floor(cuota));

            nfcuotas.Text = Convert.ToString((Math.Floor(cuota))) + ".00";

            nfMontoTotal.Text = Convert.ToString((Math.Floor(montoT))) + ".00";

        }

        private void ecbuscarcliente_TextChanged(object sender, EventArgs e)
        {
            seleccionarLineaDataGridView(sender, dataGridEliminarCliente);
        }

        private void dataGridEliminarCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            string nombre = dataGridEliminarCliente.Rows[e.RowIndex].Cells[1].Value.ToString();
            int idCliente = Convert.ToInt16(dataGridEliminarCliente.Rows[e.RowIndex].Cells[0].Value.ToString());


            metodoEliminarClienteSeleccionado(nombre, idCliente);

        }

        private void metodoEliminarClienteSeleccionado(string nombre, int idCliente)
        {
            if (MessageBox.Show("Desea eliminar el usuario " + nombre, "Eliminar Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Conexion con = new Conexion();

                    Conexion.ConectarBD.Open();

                    SqlCommand intervalos = new SqlCommand("delete from intervalos where idCliente=" + idCliente, Conexion.ConectarBD);

                    intervalos.ExecuteNonQuery();

                    SqlCommand clientes = new SqlCommand("delete from clientes where idCliente=" + idCliente, Conexion.ConectarBD);

                    clientes.ExecuteNonQuery();


                    dataGridEliminarCliente.Rows.RemoveAt(dataGridEliminarCliente.CurrentRow.Index);

                }
                catch (SqlException sqle)
                {
                    MessageBox.Show(sqle.ToString());
                }


                Conexion.ConectarBD.Close();
            }
        }

        private void eccancelar_Click(object sender, EventArgs e)
        {
            adb.limpiarEliminarCliente();
            dropeliminarcliente.Visible = false;

            this.ClientSize = new System.Drawing.Size(751, 261);

        }

        private void eceliminar_Click(object sender, EventArgs e)
        {
            string nombre = dataGridEliminarCliente.Rows[dataGridEliminarCliente.CurrentRow.Index].Cells[1].Value.ToString();
            int idCliente = Convert.ToInt16(dataGridEliminarCliente.Rows[dataGridEliminarCliente.CurrentRow.Index].Cells[0].Value.ToString());


            metodoEliminarClienteSeleccionado(nombre, idCliente);
        }

        private void acbuscarcliente_TextChanged(object sender, EventArgs e)
        {
            seleccionarLineaDataGridView(sender, acDataGridView);
        }

        private void aceditar_Click(object sender, EventArgs e)
        {
            llenarCamposActualizarCliente();
        }

        public void seleccionarLineaDataGridView(object sender, DataGridView datagrid)
        {
            TextBox sen = (TextBox)sender;

            if (sen.Text.ToString().Trim().Length > 0 && datagrid.Rows.Count > 0)
            {
                for (int i = 0; i < datagrid.Rows.Count; i++)
                {
                    for (int j = 0; j < datagrid.Rows[i].Cells.Count; j++)
                    {
                        if (datagrid.Rows[i].Cells[j].Value.ToString().Contains(sen.Text.ToLower()))
                        {
                            datagrid.Rows[i].Cells[j].Selected = true;
                        }
                    }
                }
            }
        }

        private void acDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            llenarCamposActualizarCliente();
        }

        private void llenarCamposActualizarCliente()
        {
            if (acDataGridView.Rows.Count > 0)
            {
                this.acnombre.Text = acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[1].Value.ToString();
                this.accedula.Text = acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[2].Value.ToString();
                this.acdireccion.Text = acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[3].Value.ToString();
                this.actelefono.Text = acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[4].Value.ToString();
            }

        }

        private void acactualizar_Click(object sender, EventArgs e)
        {

            bool dir = false;
            bool tel = false;

            Conexion con = new Conexion();

            try
            {


                if (acdireccion.Text.ToLower().Trim() != acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[3].Value.ToString().ToLower().Trim())
                {
                    //MessageBox.Show(acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[3].Value.ToString());
                    dir = con.actualizar("clientes", "direccion", acdireccion.Text.Trim(), "clientes.idCliente=CONVERT(int," + acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[0].Value.ToString() + ")");
                    MessageBox.Show(Conexion.mensaje);
                }

                if (actelefono.Text.ToLower().Trim() != acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[4].Value.ToString().ToLower().Trim())
                {

                    tel = con.actualizar("clientes", "telefono", actelefono.Text.Trim(), "clientes.idCliente=CONVERT(int," + acDataGridView.Rows[acDataGridView.CurrentRow.Index].Cells[0].Value.ToString() + ")");
                }


            }
            catch (SqlException)
            {
                MessageBox.Show("Error al actualizar Datos");
            }



            if (dir == true || tel == true)
            {
                MessageBox.Show("Datos Actualizados");


                adb.limpiarActualizarCliente();

            }

        }

        private void acnombre_TextChanged(object sender, EventArgs e)
        {
            if (acnombre.Text.Length > 0)
            {
                acactualizar.Enabled = true;
            }
            else
            {
                acactualizar.Enabled = false;
            }
        }

        private void accancelar_Click(object sender, EventArgs e)
        {
            groupactualizarcliente.Visible = false;
            adb.limpiarActualizarCliente();

            this.ClientSize = new System.Drawing.Size(751, 261);
        }

        private void nfCalcularMonto_VisibleChanged(object sender, EventArgs e)
        {
            if (nfCalcularMonto.Visible == true)
            {
                nffacturar.Enabled = false;
            }
        }

        private void rccedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 13 || e.KeyChar == 8 || e.KeyChar.ToString() == "-")
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show(e.KeyChar.ToString());
                e.Handled = true;
            }
        }

        private void rctelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 13 || e.KeyChar == 8 || e.KeyChar.ToString() == "-")
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show(e.KeyChar.ToString());
                e.Handled = true;
            }
        }

        private void rcaceptar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(rctelefono.Text);
            if (rcnombre.Text.Trim().Length > 2)
            {
                if (rcdirecion.Text.Trim().Length > 5)
                {
                    if (rccedula.Text.Trim().Length == 13)
                    {
                        if (rctelefono.Text.Trim().Length == 12)
                        {
                            Conexion conn = new Conexion();

                            if (conn.insertar("clientes(nombre,cedula,direccion,telefono)", "('" + rcnombre.Text.Trim() + "','" + rccedula.Text.Trim() + "','" + rcdirecion.Text.Trim() + "','" + rctelefono.Text.Trim() + "')", "") == true)
                            {
                                MessageBox.Show("Cliente guardado");
                                this.adb.limpiarNuevoCliente();
                            }
                            else
                            {
                                MessageBox.Show(Conexion.mensaje);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tamaño de teléfono incorrecto");
                            rctelefono.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tamaño de cédula incorrecto");
                        rccedula.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Dirección muy corta");
                    rcdirecion.Focus();
                }
            }
            else
            {
                MessageBox.Show("Nombre muy corto");
                rcnombre.Focus();
            }
        }

        private void rccancelar_Click(object sender, EventArgs e)
        {
            dropregistrarClientes.Visible = false;
            this.adb.limpiarNuevoCliente();
        }

        private void abuscarcliente_TextChanged(object sender, EventArgs e)
        {
            seleccionarLineaDataGridView(sender, dataGridabono);
        }

        private void acancelar_Click(object sender, EventArgs e)
        {
            groupabono.Visible = false;
            adb.limpiarAbono();

            this.ClientSize = new System.Drawing.Size(751, 261);
        }

        private void averregistro_Click(object sender, EventArgs e)
        {
            if (dataGridabono.Rows.Count > 0)
            {
                int idC = Convert.ToInt32(dataGridabono.Rows[dataGridabono.CurrentRow.Index].Cells[0].Value.ToString());
                int idP = Convert.ToInt32(dataGridabono.Rows[dataGridabono.CurrentRow.Index].Cells[1].Value.ToString());
                string nombre = dataGridabono.Rows[dataGridabono.CurrentRow.Index].Cells[2].Value.ToString();

                Abonar ab = new Abonar(idC, idP, nombre);

                ab.ShowDialog();
            }
            

        }

        private void dataGridabono_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridabono.Rows.Count > 0)
            {
                int idC = Convert.ToInt32(dataGridabono.Rows[dataGridabono.CurrentRow.Index].Cells[0].Value.ToString());
                int idP = Convert.ToInt32(dataGridabono.Rows[dataGridabono.CurrentRow.Index].Cells[1].Value.ToString());
                string nombre = dataGridabono.Rows[dataGridabono.CurrentRow.Index].Cells[2].Value.ToString();

                Abonar ab = new Abonar(idC, idP, nombre);

                ab.ShowDialog();
            }
        }

    }
}