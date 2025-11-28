namespace PdfInspector.Separador
{
    public static class ErrorLog
    {
        private static readonly string _tempDir;
        private static readonly string _logPath;

        static ErrorLog()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "pdfsplitter");
            _logPath = Path.Combine(_tempDir, "log.txt");

            if (!Directory.Exists(_tempDir))
                Directory.CreateDirectory(_tempDir);
        }

        public static void Log(Exception ex)
        {
            try
            {
                File.AppendAllText(_logPath,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\n{ex}\n\n");
            }
            catch
            {
            }
        }
    }
}
