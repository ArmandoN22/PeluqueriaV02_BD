using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Frm_Crud_Clientes : Form
    {

        public Frm_Crud_Clientes()
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
            txtCedula.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }

        private void Estadotexto(bool lEstado)
        {
            txtCedula.Enabled = lEstado;
            txtNombre.Enabled = lEstado;
            txtApellido.Enabled = lEstado;
            txtTelefono.Enabled = lEstado;
            txtCorreo.Enabled = lEstado;
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

        }

        private void Formato_clientes()
        {

            dgvClientes.Columns[0].Width = 100;
            dgvClientes.Columns[1].Width = 120;
            dgvClientes.Columns[2].Width = 120;
            dgvClientes.Columns[3].Width = 110;

        }

        private void Mostrar(string cTexto)
        {
            try
            {

                dgvClientes.DataSource = L_Clientes.Mostrar(cTexto);
                Formato_clientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Guardar()
        {
            if (txtCedula.Text == "" || txtNombre.Text == string.Empty || txtApellido.Text == string.Empty || txtTelefono.Text == string.Empty || txtCorreo.Text == string.Empty)
            {
                MessageBox.Show("Faltan Datos por ingresar", "Avisos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else //Guardamos la Informacion
            {
                string Rpta = "";
                try
                {
                    if (Operacion == "Insertar")
                    {
                        E_Clientes oCl = new E_Clientes();
                        //string Rpta = "";
                        oCl.Id = Convert.ToInt32(txtCedula.Text);
                        oCl.Nombre = txtNombre.Text;
                        oCl.Apellido = txtApellido.Text;
                        oCl.Telefono = txtTelefono.Text;
                        oCl.Correo = txtCorreo.Text;
                        Rpta = L_Clientes.Guardar(oCl);
                        if (Rpta == "OK")
                        {
                            Mostrar("%");
                            MessageBox.Show("Los datos han sido guardados correctamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Estado_Botones_Principales(true);
                            this.Estado_Botones_Procesos(false);
                            Estadotexto(false);
                            tbpServicios.SelectedIndex = 0;

                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else if (Operacion == "Editar")
                    {
                        E_Clientes oCl = new E_Clientes();
                        //string Rpta = "";
                        oCl.Id = Convert.ToInt32(txtCedula.Text);
                        oCl.Nombre = txtNombre.Text;
                        oCl.Apellido = txtApellido.Text;
                        oCl.Telefono = txtTelefono.Text;
                        oCl.Correo = txtCorreo.Text;
                        Rpta = L_Clientes.Actualizar(oCl, Cedula.ToString());
                        if (Rpta == "OK")
                        {
                            Operacion = "Insertar";
                            Mostrar("%");
                            MessageBox.Show("Los datos han sido actualizados correctamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Estado_Botones_Principales(true);
                            this.Estado_Botones_Procesos(false);
                            Estadotexto(false);
                            tbpServicios.SelectedIndex = 0;

                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Rpta = ex.Message;
                    MessageBox.Show(Rpta, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
        }

        private void Selecciona_Item()
        {
            if (string.IsNullOrEmpty(dgvClientes.CurrentRow.Cells[0].Value.ToString()))
            {
                MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Cedula = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                txtCedula.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[1].Value);
                txtApellido.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[2].Value);
                txtTelefono.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[3].Value);
                txtCorreo.Text = Convert.ToString(dgvClientes.CurrentRow.Cells[4].Value);
            }

        }

        private void Eliminar()
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                if (string.IsNullOrEmpty(dgvClientes.CurrentRow.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DialogResult result = MessageBox.Show("¿Estás seguro de eliminar?", "Confirmación de eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string Rpta = "";
                        Cedula = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                        Rpta = L_Clientes.Eliminar(Cedula);

                        if (Rpta == "OK")
                        {
                            Mostrar("%");
                            MessageBox.Show("Los datos han sido eliminados correctamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                MessageBox.Show("La tabla esta vacia", "Avisos Del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void Frm_Crud_Clientes_Load(object sender, EventArgs e)
        {
            Mostrar("%");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Mostrar(txtBuscar.Text.Trim());
        }

        private void btn_Nuevo_Click(object sender, EventArgs e)
        {
            //nEstadoguarda = 1; //Nuevo Registro
            LimpiaTexto();
            Estadotexto(true);
            Estado_Botones_Principales(false);
            Estado_Botones_Procesos(true);
            tbpServicios.SelectedIndex = 1;
            txtCedula.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //nEstadoguarda = 0; //Cancelar Registro
            LimpiaTexto();
            Estadotexto(false);
            Estado_Botones_Procesos(false);
            Estado_Botones_Principales(true);
            tbpServicios.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                Operacion = "Editar";
                Selecciona_Item();
                Estadotexto(true);
                Estado_Botones_Principales(false);
                Estado_Botones_Procesos(true);
                tbpServicios.SelectedIndex = 1;
                txtCedula.Focus();

            }
            else
            {
                MessageBox.Show("La tabla esta vacia", "Avisos Del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }
    }
}
