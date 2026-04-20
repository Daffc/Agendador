using FluentAssertions;
using Domain.Entities.Usuario;
using Domain.Enums;
using Domain.ValueObjects;

public class UsuarioTests
{
    [Fact]
    public void Deve_criar_usuario_valido()
    {
        var usuario = new Usuario(
            "Douglas",
            new Email("teste@email.com"),
            "hash",
            TipoUsuario.Cliente);

        usuario.Nome.Should().Be("Douglas");
    }

    [Fact]
    public void Deve_lancar_excecao_para_nome_invalido()
    {
        Action act = () => new Usuario(
            "",
            new Email("teste@email.com"),
            "hash",
            TipoUsuario.Cliente);

        act.Should().Throw<Exception>();
    }
}