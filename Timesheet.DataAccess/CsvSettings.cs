namespace Timesheet.DataAccess.CSV
{
    public class CsvSettings
    {
        public CsvSettings(string path, char delimeter)
        {
            Path = path;
            Delimeter = delimeter;
        }

        public string Path { get; }
        public char Delimeter { get; }
    }
}
