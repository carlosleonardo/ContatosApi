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
builder.Services.AddCors(options =>
{
	options.AddPolicy("Permissoes", optionsBuilder =>
	{
		optionsBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
});

var app = builder.Build();
app.UseCors("Permissoes");
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contatos API V1");
	});
}

var contatosMap = app.MapGroup("/contatos");

contatosMap.MapGet("/", async (ContatosDBContext db) =>
{
	return TypedResults.Ok(await db.Contatos.ToListAsync());
});

contatosMap.MapGet("/{id}", async Task<IResult> (ContatosDBContext db, int id) =>
{
	return await db.Contatos.FindAsync(id)
		is Contato contato
		? TypedResults.Ok(contato)
		: TypedResults.NotFound();
});
contatosMap.MapPost("/", async (ContatosDBContext db, Contato contato) =>
{
	Console.WriteLine("Data de Nascimento", contato.DataNascimento);
	db.Contatos.Add(contato);
	await db.SaveChangesAsync();
	return TypedResults.Created($"/contatos/{contato.Id}", contato);
});
contatosMap.MapPut("/{id}", async Task<IResult> (ContatosDBContext db, Contato contatoAtualizado, int id) =>
{
	var contato = await db.Contatos.FindAsync(id);
	if (contato is null) return TypedResults.NotFound();
	contato.Nome = contatoAtualizado.Nome;
	contato.Telefone = contatoAtualizado.Telefone;
	contato.Email = contatoAtualizado.Email;
	contato.Endereco = contatoAtualizado.Endereco;
	contato.Observacao = contatoAtualizado.Observacao;
	contato.DataNascimento = contatoAtualizado.DataNascimento;
	await db.SaveChangesAsync();
	return TypedResults.NoContent();
});
contatosMap.MapDelete("/{id}", async Task<IResult> (ContatosDBContext db, int id) =>
{
	var contato = await db.Contatos.FindAsync(id);
	if (contato is null) return TypedResults.NotFound();
	db.Contatos.Remove(contato);
	await db.SaveChangesAsync();
	return TypedResults.NoContent();
});

app.Run();
