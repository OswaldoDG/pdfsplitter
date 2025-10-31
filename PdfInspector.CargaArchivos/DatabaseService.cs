using MySqlConnector;
using PdfInspector.CargaArchivos.models;

public static class DatabaseService
{
    private const string TableName = "archivo_pdf";

    public static bool ArchivoYaProcesado(string fileName, string connectionString)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var command = new MySqlCommand($"SELECT COUNT(*) FROM {TableName} WHERE Nombre = @fileName", connection);
        command.Parameters.AddWithValue("@fileName", fileName);
        long count = (long)command.ExecuteScalar();
        return count > 0;
    }

    public static void MarcarArchivoComoProcesado(ArchivoPdf archivo, string connectionString)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var sql = $"INSERT INTO {TableName} (Id, Nombre, Ruta, Estado, Prioridad, TotalPaginas) " +
                  "VALUES (@Id, @Nombre, @Ruta, @Estado, @Prioridad, @TotalPaginas)";

        var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Id", archivo.Id);
        command.Parameters.AddWithValue("@Nombre", archivo.Nombre);
        command.Parameters.AddWithValue("@Ruta", archivo.Ruta);
        command.Parameters.AddWithValue("@Estado", (int)archivo.Estado);
        command.Parameters.AddWithValue("@Prioridad", archivo.Prioridad);
        command.Parameters.AddWithValue("@TotalPaginas", (int)archivo.TotalPaginas);
        command.ExecuteNonQuery();
    }
}