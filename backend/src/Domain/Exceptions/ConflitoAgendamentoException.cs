namespace Domain.Exceptions;

public class ConflitoAgendamentoException : DomainException
{
    public ConflitoAgendamentoException()
        : base("Já existe um agendamento para o atendente neste horário.")
    {
    }
}