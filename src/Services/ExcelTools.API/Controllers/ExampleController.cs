using ExcelToolsApi.Domain.Model;
using ExcelToolsApi.Persistence.DBContext;
using Microsoft.AspNetCore.Mvc;

namespace ExcelTools.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ExampleController : ControllerBase
{
    private readonly DBContext _dbContext;

    public ExampleController(DBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet("/")]
    public List<TaskModel> GetExample()
    {
        var listTask = _dbContext.Task.ToList();
        return listTask;
    }
}
