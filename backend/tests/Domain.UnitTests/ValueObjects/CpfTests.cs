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

    [Fact]
    public void Deve_ser_igual_quando_valores_sao_iguais()
    {
        Cpf cpf1 = new Cpf("11144477735");
        Cpf cpf2 = new Cpf("111.444.777-35");

        cpf1.Should().Be(cpf2);
    }

    [Fact]
    public void Deve_ser_diferente_quando_valores_sao_diferentes()
    {
        Cpf cpf1 = new Cpf("11144477735");
        Cpf cpf2 = new Cpf("52998224725");

        cpf1.Should().NotBe(cpf2);
    }

    [Fact]
    public void HashCode_deve_ser_igual_para_cpfs_iguais()
    {
        var cpf1 = new Cpf("11144477735");
        var cpf2 = new Cpf("111.444.777-35");

        cpf1.GetHashCode().Should().Be(cpf2.GetHashCode());
    }
}