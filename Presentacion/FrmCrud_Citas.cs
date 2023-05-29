using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Logica;

namespace Presentacion
{
    public partial class FrmCrud_Citas : Form
    {
        public FrmCrud_Citas()
        {
            InitializeComponent();
        }

        #region "Mis Variables"
        int nCodigo_pr = 0;
        int nEstadoguarda = 0;
        string Operacion = "Insertar";
        int Cedula;
        #endregion

        #region "Mis Metodos"

        private void LimpiaTexto()
        {
            
            dtpFecha.Text = string.Empty;
            cmbHoras.Text = "";
            cmbEmpleado.Text = "";
            txtBuscarCliente.Clear();
            cmbClientes.Text = "";
        }

        private void Estadotexto(bool lEstado)
        {
            dtpFecha.Enabled = lEstado;
            cmbHoras.Enabled = lEstado;
            cmbEmpleado.Enabled = lEstado;
            txtBuscarCliente.Enabled = lEstado;
            cmbClientes.Enabled = lEstado;
        }

        private void Estado_Botones_Principales(bool lEstado)
        {
            this.btn_Nuevo.Enabled = lEstado;
            this.btn_Actualizar.Enabled = lEstado;
            this.btn_Eliminar.Enabled = lEstado;
            this.btn_Reporte.Enabled = lEstado;
            this.btn_Salir.Enabled = lEstado;
        }

        private void Estado_Botones_Procesos(bool lEstado)
        {
            this.btnCancelar.Enabled = lEstado;
            this.btnGuardar.Enabled = lEstado;
            this.btnBuscarCliente.Enabled = lEstado;
        }

        private void Listado_em()
        {
            L_Citas Logica = new L_Citas();
            cmbEmpleado.DataSource = L_Citas.listar_em();
            cmbEmpleado.ValueMember = "Cedula";
            cmbEmpleado.DisplayMember = "NombreCompleto";
        }

        private void Listado_cl(string cTexto)
        {
            L_Citas Logica = new L_Citas();
            cmbClientes.DataSource = L_Citas.listar_cl(cTexto);
            cmbClientes.ValueMember = "Cedula";
            cmbClientes.DisplayMember = "NombreCompleto";
        }

        private void Formato_citas()
        {
          
            dgvCitas.Columns[3].Visible = false;
            dgvCitas.Columns[4].Visible = false;

        }

        private void Mostrar(string cTexto)
        {
            try
            {

                dgvCitas.DataSource = L_Citas.Mostrar(cTexto);
                Formato_citas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        #endregion

        private void FrmCrud_Citas_Load(object sender, EventArgs e)
        {
            Mostrar("%");
            Listado_cl("%");
            Listado_em();
        }

        private void btn_Nuevo_Click(object sender, EventArgs e)
        {
            LimpiaTexto();
            Estadotexto(true);
            Estado_Botones_Principales(false);
            Estado_Botones_Procesos(true);
            tbpCitas.SelectedIndex = 1;
            dtpFecha.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiaTexto();
            Estadotexto(false);
            Estado_Botones_Procesos(false);
            Estado_Botones_Principales(true);
            tbpCitas.SelectedIndex = 0;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            Listado_cl(txtBuscarCliente.Text.Trim());
        }
    }
}
