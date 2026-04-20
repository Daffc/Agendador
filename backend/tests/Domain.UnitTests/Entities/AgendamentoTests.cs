using FluentAssertions;
using Domain.Entities.Agendamento;
using Domain.Enums;
using Domain.Exceptions;

public class AgendamentoTests
{
    [Fact]
    public void Deve_criar_agendamento_valido()
    {
        var agendamento = AgendamentoFixture.CriarPadrao();

        agendamento.Status.Should().Be(StatusAgendamento.Pendente);
    }

    [Fact]
    public void Deve_confirmar_agendamento()
    {
        var agendamento = AgendamentoFixture.CriarPadrao();

        agendamento.Confirmar(DateTime.UtcNow);

        agendamento.Status.Should().Be(StatusAgendamento.Confirmado);
    }

    [Fact]
    public void Nao_deve_confirmar_se_nao_estiver_pendente()
    {
        var agendamento = AgendamentoFixture.CriarPadrao();
        agendamento.Confirmar(DateTime.UtcNow);

        Action act = () => agendamento.Confirmar(DateTime.UtcNow);

        act.Should().Throw<RegraNegocioException>();
    }

    [Fact]
    public void Deve_recusar_com_justificativa()
    {
        var agendamento = AgendamentoFixture.CriarPadrao();

        agendamento.Recusar("Indisponível");

        agendamento.Status.Should().Be(StatusAgendamento.Recusado);
    }

    [Fact]
    public void Nao_deve_recusar_sem_justificativa()
    {
        var agendamento = AgendamentoFixture.CriarPadrao();

        Action act = () => agendamento.Recusar("");

        act.Should().Throw<RegraNegocioException>();
    }

    [Fact]
    public void Deve_cancelar()
    {
        var agendamento = AgendamentoFixture.CriarPadrao();

        agendamento.Cancelar(DateTime.UtcNow, true);

        agendamento.Status.Should().Be(StatusAgendamento.Cancelado);
    }

    [Fact]
    public void Deve_concluir_agendamento()
    {
        var now = DateTime.UtcNow;

        var agendamento = new Agendamento(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            now.AddMinutes(-1),
            now.AddHours(-2));

        agendamento.Confirmar(now.AddHours(-1));
        agendamento.Concluir(now, "Atendimento realizado");

        agendamento.Status.Should().Be(StatusAgendamento.Realizado);
    }
}