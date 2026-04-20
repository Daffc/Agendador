using System.Text.RegularExpressions;
using Domain.Abstractions;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Valor { get; }

    public Email(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("Email é obrigatório.");

        valor = valor.Trim().ToLowerInvariant();

        if (!Regex.IsMatch(valor, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new RegraNegocioException("Email inválido.");

        Valor = valor;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Valor;
    }
}