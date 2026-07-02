# Aplicação dos Princípios SOLID

A arquitetura foi construída seguindo os princípios SOLID para garantir baixo acoplamento, alta coesão e facilidade de manutenção.

---

# Single Responsibility Principle (SRP)

Cada classe possui uma única responsabilidade.

```text
Cliente
 └── Gerencia o estado do cliente.

Produto
 └── Gerencia estoque e preço.

Pedido
 └── Controla a compra.

PedidoItem
 └── Controla um item do pedido.

PedidoService
 └── Coordena o fluxo da aplicação.
```

Dessa forma:

* Cada classe possui uma responsabilidade bem definida.
* As regras de negócio permanecem desacopladas.
* O código torna-se mais legível, reutilizável e fácil de manter.
* Novas funcionalidades podem ser adicionadas com menor impacto na aplicação.

---

# Open/Closed Principle (OCP)

As entidades são abertas para extensão e fechadas para modificação.

Exemplo:

Novos status de pedido podem ser adicionados sem alterar consumidores da entidade.

---

# Liskov Substitution Principle (LSP)

Todas as implementações podem substituir suas abstrações sem alterar o comportamento esperado.

Exemplo:

```text
IClienteRepository

↓

ClienteRepository
```

---

# Interface Segregation Principle (ISP)

As interfaces possuem responsabilidades específicas.

Exemplo:

```text
IClienteService

IProdutoService

IPedidoService
```

Cada serviço expõe apenas operações relacionadas ao seu domínio.

---

# Dependency Inversion Principle (DIP)

As camadas dependem de abstrações.

```text
Controllers

↓

Services

↓

Repositories

↓

EF Core
```

Nenhuma regra de negócio depende diretamente do Entity Framework.

---

# Benefícios Obtidos

- Código desacoplado
- Alta legibilidade
- Fácil manutenção
- Facilidade para testes
- Evolução simples do projeto