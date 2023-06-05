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
using System.Drawing.Printing;
using Entidades;

namespace Presentacion
{
    public partial class FrmFactura : Form
    {
        public FrmFactura()
        {
            InitializeComponent();
        }

        int IdFa;
        string IdCi;
        DateTime Fecha;
        string nombre;
        int IdCl;
        string Total;
        int posicionY;

        List<E_Servicios> servicios = new List<E_Servicios>();

        private void Mostrar()
        {
            try
            {

                dgvFactura.DataSource = L_Factura.Mostrar();
                //this.Formato_ca();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Eliminar()
        {
            if (dgvFactura.SelectedRows.Count > 0)
            {
                if (string.IsNullOrEmpty(dgvFactura.CurrentRow.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DialogResult result = MessageBox.Show("¿Estás seguro de eliminar esta Factura?", "Confirmación de eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string Rpta = "";
                        IdFa = Convert.ToInt32(dgvFactura.CurrentRow.Cells[0].Value);
                        Rpta = L_Factura.Eliminar(IdFa);

                        if (Rpta == "OK")
                        {
                            Mostrar();
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

        public void Facturar()
        {
            printDocument1 = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            printDocument1.PrinterSettings = ps;
            printDocument1.PrintPage += Imprimir;
            printDocument1.Print();

        }

        private void Selecciona_Item()
        {
            if (string.IsNullOrEmpty(dgvFactura.CurrentRow.Cells[0].Value.ToString()))
            {
                MessageBox.Show("Seleccione un Registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                IdFa = Convert.ToInt32(dgvFactura.CurrentRow.Cells[0].Value.ToString());
                Fecha = Convert.ToDateTime(dgvFactura.CurrentRow.Cells[1].Value.ToString());
                IdCi = Convert.ToString(dgvFactura.CurrentRow.Cells[2].Value.ToString());
                //IdCl = 
                nombre = Convert.ToString(dgvFactura.CurrentRow.Cells[3].Value.ToString());
                Total = Convert.ToString(dgvFactura.CurrentRow.Cells[4].Value.ToString());
                servicios = L_Factura.MostrarServicios(IdCi);
                //txtCedula.Text = Convert.ToString(dgvEmpleados.CurrentRow.Cells[0].Value);
                //txtNombre.Text = Convert.ToString(dgvEmpleados.CurrentRow.Cells[1].Value);
                //txtApellido.Text = Convert.ToString(dgvEmpleados.CurrentRow.Cells[2].Value);
                //txtTelefono.Text = Convert.ToString(dgvEmpleados.CurrentRow.Cells[3].Value);
                //dtpFecha.Text = Convert.ToString(dgvEmpleados.CurrentRow.Cells[4].Value);
                //txtSalario.Text = Convert.ToString(dgvEmpleados.CurrentRow.Cells[5].Value);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmFactura_Load(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 14);
            int ancho = 600;
            int y = 50;

            e.Graphics.DrawString("------------------------ Peluqueria AJ --------------------------", font, Brushes.Black, new RectangleF(200, y + 20, ancho, 20));
            e.Graphics.DrawString("Fecha: " + Fecha, font, Brushes.Black, new RectangleF(200, y + 110, ancho, 20));
            e.Graphics.DrawString("IDFactura: " + IdFa, font, Brushes.Black, new RectangleF(200, y + 70, ancho, 20));
            e.Graphics.DrawString("IDCita: " + IdCi, font, Brushes.Black, new RectangleF(200, y + 90, ancho, 20));
            e.Graphics.DrawString("Cliente: " + nombre, font, Brushes.Black, new RectangleF(200, y + 130, ancho, 20));

            e.Graphics.DrawString("--------------------------- Servicios -------------------------------", font, Brushes.Black, new RectangleF(200, y + 160, ancho, 20));

            int contador = 0;
            if (servicios.Count > 0)
            {
                foreach (var servicio in servicios)
                {
                    string nombreServicio = servicio.Nombre;
                    float precioServicio = servicio.Precio;

                    // Calcular la posición vertical (y) para mostrar cada servicio
                    posicionY = y + 200 + contador * 20;

                    e.Graphics.DrawString("Nombre: " + nombreServicio + " - Precio: " + precioServicio.ToString(), font, Brushes.Black, new RectangleF(200, posicionY, ancho, 20));

                    contador++;
                }
            }

            e.Graphics.DrawString("Total: " + Total, font, Brushes.Black, new RectangleF(200, posicionY + 30, ancho, 20));
            e.Graphics.DrawString("-----------------------------------------------------------------------", font, Brushes.Black, new RectangleF(200, posicionY + 60, ancho, 20));

        }



        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            Selecciona_Item();
            Facturar();

        }
    }
}
