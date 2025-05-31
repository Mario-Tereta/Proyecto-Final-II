using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TechHelper_AI
{
    public partial class ContConsulta : Form
    {
        public event Action<int, string> ContinuarConsulta;
        public ContConsulta()
        {
            InitializeComponent();
            _ = CargarConsultasPendientesAsync();
        }

        private async Task CargarConsultasPendientesAsync()
        {
            string query = @"
        SELECT 'PC' AS Tipo, Id, Codigo, Marca, Modelo, Problema, FechaTrabajo, Comentarios
        FROM Computadoras
        WHERE Solucion IS NULL OR Solucion = ''
        UNION ALL
        SELECT Marca, Id, Codigo, Marca, Modelo, Problema, FechaTrabajo, Comentarios
        FROM Impresoras
        WHERE Solucion IS NULL OR Solucion = ''";
            using (SqlConnection conn = new SqlConnection(ConexionBD.ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                await Task.Run(() => da.Fill(dt));
                dataGridView1.DataSource = dt;
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow == null)
    {
                MessageBox.Show("Seleccione una consulta para continuar.");
                return;
            }
            var row = dataGridView1.CurrentRow;
            string tipo = row.Cells["Tipo"].Value.ToString();
            int id = Convert.ToInt32(row.Cells["Id"].Value);

            // Notifica al padre
            ContinuarConsulta?.Invoke(id, tipo);
        }
    }
}
