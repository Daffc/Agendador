using FluentAssertions;
using Domain.Entities.Disponibilidade;
using Domain.Enums;

public class DisponibilidadeTests
{
    [Fact]
    public void Deve_criar_disponibilidade_valida()
    {
        var disp = new Disponibilidade(
            Guid.NewGuid(),
            DiaSemana.Segunda,
            TimeSpan.FromHours(8),
            TimeSpan.FromHours(18));

        disp.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Deve_lancar_excecao_se_hora_final_menor()
    {
        Action act = () => new Disponibilidade(
            Guid.NewGuid(),
            DiaSemana.Segunda,
            TimeSpan.FromHours(18),
            TimeSpan.FromHours(8));

        act.Should().Throw<Exception>();
    }
}