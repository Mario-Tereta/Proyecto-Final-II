using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.ComponentModel;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;


namespace TechHelper_AI
{
    public partial class NuevaConsulta : Form
    {

        private List<object> historialIA = new List<object>
        {
        new { role = "system", content = "Eres un asistente técnico especializado en computadoras e impresoras." }
        };
        private string solucionEncontrada = "";
        private bool usoIA = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ConsultaActualizada { get; private set; }
        private bool esEdicionConsulta = false;
        private string tipoEdicion = ""; // Guarda si es PC o Impresora cuando editas (por si lo necesitas)
        private int idConsulta = 0; // Guarda el ID de la consulta (por si necesitas actualizarla después)
        private readonly bool modoEdicion;
        private readonly string tipo;
        private readonly int consultaId;
       
        public NuevaConsulta()
        {
            InitializeComponent();
            this.modoEdicion = false; // Modo creación
            this.tipo = ""; // O ponle valor por defecto
            
            txtCodigo.Visible = true;
            txtMarca.Visible = true;
            txtModelo.Visible = true;
            txtProblema.Visible = true;
            txtSolucion.Visible = true;

            rbComputadora.Visible = true;
            rbImpresora.Visible = true;
            cbMarca.Visible = true;
            cbModelo.Visible = true;
            cbMarcaImpresora.Visible = true;
            txtMarca.ReadOnly = false;
            txtModelo.ReadOnly = false;
            txtCodigo.ReadOnly = false;
            this.Text = "Nueva Consulta";

            cbMarca.SelectedIndexChanged += async (s, e) =>
            {
                await CargarModelosComputadoraAsync(cbMarca.SelectedItem?.ToString() ?? "");
                await LlenarCamposAutomaticamente();
            };
            cbModelo.SelectedIndexChanged += async (s, e) =>
            {
                await LlenarCamposAutomaticamente();
            };
            cbMarcaImpresora.SelectedIndexChanged += async (s, e) =>
            {
                await CargarModelosImpresoraAsync(cbMarcaImpresora.SelectedItem?.ToString() ?? "");
                await LlenarCamposAutomaticamente();
            };
            txtModeloLibre.TextChanged += async (s, e) =>
            {
                await LlenarCamposAutomaticamente();
            };
        }
        

