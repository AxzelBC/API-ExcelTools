using ExcelToolsApi.Infraestructure.Extensions;
using ExcelToolsApi.Persistence.DBContext;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

builder.Services.AddHealthChecks();
//builder.Services.AddHealthChecksUI().AddInMemoryStorage();

// Add services to the container.
builder.Services.AddControllers();

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
builder.Services.AddDbContext<DBContext>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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