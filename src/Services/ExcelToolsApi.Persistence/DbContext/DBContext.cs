using ExcelToolsApi.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExcelToolsApi.Persistence.DB;

public class ApiDbContext : IdentityDbContext
{
    public DbSet<TaskModel> Task { get; set; }

    //public string DbPath { get; }
    private readonly string Connection;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }

    public ApiDbContext()
    {
        //Connection = Environment.GetEnvironmentVariable("CONNECTION_DB") ?? "";
        Connection = "Host=localhost;Port=5440;Username=postgres;Password=postgres;Database=tareasdb";
        //var folder = Environment.SpecialFolder.LocalApplicationData;
        //var path = Environment.GetFolderPath(folder);
        //DbPath = Path.Join(path, "tasks.db");
        //Console.WriteLine(DbPath);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite($"Data Source={DbPath}");
        => options.UseNpgsql(@Connection);
}
