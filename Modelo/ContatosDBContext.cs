using Microsoft.EntityFrameworkCore;

namespace ContatosApi.Modelo;

public class ContatosDBContext : DbContext
{
	public DbSet<Contato> Contatos { get; set; }
	public ContatosDBContext(DbContextOptions<ContatosDBContext> options) : base(options)
	{

	}
}