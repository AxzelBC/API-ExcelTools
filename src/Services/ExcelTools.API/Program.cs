using ExcelToolsApi.Infraestructure.Extensions;
using ExcelToolsApi.Persistence.DB;
using ExcelToolsApi.JWT.Service;
using ExcelToolsApi.Infraestructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ExcelToolsApi.Persistence;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseContentRoot(Directory.GetCurrentDirectory());

//############ Services ############
//builder.Services.AddHealthChecks();
//builder.Services.AddHealthChecksUI().AddInMemoryStorage();
// Add services to the container.
builder.Services.AddControllers();

// Services dependecy inyection
builder.Services
.AddJWTService()
.AddInfrastructure(builder.Configuration)
.AddPersistence();

// auth 
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ApiDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes("super-secret-key-with-all-powers")),
    ClockSkew = TimeSpan.Zero
});

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
// builder.Services.AddDbContext<ApiDbContext>();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
//Swagger
builder.Services.AddSwaggerGen(
    c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
            }
        });
    }
);

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