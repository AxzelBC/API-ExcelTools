using ExcelToolsApi.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ExcelToolsApi.Persistence.DB;

public class ApiDbContext : DbContext
{
    public DbSet<TaskModel> Task { get; set; }

    //public string DbPath { get; }

    public ApiDbContext()
    {
        //var folder = Environment.SpecialFolder.LocalApplicationData;
        //var path = Environment.GetFolderPath(folder);
        //DbPath = Path.Join(path, "tasks.db");
        //Console.WriteLine(DbPath);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite($"Data Source={DbPath}");
        => options.UseNpgsql(@"Host=exceltools.database:5432;Username=postgres;Password=postgres;Database=tareasdb");
}
