using MySqlConnector;
using PdfInspector.Separador.models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Separador
{
    public static class DatabaseService
    {
        public static async Task<ArchivoPdf> ObtieneArchivoFinalizado(string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var command = new MySqlCommand(
                "SELECT Id, Nombre, Ruta, Estado,IdProceso FROM archivo_pdf WHERE Estado = @Estado and IdProceso is null LIMIT 1",
                connection);

            command.Parameters.AddWithValue("@Estado", (int)EstadoRevision.Finalizada);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var archivo = new ArchivoPdf
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Ruta = reader.GetString("Ruta"),
                    Estado = (EstadoRevision)reader.GetInt32("Estado"),
                };

                archivo.IdProceso = reader.IsDBNull("IdProceso") ? null : reader.GetString("IdProceso");
                command.Dispose();
                connection.Close();
                return archivo;
            }

            command.Dispose();
            connection.Close();
            return null;
        }

        public static async Task<ArchivoPdf> PorId(string connectionString, int id)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var command = new MySqlCommand(
                "SELECT Id, Nombre, Ruta, Estado, IdProceso FROM archivo_pdf WHERE Id = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {

                var archivo = new ArchivoPdf
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Ruta = reader.GetString("Ruta"),
                    Estado = (EstadoRevision)reader.GetInt32("Estado"),
                };

                archivo.IdProceso = reader.IsDBNull("IdProceso") ? null : reader.GetString("IdProceso");
                command.Dispose();
                connection.Close();
                return archivo;
            }

            command.Dispose();
            connection.Close();
            return null;
        }

        public static async Task<List<ParteDocumental>> ObtienePartesDocumental(int archivoPdfId, string connectionString)
        {
            var parts = new List<ParteDocumental>();
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var command = new MySqlCommand(
                "SELECT Id, ArchivoPdfId, TipoDocumentoId, PaginaInicio, PaginaFin, IdAgrupamiento FROM parte_documental WHERE ArchivoPdfId = @ArchivoPdfId",
                connection);

            command.Parameters.AddWithValue("@ArchivoPdfId", archivoPdfId);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                parts.Add(new ParteDocumental
                {
                    Id = reader.GetInt32("Id"),
                    ArchivoPdfId = reader.GetInt32("ArchivoPdfId"),
                    TipoDocumentoId = reader.GetInt32("TipoDocumentoId"),
                    PaginaInicio = reader.GetInt32("PaginaInicio"),
                    PaginaFin = reader.GetInt32("PaginaFin"),
                    IdAgrupamiento = reader.IsDBNull(reader.GetOrdinal("IdAgrupamiento")) ? (int?)null : reader.GetInt32("IdAgrupamiento")
                });
            }

            command.Dispose();
            connection.Close();

            return parts;
        }

        public static async Task<StringDictionary> ObtieneTiposDocumento(string connectionString)
        {
            StringDictionary sd = new StringDictionary();
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var command = new MySqlCommand("SELECT Id, Nombre FROM tipo_documento", connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                sd.Add(reader.GetInt32("Id").ToString(), reader.GetString("Nombre"));   
            }

            command.Dispose();
            connection.Close();
            return sd;   
        }

        public static async Task ActualizaEstadoArchivo(int archivoPdfId, models.EstadoRevision newStatus, string connectionString, string procesoId)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            var command = new MySqlCommand("UPDATE archivo_pdf SET Estado = @Estado, IdProceso=@IdProceso WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Estado", (int)newStatus);
            command.Parameters.AddWithValue("@Id", archivoPdfId);
            command.Parameters.AddWithValue("@IdProceso", procesoId);


            await command.ExecuteNonQueryAsync();
            command.Dispose();
            connection.Close();
        }

        public static void ActualizaRutaParte(int id, string ruta, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            var command = new MySqlCommand("UPDATE parte_documental SET RutaArchivo = @Ruta WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Ruta", ruta);
            command.ExecuteNonQuery();
            command.Dispose();  
            connection.Close(); 
        }

        public static async Task EliminaZombies(string connectionString)
        {
            using var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();

            var query = @"UPDATE archivo_pdf SET Estado = 2 WHERE Estado = 5 AND UltimaRevision <= DATE_SUB(UTC_TIMESTAMP(), INTERVAL 1 HOUR);";

            using var cmd = new MySqlCommand(query, conn);
            await cmd.ExecuteNonQueryAsync();
            cmd.Dispose();
            conn.Close();
        }
    }
}
