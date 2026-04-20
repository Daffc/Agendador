using FluentAssertions;
using Domain.Exceptions;
using Domain.ValueObjects;

public class DataHoraAgendamentoTests
{
    [Fact]
    public void Deve_criar_data_valida()
    {
        var agora = DateTime.UtcNow;
        var data = new DataHoraAgendamento(agora.AddHours(1), agora);

        data.Valor.Should().BeAfter(agora);
    }

    [Fact]
    public void Deve_lancar_excecao_para_data_no_passado()
    {
        var agora = DateTime.UtcNow;

        Action act = () => new DataHoraAgendamento(agora.AddHours(-1), agora);

        act.Should().Throw<RegraNegocioException>();
    }
}