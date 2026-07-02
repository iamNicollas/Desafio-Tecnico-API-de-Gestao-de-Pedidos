namespace OrderManagement.Application.DTOs.Cliente
{
    public class UpdateClienteDto
    {
        // Não permitimos alterar o documento, pois normalmente CPF/CNPJ é um identificador permanente.

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
