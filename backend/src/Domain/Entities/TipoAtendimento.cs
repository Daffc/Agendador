using Domain.Abstractions;
using Domain.Exceptions;

namespace Domain.Entities.TipoAtendimento;

public class TipoAtendimento : Entity<Guid>
{
    public string Nome { get; private set; } = default!;

    private TipoAtendimento() { }

    public TipoAtendimento(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new RegraNegocioException("Nome é obrigatório.");

        Id = Guid.NewGuid();
        Nome = nome;
    }
}