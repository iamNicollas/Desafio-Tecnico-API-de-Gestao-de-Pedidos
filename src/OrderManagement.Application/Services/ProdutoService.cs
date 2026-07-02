using OrderManagement.Application.DTOs.Produto;
using OrderManagement.Application.Interfaces;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Services
{
    // Principio SOLID - DIP de depender da abstração e não da implementação
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodosAsync()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();

            return produtos.Select(produto => new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                Ativo = produto.Ativo
            });
        }

        public async Task<ProdutoDto> ObterPorIdAsync(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            if (produto is null)
                throw new DomainException("Produto não encontrado.");

            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                Ativo = produto.Ativo
            };
        }

        public async Task<Guid> CriarAsync(CreateProdutoDto dto)
        {
            var produto = new Produto(
                dto.Nome,
                dto.Descricao,
                dto.Preco,
                dto.Estoque);

            await _produtoRepository.AdicionarAsync(produto);

            return produto.Id;
        }

        public async Task AtualizarAsync(Guid id, UpdateProdutoDto dto)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            if (produto is null)
                throw new DomainException("Produto não encontrado.");

            // Domain - Quem modifica o estado da entidade é a própria entidade
            produto.AlterarNome(dto.Nome);
            produto.AlterarDescricao(dto.Descricao);
            produto.AlterarPreco(dto.Preco);

            produto.DefinirEstoque(dto.Estoque);

            await _produtoRepository.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            if (produto is null)
                throw new DomainException("Produto não encontrado.");

            await _produtoRepository.RemoverAsync(id);
        }
    }
}
