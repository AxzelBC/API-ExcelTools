using ExcelToolsApi.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ExcelToolsApi.Persistence.DB;

public class ApiDbContext : DbContext
{
    public DbSet<TaskModel> Task { get; set; }

    //public string DbPath { get; }
    private readonly string Connection;

    public ApiDbContext()
    {
        Connection = Environment.GetEnvironmentVariable("CONNECTION_DB") ?? "";
        //var folder = Environment.SpecialFolder.LocalApplicationData;
        //var path = Environment.GetFolderPath(folder);
        //DbPath = Path.Join(path, "tasks.db");
        //Console.WriteLine(DbPath);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite($"Data Source={DbPath}");
        => options.UseNpgsql(@Connection);
}
