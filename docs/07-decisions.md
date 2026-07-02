# Decisões Técnicas

## Clean Architecture

Escolhida para separar responsabilidades.

---

## DDD

As regras pertencem ao domínio.

Não aos Controllers.

---

## Repository Pattern

Evita dependência direta do Entity Framework.

---

## Value Objects

Utilizados para:

- Documento
- Email

Garantindo validação na própria criação do objeto.

---

## Entity Framework Core

Escolhido pela produtividade, integração com .NET e suporte a migrations.

---

## SQL Server

Banco relacional adequado para consistência transacional.

---

## Trade-offs

### Vantagens

- Código organizado
- Fácil manutenção
- Baixo acoplamento
- Alta testabilidade

### Desvantagens

- Estrutura inicial maior
- Curva de aprendizado
- Mais arquivos