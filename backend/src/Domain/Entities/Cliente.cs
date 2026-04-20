using Domain.Abstractions;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.Usuario;

public class Cliente : Entity<Guid>
{
    public Cpf Cpf { get; private set; } = default!;
    public DateTime DataNascimento { get; private set; }
    public Telefone Telefone { get; private set; } = default!;
    public string? Observacoes { get; private set; }
    public bool Ativo { get; private set; }

    private Cliente() { }

    public Cliente(Guid usuarioId, Cpf cpf, DateTime dataNascimento, Telefone telefone)
    {
        Id = usuarioId;

        if (dataNascimento > DateTime.UtcNow)
            throw new RegraNegocioException("Data de nascimento inválida.");

        Cpf = cpf;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Ativo = true;
    }

    public void AtualizarTelefone(Telefone telefone)
    {
        Telefone = telefone;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}