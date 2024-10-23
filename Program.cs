using ContatosApi.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var stringConexao = builder.Configuration.GetConnectionString("ConexaoBD") ?? "Data Source=Contatos.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Contatos API",
		Description = "Mantendo uma lista de Contatos",
		Version = "v1"
	});
});
builder.Services.AddDbContext<ContatosDBContext>(options => options.UseSqlite(stringConexao));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1");
	});
}


app.MapGet("/", () => "Hello World!");

app.Run();
