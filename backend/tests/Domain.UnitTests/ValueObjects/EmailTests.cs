using FluentAssertions;
using Domain.ValueObjects;
using Domain.Exceptions;

public class EmailTests
{
    [Theory]
    [InlineData(" TESTE@EMAIL.COM ", "teste@email.com")]
    [InlineData("  João.Silva@Empresa.com.br  ", "joão.silva@empresa.com.br")]
    [InlineData("USER@DOMAIN.COM", "user@domain.com")]
    [InlineData("user.name+tag@example.co.uk", "user.name+tag@example.co.uk")]
    [InlineData("  admin@SISTEMA.gov.br  ", "admin@sistema.gov.br")]
    [InlineData("primeiro.ultimo@sub.dominio.com.br", "primeiro.ultimo@sub.dominio.com.br")]
    [InlineData("a@b.c", "a@b.c")]
    public void Deve_normalizar_diferentes_formatos_email(string entrada, string esperado)
    {
        var email = new Email(entrada);

        email.Valor.Should().Be(esperado);
    }

    [Theory]
    [InlineData("invalido")]
    [InlineData("")]
    [InlineData("   ")]
    public void Deve_lancar_excecao_para_email_invalido(string valor)
    {
        Action act = () => new Email(valor);

        act.Should().Throw<RegraNegocioException>();
    }
}