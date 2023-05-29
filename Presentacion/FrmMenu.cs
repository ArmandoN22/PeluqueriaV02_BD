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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        public Form formActivo = null;
        public void FormulariosFijo(Form formHijo)
        {
            if (formActivo != null)
                formActivo.Close();
            formActivo = formHijo;
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            panelFormularioFijo.Controls.Add(formHijo);
            panelFormularioFijo.Tag = formHijo;
            formHijo.BringToFront();
            formHijo.Show();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            FormulariosFijo(new frm_Crud_Empleados());
        }

        private void btnServicios_Click(object sender, EventArgs e)
        {
            FormulariosFijo(new Frm_Crud_Servicios());
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FormulariosFijo(new Frm_Crud_Clientes());
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            FormulariosFijo(new FrmCrud_Citas());
        }
    }
}
