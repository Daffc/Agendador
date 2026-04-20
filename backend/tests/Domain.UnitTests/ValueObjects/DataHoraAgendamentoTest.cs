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

    [Fact]
    public void Deve_ser_igual_quando_valores_sao_iguais()
    {
        var agora = DateTime.UtcNow;
        var data1 = new DataHoraAgendamento(agora.AddHours(1), agora);
        var data2 = new DataHoraAgendamento(agora.AddHours(1), agora);

        data1.Should().Be(data2);
    }

    [Fact]
    public void Deve_ser_diferente_quando_valores_sao_diferentes()
    {
        var agora = DateTime.UtcNow;
        var data1 = new DataHoraAgendamento(agora.AddHours(1), agora);
        var data2 = new DataHoraAgendamento(agora.AddHours(2), agora);

        data1.Should().NotBe(data2);
    }

    [Fact]
    public void HashCode_deve_ser_igual_para_datas_iguais()
    {
        var agora = DateTime.UtcNow;
        var data1 = new DataHoraAgendamento(agora.AddHours(1), agora);
        var data2 = new DataHoraAgendamento(agora.AddHours(1), agora);

        data1.GetHashCode().Should().Be(data2.GetHashCode());
    }
}