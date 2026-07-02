# Modelo de Domínio

## Objetivo

O domínio foi modelado seguindo princípios de Domain-Driven Design (DDD), onde cada entidade possui responsabilidades bem definidas e é responsável por proteger suas próprias regras de negócio.

As regras são encapsuladas nas entidades, evitando que a camada de aplicação manipule diretamente seus estados internos.

---

# Cliente

## Responsabilidade

Representar um cliente apto a realizar pedidos.

## Regras de Negócio

- Cliente inativo não pode criar pedidos.
- Apenas a própria entidade pode alterar seu estado.

## A entidade conhece

- Seu nome
- Seu e-mail
- Seu documento
- Seu status (ativo/inativo)

## Propriedades

| Propriedade | Descrição |
|-------------|-----------|
| Id | Identificador |
| Nome | Nome do cliente |
| Email | Value Object |
| Documento | Value Object |
| Ativo | Situação do cliente |
| CreatedAt | Data de criação |
| UpdatedAt | Última alteração |

## Comportamentos

| Método | Responsabilidade |
|---------|------------------|
| Ativar() | Ativa o cliente |
| Desativar() | Desativa o cliente |
| PodeCriarPedido() | Verifica se está apto a comprar |

---

# Produto

## Responsabilidade

Representar um produto disponível para venda.

## Regras de Negócio

- O estoque nunca pode ser negativo.
- O preço deve ser maior que zero.

## A entidade conhece

- Seu estoque
- Seu preço
- Sua disponibilidade

## Propriedades

| Propriedade |
|-------------|
| Id |
| Nome |
| Descricao |
| Preco |
| Estoque |
| Ativo |
| CreatedAt |
| UpdatedAt |

## Comportamentos

| Método |
|---------|
| AlterarNome() |
| AlterarDescricao() |
| AlterarPreco() |
| DefinirEstoque() |
| PossuiEstoque() |
| DebitarEstoque() |
| ReporEstoque() |
| Ativar() |
| Desativar() |

---

# Pedido

## Responsabilidade

Representar uma compra realizada por um cliente.

O Pedido é o Aggregate Root da aplicação.

## Regras de Negócio

- Deve possuir pelo menos um item.
- Controla a transição entre status.
- Calcula automaticamente o valor total.
- Mantém histórico das alterações.

## Propriedades

| Propriedade |
|-------------|
| Id |
| ClienteId |
| Cliente |
| Status |
| ValorTotal |
| Itens |
| Histórico |
| CreatedAt |

## Comportamentos

| Método |
|---------|
| AdicionarItem() |
| ValidarPedido() |
| MarcarComoPago() |
| MarcarComoEnviado() |
| Cancelar() |

---

## Exemplo

```csharp
pedido.AdicionarItem(item);
```

Ao adicionar um item, o Pedido automaticamente:

- recalcula o valor total;
- mantém sua consistência;
- permanece responsável pelo próprio estado.

---

# PedidoItem

## Responsabilidade

Representar um produto dentro de um pedido.

## Propriedades

| Propriedade |
|-------------|
| Id |
| PedidoId |
| ProdutoId |
| Quantidade |
| PrecoUnitario |
| ValorTotal |

## Comportamentos

| Método |
|---------|
| AlterarQuantidade() |
| DefinirPrecoUnitario() |

O valor total é recalculado automaticamente.

---

# Histórico de Status

## Responsabilidade

Registrar todas as mudanças de status do pedido.

## Propriedades

| Propriedade |
|-------------|
| PedidoId |
| StatusAnterior |
| NovoStatus |
| Motivo |
| DataAlteracao |

---

## Considerações

Toda regra de negócio permanece encapsulada nas entidades.

A camada Application apenas coordena o fluxo da aplicação, respeitando o princípio de domínio rico (Rich Domain Model).