using Domain.Abstractions;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed class DataHoraAgendamento : ValueObject
{
    public DateTime Valor { get; }

    public DataHoraAgendamento(DateTime valor, DateTime agora)
    {
        if (valor < agora)
            throw new RegraNegocioException("Data do agendamento não pode ser no passado.");

        Valor = valor;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Valor;
    }
}