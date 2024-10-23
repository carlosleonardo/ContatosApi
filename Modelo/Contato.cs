using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContatosApi.Modelo;

public class Contato
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Telefone { get; set; }
    public string Email { get; set; } = null!;
    public string? Endereco { get; set; }
    public string? Observacao { get; set; }
    public DateOnly DataNascimento { get; set; }
}
