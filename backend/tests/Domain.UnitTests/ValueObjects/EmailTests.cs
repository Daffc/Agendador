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

    [Fact]
    public void Deve_ser_igual_quando_valores_sao_iguais()
    {
        var email1 = new Email("teste@email.com");
        var email2 = new Email(" TESTE@EMAIL.COM ");

        email1.Should().Be(email2);
    }

    [Fact]
    public void Deve_ser_diferente_quando_valores_sao_diferentes()
    {
        var email1 = new Email("teste@email.com");
        var email2 = new Email("USER@DOMAIN.COM");

        email1.Should().NotBe(email2);
    }

    [Fact]
    public void HashCode_deve_ser_igual_para_emails_iguais()
    {
        var email1 = new Email("teste@email.com");
        var email2 = new Email("teste@email.com");

        email1.GetHashCode().Should().Be(email2.GetHashCode());
    }
}