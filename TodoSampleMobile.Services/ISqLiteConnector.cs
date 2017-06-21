using SQLite;

namespace TodoSampleMobile.Services
{
    public interface ISqLiteConnector
    {
        string DatabaseFilename { get; }
        SQLiteConnection GetConnection(bool recreate = false);
    }
}