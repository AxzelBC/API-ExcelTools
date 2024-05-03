using ExcelToolsApi.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExcelToolsApi.Persistence.Identity;

public class ApiIdentityDbContext : IdentityDbContext

{
    public DbSet<TaskModel> Task { get; set; }

    //public string DbPath { get; }
    private readonly string Connection;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }

    public ApiIdentityDbContext()
    {
        Connection = Environment.GetEnvironmentVariable("IDENTITY_CONNECTION_DB") ?? "";
        //var folder = Environment.SpecialFolder.LocalApplicationData;
        //var path = Environment.GetFolderPath(folder);
        //DbPath = Path.Join(path, "tasks.db");
        //Console.WriteLine(DbPath);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite($"Data Source={DbPath}");
        => options.UseNpgsql(@Connection);
}
