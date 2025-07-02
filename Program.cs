using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServiceApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Налаштування контексту бази даних
builder.Services.AddDbContext<ServiceDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service API", Version = "v1" });
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Централізована обробка помилок
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error"); // або кастомний middleware
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
