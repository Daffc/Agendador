using Domain.Abstractions;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.Usuario;

public class Usuario : AggregateRoot<Guid>
{
    public string Nome { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public string SenhaHash { get; private set; } = default!;
    public TipoUsuario TipoUsuario { get; private set; }

    public Cliente? Cliente { get; private set; }

    private Usuario() { }

    public Usuario(string nome, Email email, string senhaHash, TipoUsuario tipoUsuario)
    {
        Id = Guid.NewGuid();

        DefinirNome(nome);
        Email = email;
        SenhaHash = senhaHash;
        TipoUsuario = tipoUsuario;
    }

    public void DefinirNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new RegraNegocioException("Nome é obrigatório.");

        Nome = nome;
    }

    public void DefinirSenha(string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new RegraNegocioException("Senha inválida.");

        SenhaHash = senhaHash;
    }

    public void TornarCliente(Cpf cpf, DateTime dataNascimento, Telefone telefone)
    {
        if (TipoUsuario != TipoUsuario.Cliente)
            throw new RegraNegocioException("Usuário não é do tipo Cliente.");

        Cliente = new Cliente(Id, cpf, dataNascimento, telefone);
    }
}