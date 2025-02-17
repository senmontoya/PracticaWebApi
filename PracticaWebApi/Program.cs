using Microsoft.EntityFrameworkCore;
using PracticaWebApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<bibliotecaContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("biblioDbConnection")
    )
);
// inyeccion 2
//builder.Services.AddDbContext<bibliotecaContext>(options => options.UseSqlServer
//    (builder.Configuration.GetConnectionString("biblioDamian")
//    )
//);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
