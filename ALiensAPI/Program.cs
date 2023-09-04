using APIALiens.Data;
using APIALiens.Service.Interfaces;
using APIALiens.Service;
using Microsoft.EntityFrameworkCore;
using APIALiens.EmailModule;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adicionando os escopos e as conex�es com o banco 
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAlienService, AlienService>();
builder.Services.AddScoped<IPlanetaService, PlanetaService>();
builder.Services.AddScoped<IPoderService, PoderService>();
builder.Services.AddScoped<ISmtp, Smtp>();
//adicionando o m�todo do cors 
builder.Services.AddCors(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//liberando o acesso de qualquer porta 
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
