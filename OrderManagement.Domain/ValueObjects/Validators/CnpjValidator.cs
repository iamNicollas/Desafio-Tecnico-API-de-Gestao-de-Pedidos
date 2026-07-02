namespace OrderManagement.Domain.ValueObjects.Validators
{
    public static class CnpjValidator
    {
        public static bool Validar(string cnpj)
        {
            if (cnpj.Length != 14)
                return false;

            if (cnpj.All(c => c == cnpj[0]))
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp = cnpj[..12];

            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += (temp[i] - '0') * multiplicador1[i];

            int resto = soma % 11;

            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();

            temp += digito;

            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += (temp[i] - '0') * multiplicador2[i];

            resto = soma % 11;

            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
