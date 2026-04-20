using FluentAssertions;
using Domain.ValueObjects;

public class PeriodoTests
{
    [Theory]
    [InlineData("2024-01-01 10:00", "2024-01-01 12:00", "2024-01-01 11:00", "2024-01-01 13:00")]  // Sobreposição parcial
    [InlineData("2024-01-01 10:00", "2024-01-01 12:00", "2024-01-01 10:00", "2024-01-01 12:00")]  // Exatamente igual
    [InlineData("2024-01-01 10:00", "2024-01-01 14:00", "2024-01-01 11:00", "2024-01-01 13:00")]  // Contido dentro
    [InlineData("2024-01-01 11:00", "2024-01-01 13:00", "2024-01-01 10:00", "2024-01-01 14:00")]  // Contém
    public void Deve_detectar_conflito(
        string inicio1,
        string fim1,
        string inicio2,
        string fim2)
    {
        {
            var p1 = new Periodo(DateTime.Parse(inicio1), DateTime.Parse(fim1));
            var p2 = new Periodo(DateTime.Parse(inicio2), DateTime.Parse(fim2));

            p1.ConflitaCom(p2).Should().BeTrue();
        }
    }


    [Theory]
    [InlineData("2024-01-01 10:00", "2024-01-01 12:00", "2024-01-01 09:00", "2024-01-01 10:00")] // Termina quando começa (adjacente)
    [InlineData("2024-01-01 10:00", "2024-01-01 12:00", "2024-01-01 12:00", "2024-01-01 14:00")] // Começa quando termina (adjacente)
    [InlineData("2024-01-01 10:00", "2024-01-01 12:00", "2024-01-01 08:00", "2024-01-01 09:00")] // Antes
    [InlineData("2024-01-01 10:00", "2024-01-01 12:00", "2024-01-01 13:00", "2024-01-01 15:00")] // Depois
    public void Nao_deve_detectar_conflito(
        string inicio1,
        string fim1,
        string inicio2,
        string fim2)
    {
        {
            var p1 = new Periodo(DateTime.Parse(inicio1), DateTime.Parse(fim1));
            var p2 = new Periodo(DateTime.Parse(inicio2), DateTime.Parse(fim2));

            p1.ConflitaCom(p2).Should().BeFalse();
        }
    }

    [Fact]
    public void Deve_ser_igual_quando_valores_sao_iguais()
    {
        var p1 = new Periodo(DateTime.Parse("2024-01-01 10:00"), DateTime.Parse("2024-01-01 12:00"));
        var p2 = new Periodo(DateTime.Parse("2024-01-01 10:00"), DateTime.Parse("2024-01-01 12:00"));

        p1.Should().Be(p2);
    }

    [Fact]
    public void Deve_ser_diferente_quando_valores_sao_diferentes()
    {
        var p1 = new Periodo(DateTime.Parse("2024-01-01 10:00"), DateTime.Parse("2024-01-01 12:00"));
        var p2 = new Periodo(DateTime.Parse("2024-01-01 12:00"), DateTime.Parse("2024-01-01 15:00"));

        p1.Should().NotBe(p2);
    }

    [Fact]
    public void HashCode_deve_ser_igual_para_periodos_iguais()
    {
        var p1 = new Periodo(DateTime.Parse("2024-01-01 10:00"), DateTime.Parse("2024-01-01 12:00"));
        var p2 = new Periodo(DateTime.Parse("2024-01-01 10:00"), DateTime.Parse("2024-01-01 12:00"));

        p1.GetHashCode().Should().Be(p2.GetHashCode());
    }
}