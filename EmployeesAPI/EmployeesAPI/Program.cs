using EmployeesAPI.Data;
using EmployeesAPI.Domain.Repositories;
using EmployeesAPI.Profiles;
using EmployeesAPI.Domain.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<EmployeeService>();
builder.Services.AddSingleton<EmployeeContext>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employees API",
        Description = "АPI для записи и хранения данных о сотрудниках и их получением.",
        Contact = new OpenApiContact()
        {
            Name = "Сергей Акмашев",
            Email = "akmashev2002@gmail.com"
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
