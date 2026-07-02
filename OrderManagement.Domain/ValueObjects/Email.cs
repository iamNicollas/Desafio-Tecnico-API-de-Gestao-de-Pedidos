using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.ValueObjects
{
    // sealed class, por conta de ninguém precisar herdar dela.
    // E-mail é um Value Object, então não faz sentido ter herança.
    [Owned]
    public sealed class Email
    {
        public string Endereco { get; private set; } = string.Empty;

        private Email() { }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new DomainException("O e-mail é obrigatório.");

            endereco = endereco.Trim().ToLowerInvariant();

            if (!EmailValido(endereco))
                throw new DomainException("E-mail inválido.");

            Endereco = endereco;
        }

        private static bool EmailValido(string email)
        {
            return Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
