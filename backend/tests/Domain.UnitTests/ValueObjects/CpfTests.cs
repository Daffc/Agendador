using Domain.ValueObjects;
using Domain.Exceptions;
using FluentAssertions;
using Xunit;

public class CpfTests
{
    [Fact]
    public void Deve_criar_cpf_valido()
    {
        var cpf = new Cpf("11144477735");

        cpf.Valor.Should().Be("11144477735");
    }

    [Theory]
    [InlineData("123")]
    [InlineData("00000000000")]
    [InlineData("")]
    [InlineData("AAAAAAAA")]
    public void Deve_lancar_excecao_para_cpf_invalido(string valor)
    {
        Action act = () => new Cpf(valor);

        act.Should().Throw<RegraNegocioException>();
    }
}