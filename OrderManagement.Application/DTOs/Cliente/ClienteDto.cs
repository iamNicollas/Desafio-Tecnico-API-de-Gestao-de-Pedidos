namespace OrderManagement.Application.DTOs.Cliente
{
    public class ClienteDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Documento { get; set; } = string.Empty;

        public bool Ativo { get; set; }
    }
}
