using ExcelToolsApi.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ExcelToolsApi.Persistence.DBContext;

public class DBContext : DbContext
{
    public DbSet<TaskModel> Task { get; set; }

    public string DbPath { get; }

    public DBContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "tasks.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
