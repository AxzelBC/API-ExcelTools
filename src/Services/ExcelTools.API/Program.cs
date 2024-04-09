using ExcelToolsApi.Infraestructure.Extensions;
using ExcelToolsApi.Persistence.DB;
using ExcelToolsApi.JWT.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

//############ Services ############
//builder.Services.AddHealthChecks();
//builder.Services.AddHealthChecksUI().AddInMemoryStorage();
// Add services to the container.
builder.Services.AddControllers();

// Services dependecy inyection
builder.Services.AddJWTService();

// CORS
var MyAnyOriginCors = "_Any";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAnyOriginCors,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    );
});
//database
builder.Services.AddDbContext<ApiDbContext>();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
//Swagger
builder.Services.AddSwaggerOpenAPI(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();