using System.Text.RegularExpressions;
using Domain.Abstractions;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed class Telefone : ValueObject
{
    public string Valor { get; }

    public Telefone(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("Telefone é obrigatório.");

        var apenasNumeros = Regex.Replace(valor, @"\D", "");

        if (apenasNumeros.Length < 10 || apenasNumeros.Length > 11)
            throw new RegraNegocioException("Telefone inválido.");

        Valor = apenasNumeros;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Valor;
    }
}