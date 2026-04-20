using Domain.Abstractions;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities.Disponibilidade;

public class Disponibilidade : AggregateRoot<Guid>
{
    public Guid AtendenteId { get; private set; }
    public DiaSemana DiaSemana { get; private set; }
    public TimeSpan HoraInicio { get; private set; }
    public TimeSpan HoraFim { get; private set; }
    public bool Ativo { get; private set; }

    private Disponibilidade() { }

    public Disponibilidade(Guid atendenteId, DiaSemana diaSemana, TimeSpan inicio, TimeSpan fim)
    {
        if (fim <= inicio)
            throw new RegraNegocioException("Hora final deve ser maior que a inicial.");

        Id = Guid.NewGuid();
        AtendenteId = atendenteId;
        DiaSemana = diaSemana;
        HoraInicio = inicio;
        HoraFim = fim;
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}