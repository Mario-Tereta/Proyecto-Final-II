namespace TechHelper_AI
{
    partial class NuevaConsulta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.TextBox txtProblema;
        private System.Windows.Forms.TextBox txtSolucion;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.CheckBox chkIA;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label lblComentarios;
        private System.Windows.Forms.TextBox txtModeloLibre;
        private System.Windows.Forms.Label lblModeloLibre;



        private RadioButton rbComputadora;
        private RadioButton rbImpresora;
        private ComboBox cbMarca;
        private ComboBox cbModelo;
        private ComboBox cbMarcaImpresora;
        private TextBox txtSintoma;
        private Button btnBuscar;
        private Button btnGuardar;
        private CheckBox chkNuevoModelo;
        private Button btnGuardarPendiente;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevaConsulta));
            lblCodigo = new Label();
            lblMarca = new Label();
            lblModelo = new Label();
            lblProblema = new Label();
            lblSolucion = new Label();
            txtCodigo = new TextBox();
            txtMarca = new TextBox();
            txtModelo = new TextBox();
            txtProblema = new TextBox();
            txtSolucion = new TextBox();
            rbComputadora = new RadioButton();
            rbImpresora = new RadioButton();
            cbMarca = new ComboBox();
            cbModelo = new ComboBox();
            cbMarcaImpresora = new ComboBox();
            txtSintoma = new TextBox();
            btnBuscar = new Button();
            btnGuardar = new Button();
            dtpFecha = new DateTimePicker();
            chkIA = new CheckBox();
            lblComentarios = new Label();
            txtComentarios = new TextBox();
            lblModeloLibre = new Label();
            txtModeloLibre = new TextBox();
            chkNuevoModelo = new CheckBox();
            btnGuardarPendiente = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblCodigo
            // 
            lblCodigo.BackColor = Color.Transparent;
            lblCodigo.Location = new Point(30, 411);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(100, 20);
            lblCodigo.TabIndex = 15;
            lblCodigo.Text = "Código:";
            // 
            // lblMarca
            // 
            lblMarca.BackColor = Color.Transparent;
            lblMarca.Location = new Point(30, 450);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(100, 23);
            lblMarca.TabIndex = 16;
            lblMarca.Text = "Marca:";
            // 
            // lblModelo
            // 
            lblModelo.BackColor = Color.Transparent;
            lblModelo.Location = new Point(30, 491);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(100, 23);
            lblModelo.TabIndex = 17;
            lblModelo.Text = "Modelo:";
            // 
            // lblProblema
            // 
            lblProblema.BackColor = Color.Transparent;
            lblProblema.Location = new Point(548, 10);
            lblProblema.Name = "lblProblema";
            lblProblema.Size = new Size(100, 23);
            lblProblema.TabIndex = 18;
            lblProblema.Text = "Problema:";
            // 
            // lblSolucion
            // 
            lblSolucion.BackColor = Color.Transparent;
            lblSolucion.Location = new Point(548, 197);
            lblSolucion.Name = "lblSolucion";
            lblSolucion.Size = new Size(100, 23);
            lblSolucion.TabIndex = 19;
            lblSolucion.Text = "Solución:";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(30, 426);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(200, 23);
            txtCodigo.TabIndex = 0;
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(30, 466);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(200, 23);
            txtMarca.TabIndex = 1;
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(30, 506);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(200, 23);
            txtModelo.TabIndex = 2;
            // 
            // txtProblema
            // 
            txtProblema.Location = new Point(548, 223);
            txtProblema.Multiline = true;
            txtProblema.Name = "txtProblema";
            txtProblema.Size = new Size(418, 161);
            txtProblema.TabIndex = 3;
            // 
            // txtSolucion
            // 
            txtSolucion.Location = new Point(548, 30);
            txtSolucion.Multiline = true;
            txtSolucion.Name = "txtSolucion";
            txtSolucion.Size = new Size(418, 161);
            txtSolucion.TabIndex = 4;
            // 
            // rbComputadora
            // 
            rbComputadora.BackColor = Color.Transparent;
            rbComputadora.Location = new Point(73, 124);
            rbComputadora.Name = "rbComputadora";
            rbComputadora.Size = new Size(100, 20);
            rbComputadora.TabIndex = 5;
            rbComputadora.Text = "Computadora";
            rbComputadora.UseVisualStyleBackColor = false;
            rbComputadora.CheckedChanged += rbComputadora_CheckedChanged;
            // 
            // rbImpresora
            // 
            rbImpresora.BackColor = Color.Transparent;
            rbImpresora.Location = new Point(193, 124);
            rbImpresora.Name = "rbImpresora";
            rbImpresora.Size = new Size(100, 20);
            rbImpresora.TabIndex = 6;
            rbImpresora.Text = "Impresora";
            rbImpresora.UseVisualStyleBackColor = false;
            rbImpresora.CheckedChanged += rbImpresora_CheckedChanged;
            // 
            // cbMarca
            // 
            cbMarca.ForeColor = SystemColors.WindowFrame;
            cbMarca.Items.AddRange(new object[] { "HP", "Acer", "Lenovo", "Asus", "Dell", "MSI", "Samsung", "Toshiba" });
            cbMarca.Location = new Point(72, 150);
            cbMarca.Name = "cbMarca";
            cbMarca.Size = new Size(220, 23);
            cbMarca.TabIndex = 7;
            cbMarca.Text = "Marca...";
            // 
            // cbModelo
            // 
            cbModelo.ForeColor = SystemColors.WindowFrame;
            cbModelo.Location = new Point(72, 193);
            cbModelo.Name = "cbModelo";
            cbModelo.Size = new Size(220, 23);
            cbModelo.TabIndex = 8;
            cbModelo.Text = "Modelo...";
            // 
            // cbMarcaImpresora
            // 
            cbMarcaImpresora.Items.AddRange(new object[] { "Canon", "Epson" });
            cbMarcaImpresora.Location = new Point(72, 150);
            cbMarcaImpresora.Name = "cbMarcaImpresora";
            cbMarcaImpresora.Size = new Size(220, 23);
            cbMarcaImpresora.TabIndex = 9;
            cbMarcaImpresora.Visible = false;
            cbMarcaImpresora.SelectedIndexChanged += cbMarcaImpresora_SelectedIndexChanged;
            // 
            // txtSintoma
            // 
            txtSintoma.Location = new Point(51, 270);
            txtSintoma.Multiline = true;
            txtSintoma.Name = "txtSintoma";
            txtSintoma.PlaceholderText = "Describa el síntoma o error a la AI para asistencia";
            txtSintoma.Size = new Size(418, 67);
            txtSintoma.TabIndex = 10;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(217, 354);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(100, 30);
            btnBuscar.TabIndex = 11;
            btnBuscar.Text = "Buscar solución";
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(29, 597);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(141, 30);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "Guardar y generar Word";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(34, 552);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(224, 23);
            dtpFecha.TabIndex = 13;
            // 
            // chkIA
            // 
            chkIA.BackColor = Color.Transparent;
            chkIA.Location = new Point(189, 603);
            chkIA.Name = "chkIA";
            chkIA.Size = new Size(250, 20);
            chkIA.TabIndex = 14;
            chkIA.Text = "¿Se utilizó inteligencia artificial?";
            chkIA.UseVisualStyleBackColor = false;
            // 
            // lblComentarios
            // 
            lblComentarios.BackColor = Color.Transparent;
            lblComentarios.Location = new Point(573, 439);
            lblComentarios.Name = "lblComentarios";
            lblComentarios.Size = new Size(154, 20);
            lblComentarios.TabIndex = 20;
            lblComentarios.Text = "Comentarios del técnico:";
            // 
            // txtComentarios
            // 
            txtComentarios.Location = new Point(548, 462);
            txtComentarios.Multiline = true;
            txtComentarios.Name = "txtComentarios";
            txtComentarios.Size = new Size(418, 129);
            txtComentarios.TabIndex = 21;
            // 
            // lblModeloLibre
            // 
            lblModeloLibre.BackColor = Color.Transparent;
            lblModeloLibre.Location = new Point(321, 171);
            lblModeloLibre.Name = "lblModeloLibre";
            lblModeloLibre.Size = new Size(100, 20);
            lblModeloLibre.TabIndex = 22;
            lblModeloLibre.Text = "Ingresa Modelo";
            lblModeloLibre.Visible = false;
            // 
            // txtModeloLibre
            // 
            txtModeloLibre.ForeColor = SystemColors.WindowText;
            txtModeloLibre.Location = new Point(310, 194);
            txtModeloLibre.Name = "txtModeloLibre";
            txtModeloLibre.ScrollBars = ScrollBars.Horizontal;
            txtModeloLibre.Size = new Size(110, 23);
            txtModeloLibre.TabIndex = 23;
            txtModeloLibre.Visible = false;
            // 
            // chkNuevoModelo
            // 
            chkNuevoModelo.BackColor = Color.Transparent;
            chkNuevoModelo.Location = new Point(305, 148);
            chkNuevoModelo.Name = "chkNuevoModelo";
            chkNuevoModelo.Size = new Size(115, 20);
            chkNuevoModelo.TabIndex = 24;
            chkNuevoModelo.Text = "¿Modelo nuevo?";
            chkNuevoModelo.UseVisualStyleBackColor = false;
            chkNuevoModelo.CheckedChanged += chkNuevoModelo_CheckedChanged;
            // 
            // btnGuardarPendiente
            // 
            btnGuardarPendiente.Location = new Point(686, 597);
            btnGuardarPendiente.Name = "btnGuardarPendiente";
            btnGuardarPendiente.Size = new Size(160, 30);
            btnGuardarPendiente.TabIndex = 25;
            btnGuardarPendiente.Text = "Guardar y continuar después";
            btnGuardarPendiente.Click += btnGuardarPendiente_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Enabled = false;
            pictureBox1.Location = new Point(84, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(336, 101);
            pictureBox1.TabIndex = 26;
            pictureBox1.TabStop = false;
            // 
            // NuevaConsulta
            // 
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(999, 639);
            Controls.Add(pictureBox1);
            Controls.Add(txtCodigo);
            Controls.Add(txtMarca);
            Controls.Add(txtModelo);
            Controls.Add(txtProblema);
            Controls.Add(txtSolucion);
            Controls.Add(rbComputadora);
            Controls.Add(rbImpresora);
            Controls.Add(cbMarca);
            Controls.Add(cbModelo);
            Controls.Add(cbMarcaImpresora);
            Controls.Add(txtSintoma);
            Controls.Add(btnBuscar);
            Controls.Add(btnGuardar);
            Controls.Add(dtpFecha);
            Controls.Add(chkIA);
            Controls.Add(lblCodigo);
            Controls.Add(lblMarca);
            Controls.Add(lblModelo);
            Controls.Add(lblProblema);
            Controls.Add(lblSolucion);
            Controls.Add(lblComentarios);
            Controls.Add(txtComentarios);
            Controls.Add(lblModeloLibre);
            Controls.Add(txtModeloLibre);
            Controls.Add(chkNuevoModelo);
            Controls.Add(btnGuardarPendiente);
            Name = "NuevaConsulta";
            Text = "Nueva Consulta";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        #endregion

        private Label lblCodigo;
        private Label lblMarca;
        private Label lblModelo;
        private Label lblProblema;
        private Label lblSolucion;
        private PictureBox pictureBox1;
    }
}