using Domain.Abstractions;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Agendamento;

public class Agendamento : AggregateRoot<Guid>
{
    public Guid ClienteId { get; private set; }
    public Guid AtendenteId { get; private set; }
    public Guid TipoAtendimentoId { get; private set; }

    public DateTime DataHora { get; private set; }
    public StatusAgendamento Status { get; private set; }

    public string? Observacoes { get; private set; }
    public string? JustificativaRecusa { get; private set; }
    public string? ResumoAtendimento { get; private set; }

    public DateTime DataCriacao { get; private set; }
    public DateTime? DataConfirmacao { get; private set; }
    public DateTime? DataCancelamento { get; private set; }

    private Agendamento() { }

    public Agendamento(
        Guid clienteId,
        Guid atendenteId,
        Guid tipoAtendimentoId,
        DateTime dataHora,
        DateTime agora)
    {
        if (dataHora < agora)
            throw new RegraNegocioException("Não é possível agendar no passado.");

        Id = Guid.NewGuid();
        ClienteId = clienteId;
        AtendenteId = atendenteId;
        TipoAtendimentoId = tipoAtendimentoId;
        DataHora = dataHora;

        Status = StatusAgendamento.Pendente;
        DataCriacao = agora;
    }

    public void Confirmar(DateTime agora)
    {
        if (Status != StatusAgendamento.Pendente)
            throw new RegraNegocioException("Agendamento não pode ser confirmado.");

        Status = StatusAgendamento.Confirmado;
        DataConfirmacao = agora;
    }

    public void Recusar(string justificativa)
    {
        if (Status != StatusAgendamento.Pendente)
            throw new RegraNegocioException("Agendamento não pode ser recusado.");

        if (string.IsNullOrWhiteSpace(justificativa))
            throw new RegraNegocioException("Justificativa é obrigatória.");

        Status = StatusAgendamento.Recusado;
        JustificativaRecusa = justificativa;
    }

    public void Cancelar(DateTime agora, bool ehAdmin)
    {
        if (Status == StatusAgendamento.Realizado)
            throw new RegraNegocioException("Não é possível cancelar um agendamento finalizado.");

        if (!ehAdmin && DataHora <= agora)
            throw new RegraNegocioException("Não é possível cancelar após o horário.");

        Status = StatusAgendamento.Cancelado;
        DataCancelamento = agora;
    }

    public void Concluir(DateTime agora, string? resumo)
    {
        if (Status != StatusAgendamento.Confirmado)
            throw new RegraNegocioException("Somente agendamentos confirmados podem ser concluídos.");

        if (DataHora > agora)
            throw new RegraNegocioException("Não é possível concluir antes do horário.");

        Status = StatusAgendamento.Realizado;
        ResumoAtendimento = resumo;
    }

    public void ReatribuirAtendente(Guid novoAtendenteId)
    {
        if (Status == StatusAgendamento.Realizado)
            throw new RegraNegocioException("Não é possível alterar um agendamento finalizado.");

        AtendenteId = novoAtendenteId;
    }
}