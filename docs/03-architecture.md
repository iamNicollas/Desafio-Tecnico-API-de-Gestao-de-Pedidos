# Arquitetura da Solução

## Visão Geral

O projeto foi desenvolvido seguindo princípios de **Clean Architecture**, **DDD (Domain-Driven Design)** e **SOLID**, separando claramente as responsabilidades entre as camadas.

A estrutura busca manter o domínio independente de tecnologias externas, facilitando manutenção, testes e evolução do sistema.

---

# Estrutura da Solução

```
OrderManagement.sln

src/

OrderManagement.Api
OrderManagement.Application
OrderManagement.Domain
OrderManagement.Infrastructure
```

---

# Camadas

## API

Responsável por:

- Controllers
- Swagger
- Middlewares
- Configuração da aplicação

Não contém regras de negócio.

---

## Application

Responsável por:

- Casos de uso
- DTOs
- Interfaces de serviços
- Coordenação das regras do domínio

Exemplo:

```
Criar Pedido

↓

Buscar Cliente

↓

Buscar Produto

↓

Executar regras do domínio

↓

Persistir
```

---

## Domain

É o coração da aplicação.

Contém:

- Entidades
- Value Objects
- Enums
- Exceptions
- Interfaces de Repositórios

Nenhuma dependência de Entity Framework.

---

## Infrastructure

Responsável pela implementação técnica.

Contém:

- Entity Framework Core
- DbContext
- Repositories
- Configurações das entidades

---

# Fluxo da Aplicação

```
Controller

↓

Service

↓

Entidades do Domínio

↓

Repository

↓

Entity Framework

↓

SQL Server
```

---

# Benefícios

- Baixo acoplamento
- Alta coesão
- Fácil manutenção
- Facilidade para testes
- Separação clara de responsabilidades