using ExcelToolsApi.Domain;
using ExcelToolsApi.Domain.Model;
using ExcelToolsApi.Persistence.DB;
using Microsoft.AspNetCore.Mvc;

namespace ExcelTools.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ExampleController : ControllerBase
{
    private readonly ApiDbContext _apiDbContext;

    public ExampleController(ApiDbContext dBContext)
    {
        _apiDbContext = dBContext;
    }

    [HttpGet]
    public List<TaskModel> GetExample()
    {
        var listTask = _apiDbContext.Task.ToList();
        return listTask;
    }

    [HttpPost]
    public TaskModel PostExample([FromBody] TaskDTO dto)
    {
        var model = new TaskModel
        {
            Name = dto.Name,
            Description = dto.Description,
            DateCreated = DateOnly.FromDateTime(DateTime.Now)
        };

        var resutl = _apiDbContext.Task.Add(model);
        _apiDbContext.SaveChanges();

        return resutl.Entity;
    }
}
