using System.Text.RegularExpressions;
using Domain.Abstractions;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed class Cpf : ValueObject
{
    public string Valor { get; }

    public Cpf(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("CPF é obrigatório.");

        var apenasNumeros = Regex.Replace(valor, @"\D", "");

        if (apenasNumeros.Length != 11 || !EhValido(apenasNumeros))
            throw new RegraNegocioException("CPF inválido.");

        Valor = apenasNumeros;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Valor;
    }

    private static bool EhValido(string cpf)
    {
        if (cpf.Distinct().Count() == 1)
            return false;

        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        string tempCpf = cpf[..9];
        int soma = tempCpf.Select((t, i) => (t - '0') * multiplicador1[i]).Sum();
        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();
        tempCpf += digito;

        soma = tempCpf.Select((t, i) => (t - '0') * multiplicador2[i]).Sum();
        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }
}