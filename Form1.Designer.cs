namespace TechHelper_AI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Button btnNuevaConsulta;
        private Button btnContinuarConsulta;
        private Button btnConsultarTrabajos;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnNuevaConsulta = new Button();
            btnContinuarConsulta = new Button();
            btnConsultarTrabajos = new Button();
            panelContenido = new Panel();
            SuspendLayout();
            // 
            // btnNuevaConsulta
            // 
            btnNuevaConsulta.BackColor = SystemColors.Info;
            btnNuevaConsulta.Location = new Point(92, 37);
            btnNuevaConsulta.Name = "btnNuevaConsulta";
            btnNuevaConsulta.Size = new Size(200, 40);
            btnNuevaConsulta.TabIndex = 0;
            btnNuevaConsulta.Text = "Nueva consulta de soporte";
            btnNuevaConsulta.UseVisualStyleBackColor = false;
            btnNuevaConsulta.Click += btnNuevaConsulta_Click;
            // 
            // btnContinuarConsulta
            // 
            btnContinuarConsulta.BackColor = SystemColors.Info;
            btnContinuarConsulta.Location = new Point(475, 37);
            btnContinuarConsulta.Name = "btnContinuarConsulta";
            btnContinuarConsulta.Size = new Size(200, 40);
            btnContinuarConsulta.TabIndex = 1;
            btnContinuarConsulta.Text = "Continuar consulta guardada";
            btnContinuarConsulta.UseVisualStyleBackColor = false;
            btnContinuarConsulta.Click += btnContinuarConsulta_Click;
            // 
            // btnConsultarTrabajos
            // 
            btnConsultarTrabajos.BackColor = SystemColors.Info;
            btnConsultarTrabajos.Location = new Point(845, 37);
            btnConsultarTrabajos.Name = "btnConsultarTrabajos";
            btnConsultarTrabajos.Size = new Size(200, 40);
            btnConsultarTrabajos.TabIndex = 2;
            btnConsultarTrabajos.Text = "Consultar trabajos realizados";
            btnConsultarTrabajos.UseVisualStyleBackColor = false;
            btnConsultarTrabajos.Click += btnConsultarTrabajos_Click;
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.Transparent;
            panelContenido.BackgroundImageLayout = ImageLayout.Zoom;
            panelContenido.Location = new Point(25, 110);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(1177, 675);
            panelContenido.TabIndex = 3;
            // 
            // Form1
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1226, 797);
            Controls.Add(panelContenido);
            Controls.Add(btnNuevaConsulta);
            Controls.Add(btnContinuarConsulta);
            Controls.Add(btnConsultarTrabajos);
            Name = "Form1";
            Text = "TechHelper AI - Menú Principal";
            ResumeLayout(false);
        }
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>


        #endregion

        private Panel panelContenido;
    }
}
