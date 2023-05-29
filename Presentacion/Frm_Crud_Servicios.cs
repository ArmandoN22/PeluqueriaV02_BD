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
    public partial class Frm_Crud_Servicios : Form
    {

        L_Servicios servicios = new L_Servicios();

        public Frm_Crud_Servicios()
        {
            InitializeComponent();
        }

        #region "Mis Variables"
        int nCodigo_pr = 0;
        int nEstadoguarda = 0;
        string Operacion = "Insertar";
        int IdSer;
        #endregion

        #region Mis Metodos
        private void LimpiaTexto()
        {
            txtNombre.Clear();
            txtPrecio.Clear();
        }

        private void Estadotexto(bool lEstado)
        {
            txtNombre.Enabled = lEstado;
            txtPrecio.Enabled = lEstado;

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

        private void Mostrar(string cTexto)
        {
            try
            {

                dgvServicios.DataSource = L_Servicios.Mostrar(cTexto);
                //this.Formato_ca();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Guardar()
        {
            if (txtNombre.Text == string.Empty || txtPrecio.Text == string.Empty)
            {
                MessageBox.Show("Faltan Datos por ingresar", "Avisos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else //Guardamos la Informacion
            {
                if (Operacion == "Insertar")
                {
                    E_Servicios oSer = new E_Servicios();
                    string Rpta = "";
                    oSer.Id_Servicio = nCodigo_pr;
                    oSer.Nombre = txtNombre.Text;
                    oSer.Precio = float.Parse(txtPrecio.Text.Trim());
                    Rpta = L_Servicios.Guardar(oSer);
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
                    E_Servicios oSer = new E_Servicios();
                    string Rpta = "";
                    oSer.Id_Servicio = IdSer;
                    oSer.Nombre = txtNombre.Text;
                    oSer.Precio = float.Parse(txtPrecio.Text.Trim());
                    Rpta = L_Servicios.Actualizar(oSer);
                    Operacion = "Insertar";
                    if (Rpta == "OK")
                    {
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
        }

        private void Selecciona_Item()
        {
            if (string.IsNullOrEmpty(dgvServicios.CurrentRow.Cells[0].Value.ToString()))
            {
                MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                IdSer = Convert.ToInt32(dgvServicios.CurrentRow.Cells[0].Value);
                txtNombre.Text = Convert.ToString(dgvServicios.CurrentRow.Cells[1].Value);
                txtPrecio.Text = Convert.ToString(dgvServicios.CurrentRow.Cells[2].Value);
            }

        }

        private void Eliminar()
        {
            if (dgvServicios.SelectedRows.Count > 0)
            {
                if (string.IsNullOrEmpty(dgvServicios.CurrentRow.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DialogResult result = MessageBox.Show("¿Estás seguro de eliminar?", "Confirmación de eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string Rpta = "";
                        IdSer = Convert.ToInt32(dgvServicios.CurrentRow.Cells[0].Value);
                        Rpta = L_Servicios.Eliminar(IdSer);

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

        private void btn_Nuevo_Click(object sender, EventArgs e)
        {
            //nEstadoguarda = 1; //Nuevo Registro
            LimpiaTexto();
            Estadotexto(true);
            Estado_Botones_Principales(false);
            Estado_Botones_Procesos(true);
            tbpServicios.SelectedIndex = 1;
            txtNombre.Focus();
        }

        private void Frm_Crud_Servicios_Load(object sender, EventArgs e)
        {
            Mostrar("%");
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
            //LimpiaTexto();
            Guardar();
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            LimpiaTexto();
            this.Close();
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            if (dgvServicios.SelectedRows.Count > 0)
            {
                Operacion = "Editar";
                Selecciona_Item();
                Estadotexto(true);
                Estado_Botones_Principales(false);
                Estado_Botones_Procesos(true);
                tbpServicios.SelectedIndex = 1;
                txtNombre.Focus();

            }
            else
            {
                MessageBox.Show("La tabla esta vacia", "Avisos Del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            LimpiaTexto();
            Eliminar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Mostrar(txtBuscar.Text.Trim());
        }
    }
}
