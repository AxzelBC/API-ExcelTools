using System.Runtime.InteropServices;

namespace ExcelToolsApi.Domain.Model;

public class TaskModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly DateCreated { get; set; }
    public DateOnly DateModified { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}
