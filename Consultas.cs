using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace TechHelper_AI
{
    public partial class Consultas : Form
    {
        
        public Consultas()
        {
            InitializeComponent();
            cbTipo.Items.AddRange(new object[] { "Todos", "PC", "Canon", "Epson" });
            cbTipo.SelectedIndex = 0;
            _ = CargarTrabajosAsync("Todos");
        }
        private async void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CargarTrabajosAsync(cbTipo.SelectedItem.ToString());
        }

        private async Task CargarTrabajosAsync(string tipo)
        {
            string query = "";
            if (tipo == "PC")
            {
                query = @"
            SELECT 'PC' AS Tipo, Codigo, Marca, Modelo, Problema, Solucion, FechaTrabajo, UsoIA, Comentarios 
            FROM Computadoras
            WHERE Solucion IS NOT NULL AND Solucion <> ''";
            }
            else if (tipo == "Canon" || tipo == "Epson")
            {
                query = $@"
            SELECT Marca AS Tipo, Codigo, Marca, Modelo, Problema, Solucion, FechaTrabajo, UsoIA, Comentarios
            FROM Impresoras
            WHERE Marca = '{tipo}' AND Solucion IS NOT NULL AND Solucion <> ''";
            }
            else // Todos
            {
                query = @"
            SELECT 'PC' AS Tipo, Codigo, Marca, Modelo, Problema, Solucion, FechaTrabajo, UsoIA, Comentarios
            FROM Computadoras WHERE Solucion IS NOT NULL AND Solucion <> ''
            UNION ALL
            SELECT Marca AS Tipo, Codigo, Marca, Modelo, Problema, Solucion, FechaTrabajo, UsoIA, Comentarios
            FROM Impresoras WHERE Solucion IS NOT NULL AND Solucion <> ''";
            }
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                await Task.Run(() => da.Fill(dt));
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
    }
}