        public void CargarConsultaExistente(int id, string tipo)
        {
            esEdicionConsulta = true;       // Marca que es edición
            tipoEdicion = tipo;
            idConsulta = id;

            string query = tipo == "PC"
                ? "SELECT * FROM Computadoras WHERE Id = @Id"
                : "SELECT * FROM Impresoras WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtCodigo.Text = reader["Codigo"].ToString();
                        txtMarca.Text = reader["Marca"].ToString();
                        txtModelo.Text = reader["Modelo"].ToString();
                        txtProblema.Text = reader["Problema"].ToString(); // <-- AQUÍ ESTA BIEN
                        txtSolucion.Text = reader["Solucion"].ToString();
                        txtComentarios.Text = reader["Comentarios"]?.ToString() ?? "";
                        dtpFecha.Value = reader["FechaTrabajo"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["FechaTrabajo"]);
                        chkIA.Checked = reader["UsoIA"] != DBNull.Value && reader["UsoIA"].ToString() == "1";
                    }
                }
            }
            txtCodigo.ReadOnly = true;   // No editable
            txtMarca.ReadOnly = true;
            txtModelo.ReadOnly = true;
            // Solo habilita lo necesario
            txtSintoma.ReadOnly = false;
            txtSolucion.ReadOnly = false;
            // Si tienes campos que no deben usarse, los ocultas
            rbComputadora.Visible = false;
            rbImpresora.Visible = false;
            cbMarca.Visible = false;
            cbModelo.Visible = false;
            cbMarcaImpresora.Visible = false;
        }

        private async void rbComputadora_CheckedChanged(object sender, EventArgs e)
        {
            if (rbComputadora.Checked)
            {
                cbMarca.Visible = true;
                cbModelo.Visible = true;
                cbMarcaImpresora.Visible = false;
                cbMarca.SelectedIndex = -1;
                cbModelo.Items.Clear();

                // Si se prefiere, puedes mostrar directamente txtModeloLibre si no hay modelos
                txtModeloLibre.Visible = false;
                lblModeloLibre.Visible = false;
                cbModelo.Visible = true;
            }
        }

        private void rbImpresora_CheckedChanged(object sender, EventArgs e)
        {
            if (rbImpresora.Checked)
            {
                cbMarca.Visible = false;
                cbModelo.Visible = true;
                cbMarcaImpresora.Visible = true;
                cbMarcaImpresora.SelectedIndex = -1;
                cbModelo.Items.Clear();

                txtModeloLibre.Visible = false;
                lblModeloLibre.Visible = false;
                cbModelo.Visible = true;
            }
        }

        private async void cbMarcaImpresora_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMarcaImpresora.SelectedItem != null)
            {
                await CargarModelosImpresoraAsync(cbMarcaImpresora.SelectedItem.ToString());
            }
        }
        private void chkNuevoModelo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNuevoModelo.Checked)
            {
                txtModeloLibre.Visible = true;
                lblModeloLibre.Visible = true;
                cbModelo.Visible = false;
            }
            else
            {
                txtModeloLibre.Visible = false;
                lblModeloLibre.Visible = false;
                cbModelo.Visible = true;
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string marca = "";
            string modelo = "";

            // Marca seleccionada del combo
            if (rbComputadora.Checked)
                marca = cbMarca.SelectedItem?.ToString() ?? "";
            else // impresora
                marca = cbMarcaImpresora.SelectedItem?.ToString() ?? "";

            // Modelo: texto libre si está visible, combo si no.
            if (txtModeloLibre.Visible)
                modelo = txtModeloLibre.Text.Trim();
            else
                modelo = cbModelo.SelectedItem?.ToString() ?? "";

            string sintoma = txtSintoma.Text.Trim();

            // ------------------------
            // VALIDACIONES PARA COMPUTADORA
            // ------------------------
            if (rbComputadora.Checked)
            {
                // Marca
                if (cbMarca.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione la marca de la computadora.");
                    return;
                }

                // Modelo: exige uno u otro, no ambos
                if ((txtModeloLibre.Visible && string.IsNullOrWhiteSpace(txtModeloLibre.Text)) ||
                    (!txtModeloLibre.Visible && cbModelo.SelectedItem == null))
                {
                    MessageBox.Show("Seleccione o escriba el modelo de la computadora.");
                    return;
                }
            }
            // ------------------------
            // VALIDACIONES PARA IMPRESORA
            // ------------------------
            else if (rbImpresora.Checked)
            {
                if (cbMarcaImpresora.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione la marca de la impresora.");
                    return;
                }
                if ((txtModeloLibre.Visible && string.IsNullOrWhiteSpace(txtModeloLibre.Text)) ||
                    (!txtModeloLibre.Visible && cbModelo.SelectedItem == null))
                {
                    MessageBox.Show("Seleccione o escriba el modelo de la impresora.");
                    return;
                }
            }
            // ------------------------
            // VALIDACION DE SINTOMA
            // ------------------------
            if (string.IsNullOrWhiteSpace(sintoma))
            {
                MessageBox.Show("Ingrese el síntoma o error.");
                return;
            }

            // ------------------------
            // BÚSQUEDA DE SOLUCIÓN
            // ------------------------
            solucionEncontrada = "";
            usoIA = false;

            // Llama a las funciones de solución usando las variables ya validadas
            if (rbComputadora.Checked)
            {
                solucionEncontrada = await BuscarSolucionComputadoraAsync(marca, modelo, sintoma);
            }
            else if (rbImpresora.Checked)
            {
                solucionEncontrada = await BuscarSolucionImpresoraAsync(marca, modelo, sintoma);
            }

            // Si no encontró nada, invoca a la IA
            if (string.IsNullOrWhiteSpace(solucionEncontrada))
            {
                // Esto añade EL MENSAJE DEL USUARIO al historial
                historialIA.Add(new { role = "user", content = sintoma });

                // Esto consulta la IA con TODO EL HISTORIAL
                solucionEncontrada = await ConsultarIAAsync(historialIA);

                usoIA = true;

                // Esto añade LA RESPUESTA de la IA al historial
                historialIA.Add(new { role = "assistant", content = solucionEncontrada });
                usoIA = true;
            }

            MessageBox.Show("Solución encontrada:\n" + solucionEncontrada, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            string marca, modelo, tipo, codigo, problema, solucion, fecha, comentarios;
            bool usoIA;

            // Obtener campos comunes
            if (esEdicionConsulta)
            {
                // Siempre toma de los textbox visibles (modo edición)
                marca = txtMarca.Text.Trim();
                modelo = txtModelo.Text.Trim();
                codigo = txtCodigo.Text.Trim();
                problema = txtProblema.Text.Trim();
                solucion = txtSolucion.Text.Trim();
                tipo = tipoEdicion;
            }
            else
            {
                // Al crear nuevo, toma del combo/texbox según lo visible
                if (rbComputadora.Checked)
                {
                    marca = cbMarca.SelectedItem?.ToString() ?? "";
                    tipo = "PC";
                }
                else
                {
                    marca = cbMarcaImpresora.SelectedItem?.ToString() ?? "";
                    tipo = "Impresora";
                }
                modelo = txtModeloLibre.Visible ? txtModeloLibre.Text.Trim() : cbModelo.SelectedItem?.ToString() ?? "";
                codigo = txtCodigo.Text.Trim();
                problema = txtProblema.Text.Trim();
                solucion = txtSolucion.Text.Trim();
            }

            fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            usoIA = chkIA.Checked;
            comentarios = txtComentarios.Text.Trim();

            // Validaciones duras!
            if (string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo) ||
                string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(problema) || string.IsNullOrWhiteSpace(solucion))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios (incluyendo Solución).");
                return;
            }

            try
            {
                // GUARDA Y FINALIZA (determina si es update o insert)
                if (tipo == "PC")
                    await GuardarComputadoraAsync(codigo, marca, modelo, problema, solucion, fecha, usoIA, comentarios, esEdicionConsulta, idConsulta);
                else
                    await GuardarImpresoraAsync(codigo, marca, modelo, problema, solucion, fecha, usoIA, comentarios, esEdicionConsulta, idConsulta);

                GenerarWord(tipo, codigo, marca, modelo, problema, solucion, fecha, usoIA, comentarios);
                

                MessageBox.Show("Consulta guardada y finalizada correctamente.");
                ConsultaActualizada = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}");
            }
        }
        private async void btnGuardarPendiente_Click(object sender, EventArgs e)
        {
            string marca = "";
            string modelo = "";
            string tipo = "";

            if (esEdicionConsulta)
            {
                marca = txtMarca.Text.Trim();
                modelo = txtModelo.Text.Trim();
                tipo = tipoEdicion;
            }
            else
            {
                if (rbComputadora.Checked)
                {
                    marca = cbMarca.SelectedItem?.ToString() ?? "";
                    tipo = "PC";
                }
                else // es impresora
                {
                    marca = cbMarcaImpresora.SelectedItem?.ToString() ?? "";
                    tipo = "Impresora";
                }

                // Modelo: si el textbox libre está visible, toma su texto; si no, toma lo del combo
                if (txtModeloLibre.Visible)
                    modelo = txtModeloLibre.Text.Trim();
                else
                    modelo = cbModelo.SelectedItem?.ToString() ?? "";
            }

            string codigo = txtCodigo.Text.Trim();
            string problema = txtProblema.Text.Trim(); 
            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            bool usoIA = chkIA.Checked;
            string comentarios = txtComentarios?.Text?.Trim() ?? "";

            // Validar SOLO campos imprescindibles para guardar como pendiente
            if (string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo) ||
    string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(problema))
            {
                MessageBox.Show("Por favor complete los datos principales para guardar la consulta pendiente.");
                return;
            }

            try
            {
                // Solución PENDIENTE = VACÍA
                if (tipo == "PC")
                    await GuardarComputadoraAsync(codigo, marca, modelo, problema, "", fecha, usoIA, comentarios);
                else if (tipo == "Impresora")
                    await GuardarImpresoraAsync(codigo, marca, modelo, problema, "", fecha, usoIA, comentarios);

                MessageBox.Show("Consulta guardada como pendiente. Podrás completarla después.");
                ConsultaActualizada = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }
               
        // --- Métodos auxiliares asíncronos ---

        private async Task CargarMarcasComputadoraAsync()
        {
            cbMarca.Items.Clear();
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT DISTINCT Marca FROM Computadoras", conn);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cbMarca.Items.Add(reader.GetString(0));
                    }
                }
            }

            if (cbModelo.Items.Count == 0)
            {
                txtModeloLibre.Visible = true;
                lblModeloLibre.Visible = true;
                cbModelo.Visible = false;
            }
            else
            {
                txtModeloLibre.Visible = false;
                lblModeloLibre.Visible = false;
                cbModelo.Visible = true;
            }
        }

        private async Task CargarModelosComputadoraAsync(string marca)
        {
            cbModelo.Items.Clear();
            if (string.IsNullOrEmpty(marca)) return;
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT DISTINCT Modelo FROM Computadoras WHERE Marca = @marca", conn);
                cmd.Parameters.AddWithValue("@marca", marca);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cbModelo.Items.Add(reader.GetString(0));
                    }
                }
            }

            // Mostrar u ocultar txtModeloLibre y label según si la lista quedó vacía…
            if (cbModelo.Items.Count == 0)
            {
                txtModeloLibre.Visible = true;
                lblModeloLibre.Visible = true;
                cbModelo.Visible = false;
            }
            else
            {
                txtModeloLibre.Visible = false;
                lblModeloLibre.Visible = false;
                cbModelo.Visible = true;
            }
        }

        private async Task CargarModelosImpresoraAsync(string marca)
        {
            cbModelo.Items.Clear();
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT DISTINCT Modelo FROM Impresoras WHERE Marca = @marca", conn);
                cmd.Parameters.AddWithValue("@marca", marca);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cbModelo.Items.Add(reader.GetString(0));
                    }
                }
            }
            // Si no hay modelos, permite escribir uno nuevo
            if (cbModelo.Items.Count == 0)
            {
                txtModeloLibre.Visible = true;
                lblModeloLibre.Visible = true;
                cbModelo.Visible = false;
            }
            else
            {
                txtModeloLibre.Visible = false;
                lblModeloLibre.Visible = false;
                cbModelo.Visible = true;
            }
        }

        private async Task<string> BuscarSolucionComputadoraAsync(string marca, string modelo, string sintoma)
        {
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT Solucion FROM Computadoras WHERE Marca = @marca AND Modelo = @modelo AND Problema = @problema", conn);
                cmd.Parameters.AddWithValue("@marca", marca);
                cmd.Parameters.AddWithValue("@modelo", modelo);
                cmd.Parameters.AddWithValue("@problema", sintoma);
                var result = await cmd.ExecuteScalarAsync();
                return result?.ToString() ?? "";
            }
        }

        private async Task<string> BuscarSolucionImpresoraAsync(string marca, string modelo, string sintoma)
        {
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            {
                await conn.OpenAsync();
                var cmd = new SqlCommand("SELECT Solucion FROM Impresoras WHERE Marca = @marca AND Modelo = @modelo AND Problema = @problema", conn);
                cmd.Parameters.AddWithValue("@marca", marca);
                cmd.Parameters.AddWithValue("@modelo", modelo);
                cmd.Parameters.AddWithValue("@problema", sintoma);
                var result = await cmd.ExecuteScalarAsync();
                return result?.ToString() ?? "";
            }
        }

        private async Task<string> ConsultarIAAsync(List<object> historial)
        {
            // Ejemplo de integración con OpenAI usando HttpClient y Newtonsoft.Json
            try
            {
                using (var client = new HttpClient())
                {
                    var apiKey = "sin API";
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    var requestBody = new
                    {
                        model = "gpt-3.5-turbo",
                        messages = historial
                    };

                    var json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                    response.EnsureSuccessStatusCode();

                    var responseString = await response.Content.ReadAsStringAsync();
                    dynamic respuesta = JsonConvert.DeserializeObject(responseString);
                    return respuesta.choices[0].message.content.ToString();
                }
            }
            catch (Exception ex)
            {
                return "No se pudo obtener una solución de la IA. " + ex.Message;
            }
        }

        private async Task GuardarComputadoraAsync(
    string codigo, string marca, string modelo, string problema, string solucion,
    string fecha, bool usoIA, string comentarios, bool editar = false, int id = 0)
        {
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            {
                await conn.OpenAsync();
                if (editar)
                {
                    string sql = @"UPDATE Computadoras SET Codigo=@codigo, Marca=@marca, Modelo=@modelo,
                        Problema=@problema, Solucion=@solucion, FechaTrabajo=@fecha, UsoIA=@usoIA, Comentarios=@comentarios
                        WHERE Id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigo);
                        cmd.Parameters.AddWithValue("@marca", marca);
                        cmd.Parameters.AddWithValue("@modelo", modelo);
                        cmd.Parameters.AddWithValue("@problema", problema);
                        cmd.Parameters.AddWithValue("@solucion", solucion);
                        cmd.Parameters.AddWithValue("@fecha", fecha);
                        cmd.Parameters.AddWithValue("@usoIA", usoIA);
                        cmd.Parameters.AddWithValue("@comentarios", comentarios);
                        cmd.Parameters.AddWithValue("@id", id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    string sql = @"INSERT INTO Computadoras 
                    (Codigo, Marca, Modelo, Problema, Solucion, FechaTrabajo, UsoIA, Comentarios)
                    VALUES (@codigo, @marca, @modelo, @problema, @solucion, @fecha, @usoIA, @comentarios)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigo);
                        cmd.Parameters.AddWithValue("@marca", marca);
                        cmd.Parameters.AddWithValue("@modelo", modelo);
                        cmd.Parameters.AddWithValue("@problema", problema);
                        cmd.Parameters.AddWithValue("@solucion", solucion);
                        cmd.Parameters.AddWithValue("@fecha", fecha);
                        cmd.Parameters.AddWithValue("@usoIA", usoIA);
                        cmd.Parameters.AddWithValue("@comentarios", comentarios);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        private async Task GuardarImpresoraAsync(
    string codigo, string marca, string modelo, string problema, string solucion,
    string fecha, bool usoIA, string comentarios, bool editar = false, int id = 0) // usa los PARÁMETROS
        {
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            {
                await conn.OpenAsync();
                if (editar) // USA EL PARÁMETRO, NO EL CAMPO DE INSTANCIA
                {
                    string updateQuery = @"
            UPDATE Impresoras 
            SET Codigo=@codigo, Marca = @marca, Modelo = @modelo, Problema = @problema, Solucion = @solucion, FechaTrabajo = @fecha, UsoIA = @usoIA, Comentarios = @comentarios
            WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigo); // Incluye si quieres editar código también.
                        cmd.Parameters.AddWithValue("@marca", marca);
                        cmd.Parameters.AddWithValue("@modelo", modelo);
                        cmd.Parameters.AddWithValue("@problema", problema);
                        cmd.Parameters.AddWithValue("@solucion", solucion);
                        cmd.Parameters.AddWithValue("@fecha", fecha);
                        cmd.Parameters.AddWithValue("@usoIA", usoIA);
                        cmd.Parameters.AddWithValue("@comentarios", comentarios);
                        cmd.Parameters.AddWithValue("@id", id); // USA EL PARÁMETRO
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    string insertQuery = @"
            INSERT INTO Impresoras (Codigo, Marca, Modelo, Problema, Solucion, FechaTrabajo, UsoIA, Comentarios)
            VALUES (@codigo, @marca, @modelo, @problema, @solucion, @fecha, @usoIA, @comentarios)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        // cmd.Parameters.AddWithValue("@id", idConsulta); // <-- ¡Saca este parámetro!
                        cmd.Parameters.AddWithValue("@codigo", codigo);
                        cmd.Parameters.AddWithValue("@marca", marca);
                        cmd.Parameters.AddWithValue("@modelo", modelo);
                        cmd.Parameters.AddWithValue("@problema", problema);
                        cmd.Parameters.AddWithValue("@solucion", solucion);
                        cmd.Parameters.AddWithValue("@fecha", fecha);
                        cmd.Parameters.AddWithValue("@usoIA", usoIA);
                        cmd.Parameters.AddWithValue("@comentarios", comentarios);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        private async Task<string> GenerarCodigoAsync(string tipo)
        {
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            {
                await conn.OpenAsync();
                string prefijo = tipo.StartsWith("C") ? "Cn" : tipo.StartsWith("E") ? "Eps" : "PC";
                string tabla = tipo == "PC" ? "Computadoras" : "Impresoras";
                var cmd = new SqlCommand($"SELECT COUNT(*) FROM {tabla} WHERE Codigo LIKE '{prefijo}%'", conn);
                int count = (int)await cmd.ExecuteScalarAsync();
                return $"{prefijo}_{(count + 1):D2}";
            }
        }
        private async Task LlenarCamposAutomaticamente()
        {
            string marca = "";
            string modelo = "";

            // Detectar si impresora o computadora
            if (rbComputadora.Checked)
            {
                marca = cbMarca.SelectedItem?.ToString() ?? "";
                modelo = txtModeloLibre.Visible ? txtModeloLibre.Text.Trim() : cbModelo.SelectedItem?.ToString() ?? "";
            }
            else if (rbImpresora.Checked)
            {
                marca = cbMarcaImpresora.SelectedItem?.ToString() ?? "";
                modelo = txtModeloLibre.Visible ? txtModeloLibre.Text.Trim() : cbModelo.SelectedItem?.ToString() ?? "";
            }

            // Actualizar campos de la derecha
            txtMarca.Text = marca;
            txtModelo.Text = modelo;

            // Generar código automático
            string tipo = rbComputadora.Checked ? "PC"
                          : marca == "Canon" ? "CAN"
                          : marca == "Epson" ? "EPS"
                          : "EQP";

            txtCodigo.Text = await GenerarCodigoAsync(tipo);
        }
        private void GenerarWord(string tipo, string codigo, string marca, string modelo, string sintoma, string solucion, string fecha, bool usoIA, string comentarios)
        {
            // Carpeta "TechHelper" en el escritorio del usuario actual
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string carpeta = Path.Combine(desktop, "TechHelper");
            Directory.CreateDirectory(carpeta); // La crea si no existe

            // El nombre del archivo: "CODIGO_MODELO.docx"
            string fileName = Path.Combine(carpeta, $"{codigo}_{modelo}_{DateTime.Now:yyyyMMddHHmmss}.docx");

            using (WordprocessingDocument doc = WordprocessingDocument.Create(fileName, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                // ---- Logo (si lo quieres, ver sección 2 abajo)
                // Puedes insertar imagenes, ver más adelante

                // ---- Encabezado bonito
                body.Append(
                    GetTitulo("Reporte Técnico - TechHelper AI"),
                    new Paragraph(new Run(new Text(""))), // espacio
                    new Paragraph(new Run(new Text($"Fecha: {fecha}"))) { ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Right }) }
                );

                // ---- Cuerpo con estilo
                body.Append(
                    GetCampo("Tipo", tipo),
                    GetCampo("Código", codigo),
                    GetCampo("Marca", marca),
                    GetCampo("Modelo", modelo),
                    GetCampo("Síntoma", sintoma, true),
                    GetCampo("Solución", solucion, true),
                    GetCampo("¿Se utilizó IA?", usoIA ? "Sí" : "No"),
                    GetCampo("Comentarios del técnico", comentarios, true)
                );

                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }
            MessageBox.Show($"Documento generado: {fileName}");
        }

        // -- Métodos helper para estilos bonitos:
        private Paragraph GetTitulo(string texto)
        {
            return new Paragraph(new Run(
                new Text(texto)
            ))
            {
                ParagraphProperties = new ParagraphProperties(
                    new Justification() { Val = JustificationValues.Center },
                    new SpacingBetweenLines() { After = "200" },
                    new RunProperties(new Bold(), new Color { Val = "2E74B5" }, new FontSize() { Val = "36" })
                )
            };
        }

        private Paragraph GetCampo(string etiqueta, string valor, bool multi = false)
        {
            // Puedes mejorar con subrayado, color, etc
            var props = new ParagraphProperties();
            if (multi) props.SpacingBetweenLines = new SpacingBetweenLines() { After = "150" };
            return new Paragraph(
                new Run(
                    new RunProperties(new Bold()),
                    new Text(etiqueta + ": ")
                ),
                new Run(new Text(valor))
            )
            { ParagraphProperties = props };
        }
    }
}
