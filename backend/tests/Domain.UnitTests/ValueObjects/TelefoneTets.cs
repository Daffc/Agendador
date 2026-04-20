using FluentAssertions;
using Domain.ValueObjects;
using Domain.Exceptions;

public class TelefoneTests
{
    [Theory]
    [InlineData("(41) 99999-9999", "41999999999")]
    [InlineData("(11) 91234-5678", "11912345678")]
    [InlineData("(11) 1234-5678", "1112345678")]
    public void Deve_normalizar_diferentes_formatos_telefone(string entrada, string saida)
    {
        var tel = new Telefone(entrada);

        tel.Valor.Should().Be(saida);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("")]
    [InlineData("(11) 991234-5678")]
    [InlineData("(11) 1345-567")]
    public void Deve_lancar_excecao_para_telefone_invalido(string valor)
    {
        Action act = () => new Telefone(valor);

        act.Should().Throw<RegraNegocioException>();
    }
}