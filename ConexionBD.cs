using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;

namespace TechHelper_AI
{
    public static class ConexionBD
    {
        // Cambia esta cadena según tu configuración local
        public static readonly string ConnectionString = "Data Source=TERETA-PC\\SQLEXPRESS;Initial Catalog=TechHelper_DB;Integrated Security=True;TrustServerCertificate=True;";

        // Método opcional para obtener una nueva conexión
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
//resp_6836d5e85eb88198a848a72356ca146a04c4c51cb7a1cad7