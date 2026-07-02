using System.Text.RegularExpressions;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Domain.ValueObjects
{
    [Owned]
    public sealed class Documento
    {
        public string Numero { get; private set; } = string.Empty;

        private Documento () { }

        public Documento(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new DomainException("O documento é obrigatório");

            numero = Regex.Replace(numero, @"\D", "");

            bool valido = numero.Length switch
            {
                11 => CpfValidator.Validar(numero),
                14 => CnpjValidator.Validar(numero),
                _ => false
            };

            if(!valido)
                throw new DomainException("CPF ou CNPJ inválido.");

            Numero = numero;
        }

        public override string ToString()
        {
            return Numero;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Documento other)
                return false;

            return Numero == other.Numero;
        }

        public override int GetHashCode()
        {
            return Numero.GetHashCode();
        }

        public static bool operator ==(Documento? left, Documento? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Documento? left, Documento? right)
        {
            return !Equals(left, right);
        }
    }
}
