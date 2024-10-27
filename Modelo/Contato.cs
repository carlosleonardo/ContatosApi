using System.ComponentModel.DataAnnotations;

namespace ContatosApi.Modelo;

public class Contato
{
    public int Id { get; set; }

    [StringLength(50), Required, Display(Name = "Nome")]
    public string Nome { get; set; } = null!;
    [StringLength(14), Required, Display(Name = "Telefone")]
    public string? Telefone { get; set; }
    [StringLength(50), Display(Name = "E-mail")]
    public string Email { get; set; } = null!;
    [StringLength(200), Display(Name = "Endereço")]
    public string? Endereco { get; set; }
    [StringLength(300), Display(Name = "Observação")]
    public string? Observacao { get; set; }
    [Display(Name = "Data de Nascimento")]
    public DateTime DataNascimento { get; set; }
}
