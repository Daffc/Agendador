using Domain.Abstractions;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed class Periodo : ValueObject
{
    public DateTime Inicio { get; }
    public DateTime Fim { get; }

    public Periodo(DateTime inicio, DateTime fim)
    {
        if (fim <= inicio)
            throw new RegraNegocioException("Período inválido.");

        Inicio = inicio;
        Fim = fim;
    }

    public bool ConflitaCom(Periodo outro)
    {
        return Inicio < outro.Fim && Fim > outro.Inicio;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Inicio;
        yield return Fim;
    }
}