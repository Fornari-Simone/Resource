using Microsoft.EntityFrameworkCore;
using Resource.Business;
using Resource.Business.Abstraction;
using Resource.Repository;
using Resource.Repository.Abstraction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ResourceDbContext>(options =>
    options.UseSqlServer("name=ConnectionStrings:ResourceDbContext",
    a => a.MigrationsAssembly("Resource.API")));
builder.Services.AddScoped<IBusiness, Business>();
builder.Services.AddScoped<IRepository, Repository>();

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
