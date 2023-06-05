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
            clbServicios.ItemCheck += clbServicios_ItemCheck;

        }

        L_Citas l_Citas = new L_Citas();
        List<E_Servicios> serviciosSeleccionados = new List<E_Servicios>();
        //List<float> preciosServicios = L_Citas.ObtenerPrecios(); // utilizaba esta lista con el metodo total pero no me funciono

        #region "Mis Variables"
        int nCodigo_pr = 0;
        int nEstadoguarda = 0;
        string Operacion = "Insertar";
        int IdCi;
        #endregion

        #region "Mis Metodos"

        private void LimpiaTexto()
        {
            
            dtpFecha.Text = string.Empty;
            cmbHoras.Text = "";
            cmbEmpleado.Text = "";
            txtBuscarCliente.Clear();
            cmbClientes.Text = "";
            txtTotal.Clear();
            clbServicios.ClearSelected();
        }

        private void DeseleccionarElementos()
        {
            for (int i = 0; i < clbServicios.Items.Count; i++)
            {
                clbServicios.SetItemChecked(i, false);
            }
        }

        private void Estadotexto(bool lEstado)
        {
            dtpFecha.Enabled = lEstado;
            cmbHoras.Enabled = lEstado;
            cmbEmpleado.Enabled = lEstado;
            txtBuscarCliente.Enabled = lEstado;
            cmbClientes.Enabled = lEstado;
            clbServicios.Enabled = lEstado;
            txtTotal.Enabled = lEstado;
        }

        private void Estado_Botones_Principales(bool lEstado)
        {
            this.btn_Nuevo.Enabled = lEstado;
            this.btn_Actualizar.Enabled = lEstado;
            this.btn_Eliminar.Enabled = lEstado;

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

        private void Listado_sr()
        {
            L_Citas Logica = new L_Citas();
            clbServicios.DataSource = L_Citas.listar_sr();
            clbServicios.ValueMember = "ID_SERVICIO";
            clbServicios.DisplayMember = "NOMBREPRECIO";
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


        private void Guardar()
        {
            if (dtpFecha.Text == string.Empty || cmbHoras.Text == string.Empty || cmbEmpleado.Text == string.Empty || cmbClientes.Text == string.Empty || clbServicios.CheckedIndices.Count == 0)
            {
                MessageBox.Show("Faltan Datos por ingresar", "Avisos del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else //Guardamos la Informacion
            {
                if (Operacion == "Insertar")
                {
                    E_Citas oCi = new E_Citas();
                    string Rpta = "";
                    oCi.Id_Cita = nCodigo_pr;
                    oCi.FechaCita = DateTime.Parse(dtpFecha.Text);
                    oCi.HoraCita = cmbHoras.Text;
                    oCi.Id_Empleado = Convert.ToInt32(cmbEmpleado.SelectedValue); ;
                    oCi.Id_Cliente = Convert.ToInt32(cmbClientes.SelectedValue); ;
                    List<int> serviciosSeleccionados = new List<int>();
                    foreach (var item in clbServicios.CheckedItems)
                    {
                        DataRowView row = item as DataRowView;
                        int idServicio = Convert.ToInt32(row["ID_SERVICIO"]);
                        serviciosSeleccionados.Add(idServicio);
                    }

                    //L_Citas.GuardarCita_Servicios(serviciosSeleccionados);
                    Rpta = L_Citas.Guardar(oCi, serviciosSeleccionados);
                    if (Rpta == "OK")
                    {
                        Mostrar("%");
                        MessageBox.Show("Los datos han sido guardados correctamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Estado_Botones_Principales(true);
                        this.Estado_Botones_Procesos(false);
                        Estadotexto(false);
                        tbpCitas.SelectedIndex = 0;

                    }
                    else
                    {
                        MessageBox.Show(Rpta, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (Operacion == "Editar")
                {
                    E_Servicios oSer = new E_Servicios();
                    E_Citas oCi = new E_Citas();
                    string Rpta = "";
                    oCi.Id_Cita = IdCi;
                    oCi.FechaCita = DateTime.Parse(dtpFecha.Text);
                    oCi.HoraCita = cmbHoras.Text;
                    oCi.Id_Empleado = Convert.ToInt32(cmbEmpleado.SelectedValue); ;
                    oCi.Id_Cliente = Convert.ToInt32(cmbClientes.SelectedValue); ;
                    List<int> serviciosSeleccionados = new List<int>();
                    foreach (var item in clbServicios.CheckedItems)
                    {
                        DataRowView row = item as DataRowView;
                        int idServicio = Convert.ToInt32(row["ID_SERVICIO"]);
                        serviciosSeleccionados.Add(idServicio);
                    }

                    //L_Citas.GuardarCita_Servicios(serviciosSeleccionados);
                    Rpta = L_Citas.Actualizar(oCi, serviciosSeleccionados);

                    if (Rpta == "OK")
                    {
                        Mostrar("%");
                        MessageBox.Show("Los datos han sido actualizados correctamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Estado_Botones_Principales(true);
                        this.Estado_Botones_Procesos(false);
                        Estadotexto(false);
                        tbpCitas.SelectedIndex = 0;

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
            if (string.IsNullOrEmpty(dgvCitas.CurrentRow.Cells[0].Value.ToString()))
            {
                MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                IdCi = Convert.ToInt32(dgvCitas.CurrentRow.Cells[0].Value);
                dtpFecha.Text = Convert.ToString(dgvCitas.CurrentRow.Cells[1].Value);
                cmbHoras.Text = Convert.ToString(dgvCitas.CurrentRow.Cells[2].Value);
                cmbEmpleado.Text = Convert.ToString(dgvCitas.CurrentRow.Cells[5].Value);
                cmbClientes.Text = Convert.ToString(dgvCitas.CurrentRow.Cells[6].Value);
               
            }

        }

        private void selecionar_citas_servicios()
        {
            if (dgvCitas.SelectedRows.Count > 0)
            {
                // Obtén el ID de la cita seleccionada
                string idCita = Convert.ToString(dgvCitas.SelectedRows[0].Cells[0].Value);

                // Obtén los ID de servicios asociados a la cita seleccionada
                L_Citas l_Citas = new L_Citas();
                List<string> serviciosAsociados = l_Citas.ObtenerIdsServiciosAsociados(idCita);

                // Marca los servicios correspondientes en el CheckedListBox
                for (int i = 0; i < clbServicios.Items.Count; i++)
                {
                    DataRowView rowView = (DataRowView)clbServicios.Items[i];
                    string idServicio = Convert.ToString(rowView[clbServicios.ValueMember]);
                    clbServicios.SetItemChecked(i, serviciosAsociados.Contains(idServicio));
                    //string idServicio = Convert.ToString(clbServicios.Items[i]);
                    //clbServicios.SetItemChecked(i, serviciosAsociados.Contains(idServicio));
                }
            }
        }

        private void Eliminar()
        {
            if (dgvCitas.SelectedRows.Count > 0)
            {
                if (string.IsNullOrEmpty(dgvCitas.CurrentRow.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DialogResult result = MessageBox.Show("¿Estás seguro de eliminar esta Cita?", "Confirmación de eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string Rpta = "";
                        IdCi = Convert.ToInt32(dgvCitas.CurrentRow.Cells[0].Value);
                        Rpta = L_Citas.Eliminar(IdCi);

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

        public void CrearFactura()
        {
            if (dgvCitas.SelectedRows.Count > 0)
            {
                if (string.IsNullOrEmpty(dgvCitas.CurrentRow.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                 
                        string Rpta = "";
                        IdCi = Convert.ToInt32(dgvCitas.CurrentRow.Cells[0].Value);
                        Rpta = L_Factura.CrearFactura(IdCi);

                        if (Rpta == "OK")
                        {
                            Mostrar("%");
                            MessageBox.Show("La Factura ha sido creada correctamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                }
            }
            else
            {
                MessageBox.Show("La tabla esta vacia", "Avisos Del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void FrmCrud_Citas_Load(object sender, EventArgs e)
        {
            Mostrar("%");
            Listado_cl("%");
            Listado_em();
            Listado_sr();
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
            DeseleccionarElementos();
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



        private void clbServicios_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            this.BeginInvoke((MethodInvoker)ActualizarTotal);
        }

        private void ActualizarTotal()
        {
            float total = 0;

            foreach (var item in clbServicios.CheckedItems)
            {
                DataRowView row = (DataRowView)item;
                float precio = Convert.ToSingle(row["Precio"]);
                total += precio;
            }

            txtTotal.Text = total.ToString();
        }

        public void Total()
        {


            ////private void CheckedListBoxServicios_ItemCheck(object sender, ItemCheckEventArgs e)
            ////{
            //float total = 0;

            //// Recorre todos los elementos del CheckedListBox
            //for (int i = 0; i < clbServicios.Items.Count; i++)
            //{
            //    // Verifica si el elemento está seleccionado
            //    if (clbServicios.GetItemChecked(i))
            //    {
            //        // Obtiene el precio del servicio correspondiente
            //        float precio = preciosServicios[i];

            //        // Suma el precio al total
            //        total += precio;
            //    }
            //}

            //// Muestra el total en el TextBox
            //txtTotal.Text = total.ToString();

        } //si sirve utilizando el metodo invocar, pero encuentro mas sencilla la funcion actualizar total

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btn_Actualizar_Click(object sender, EventArgs e)
        {
            if (dgvCitas.SelectedRows.Count > 0)
            {
                Operacion = "Editar";
                Selecciona_Item();
                selecionar_citas_servicios();
                Estadotexto(true);
                Estado_Botones_Principales(false);
                Estado_Botones_Procesos(true);
                tbpCitas.SelectedIndex = 1;
                dtpFecha.Focus();

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

        private void btnCrearFactura_Click(object sender, EventArgs e)
        {
            CrearFactura();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Mostrar(txtBuscar.Text.Trim());
        }
    }
}
