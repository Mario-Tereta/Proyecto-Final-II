namespace TechHelper_AI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void MostrarFormularioEnPanel(Form formHijo)
        {
            panelContenido.Controls.Clear();         // Limpia el área
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(formHijo);
            formHijo.Show();
        }
        private void MostrarFormularioEnPanel_NuevaConsulta(int id, string tipo)
        {
            var frm = new NuevaConsulta();
            frm.CargarConsultaExistente(id, tipo);
            MostrarFormularioEnPanel(frm);
        }
        private void btnNuevaConsulta_Click(object sender, EventArgs e)
        {
            MostrarFormularioEnPanel(new NuevaConsulta());
        }

        private void btnContinuarConsulta_Click(object sender, EventArgs e)
        {
            var contConsulta = new ContConsulta();
            contConsulta.ContinuarConsulta += (id, tipo) => {
                MostrarFormularioEnPanel_NuevaConsulta(id, tipo);
            };
            MostrarFormularioEnPanel(contConsulta);
        }

        private void btnConsultarTrabajos_Click(object sender, EventArgs e)
        {
            MostrarFormularioEnPanel(new Consultas());
        }

        // Método auxiliar para solo mostrar un hijo a la vez
        private void AbrirFormHijo(Form formHijo)
        {
            // Cierra hijos abiertos (opcional)
            foreach (Form f in this.MdiChildren)
                f.Close();

            formHijo.MdiParent = this;
            formHijo.WindowState = FormWindowState.Maximized; // Ocupa todo el área MDI
            formHijo.Show();
        }
    }
}
