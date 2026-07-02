# Regras de Negócio

Todas as regras foram implementadas dentro das entidades do domínio, evitando lógica distribuída entre Controllers e Services.

---

# Cliente

## Regras

- Cliente deve possuir CPF/CNPJ válido.
- Cliente deve possuir e-mail válido.
- Cliente pode ser ativado ou desativado.
- Cliente inativo não pode realizar pedidos.

---

# Produto

## Regras

- Produto deve possuir preço positivo.
- Produto deve possuir estoque suficiente.
- Não é permitido estoque negativo.
- Produto pode ser ativado ou desativado.

---

# Pedido

## Regras

- Pedido deve possuir pelo menos um item.
- O valor total é calculado automaticamente.
- O estoque é debitado na criação do pedido.
- O estoque é devolvido no cancelamento.

---

# Fluxo de Status

```
Criado

↓

Pago

↓

Enviado
```

Também é permitido:

```
Criado

↓

Cancelado
```

Transições inválidas geram DomainException.

---

# Histórico

Cada alteração de status gera um registro em:

HistoricoStatusPedidos

Contendo:

- Status anterior
- Novo status
- Data
- Motivo (quando existir)

Toda alteração permanece registrada para auditoria.