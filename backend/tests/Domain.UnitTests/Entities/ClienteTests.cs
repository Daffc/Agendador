using FluentAssertions;
using Domain.ValueObjects;
using Domain.Entities.Usuario;

public class ClienteTests
{
    [Fact]
    public void Deve_criar_cliente_valido()
    {
        var cliente = new Cliente(
            Guid.NewGuid(),
            new Cpf("11144477735"),
            DateTime.UtcNow.AddYears(-20),
            new Telefone("41999999999"));

        cliente.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Deve_invalidar_data_nascimento_futura()
    {
        Action act = () => new Cliente(
            Guid.NewGuid(),
            new Cpf("11144477735"),
            DateTime.UtcNow.AddDays(1),
            new Telefone("41999999999"));

        act.Should().Throw<Exception>();
    }
}