namespace Domain.Exceptions;

public class EntidadeNaoEncontradaException : DomainException
{
    public EntidadeNaoEncontradaException(string entidade, object id)
        : base($"{entidade} com id '{id}' não foi encontrada.")
    {
    }
}