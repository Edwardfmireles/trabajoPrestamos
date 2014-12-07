using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prestamos
{
    class abilitarDessabilitarBotones 
    {
        public programaPrincipal f;

        // CONTRUCTOR DE LA CLASE abilitarDessabilitarBotones EL CUAL RECIBE COMO PARAMETO
        // LA CLASE programaPrincipal PARA ACCEDER A TODOS SUS COMPONENTES
        public abilitarDessabilitarBotones(programaPrincipal formu)
        {
            this.f = formu;

            this.f.groupabono.Visible = false;
            this.f.groupnuevafactura.Visible = false;
            this.f.dropeliminarcliente.Visible = false;
            this.f.dropregistrarClientes.Visible = false;
            this.f.groupactualizarcliente.Visible = false;

            this.f.ClientSize = new System.Drawing.Size(751, 261);
        }

        // METODO PARA DESHABILITAR Y LIMPIAR LOS CAMPOS DEL CONTENEDOR NUEVA FACTURA
        public void deshabilitarLimpiarnuevaFactura() 
        {
          // NUEVA FACTURA
            this.f.nfperiodopago.SelectedIndex = -1;

            this.f.nfnombre.Enabled = false;
            this.f.nfcedula.Enabled = false;
            this.f.nfmonto.Enabled = false;
            this.f.nfperiodopago.Enabled = false;
            this.f.nfmeses.Enabled = false;
            this.f.nfinteres.Enabled = false;
            this.f.nfmora.Enabled = false;
            this.f.nffacturar.Enabled = false;
            this.f.nfCalcularMonto.Enabled = false;

            this.f.nfnumerofactura.Text = "00001";
            this.f.nfnombre.Text = "";
            this.f.nfcedula.Text = "";
            this.f.nfmonto.Text = "";
            this.f.nfperiodopago.Text = "";
            this.f.nfmeses.Text = "";
            this.f.nfinteres.Text = "";
            this.f.nfmora.Text = "";
            this.f.nffechainicial.Text = "";
            this.f.nffechafinal.Text = "";
            this.f.nfMontoTotal.Text = "";

        }

        // METODO PARA LIMPIAR EL CAMPO DE BUSQUEDA DEL CLIENTE DEL CONTENDEDOR ELIMINAR CLIENTE
        public void limpiarEliminarCliente()
        {
            this.f.ecbuscarcliente.Text = "";
        }

        // METODO PARA LIMPIAR LOS CAMPOS DEL CONTENEDOR ACTUALIZAR CLIENTE
        public void limpiarActualizarCliente()
        {
            this.f.acactualizar.Enabled = false;

            this.f.acbuscarcliente.Text = "";
            this.f.acnombre.Text = "";
            this.f.accedula.Text = "";
            this.f.actelefono.Text = "";
            this.f.acdireccion.Text = "";
        }

        public void limpiarNuevoCliente()
        {
            this.f.rcnombre.Text = "";
            this.f.rctelefono.Text = "";
            this.f.rcdirecion.Text = "";
            this.f.rccedula.Text = "";
        }

        public void limpiarAbono()
        {
            this.f.abuscarcliente.Text = "";
        }
    }
}
