using ExcelToolsApi.Domain;
using ExcelToolsApi.Domain.Model;
using ExcelToolsApi.Persistence.Identity;
using ExcelToolsApi.PersistenceExcel.Excel;
using Microsoft.AspNetCore.Mvc;

namespace ExcelTools.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ExampleController : ControllerBase
{
    private readonly ApiIdentityDbContext _apiIdentityDbContext;
    private readonly ApiExcelDbContext _apiExcelDbContex;

    public ExampleController(
        ApiIdentityDbContext identityDbContext,
        ApiExcelDbContext excelDbContext
    )
    {
        _apiIdentityDbContext = identityDbContext;
        _apiExcelDbContex = excelDbContext;
    }

    [HttpGet]
    public List<TaskModel> GetExample()
    {
        var listTask = _apiIdentityDbContext.Task.ToList();
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

        var resutl = _apiIdentityDbContext.Task.Add(model);
        _apiIdentityDbContext.SaveChanges();

        return resutl.Entity;
    }

    [HttpPost("excel")]
    public async Task PostExampleExcel([FromBody] string name)
    {
        var movie = new MovieModel()
        {
            TitleMovie = name
        };
        await _apiExcelDbContex.CreateAsync(movie);
    }

    [HttpGet("excel")]
    public async Task<List<MovieModel>> GetExampleExcel()
    {
        var listMovies = await _apiExcelDbContex.GetMoviesAsync();
        return listMovies;
    }
}
