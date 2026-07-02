# 📦 Order Management API

> API REST desenvolvida em **.NET 8** para gerenciamento de pedidos, clientes e produtos, aplicando conceitos de **DDD**, **SOLID**, **Clean Architecture**, **Repository Pattern** e **Entity Framework Core**.

---

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![C#](https://img.shields.io/badge/C%23-12-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-green)
![DDD](https://img.shields.io/badge/DDD-Architecture-success)
![SOLID](https://img.shields.io/badge/SOLID-Principles-blue)
![License](https://img.shields.io/badge/License-MIT-yellow)

## 📖 Sobre o Projeto

Este projeto foi desenvolvido como solução para um desafio técnico com o objetivo de construir uma API robusta, organizada e escalável para gerenciamento de pedidos.

A aplicação foi estruturada priorizando:

- Separação de responsabilidades
- Regras de negócio centralizadas no domínio
- Baixo acoplamento
- Alta coesão
- Facilidade para testes
- Facilidade de manutenção

Toda a lógica de negócio foi implementada seguindo boas práticas utilizadas em projetos corporativos.

---

# 🏛 Arquitetura

O projeto segue uma arquitetura inspirada em **Clean Architecture** juntamente com conceitos de **Domain Driven Design (DDD)**.

```
┌───────────────────────┐
│        API            │
│ Controllers           │
│ Swagger               │
│ Middlewares           │
└──────────┬────────────┘
           │
           ▼
┌───────────────────────┐
│     Application       │
│ Services              │
│ DTOs                  │
│ Interfaces            │
└──────────┬────────────┘
           │
           ▼
┌───────────────────────┐
│       Domain          │
│ Entities              │
│ Value Objects         │
│ Enums                 │
│ Exceptions            │
│ Repository Contracts  │
└──────────┬────────────┘
           │
           ▼
┌───────────────────────┐
│    Infrastructure     │
│ EF Core               │
│ SQL Server            │
│ Repositories          │
│ Persistence           │
└───────────────────────┘
```

---

# 📂 Estrutura da Solução

```
OrderManagement.sln

src/

├── OrderManagement.Api
│
├── OrderManagement.Application
│
├── OrderManagement.Domain
│
└── OrderManagement.Infrastructure
```

---

# 🚀 Tecnologias Utilizadas

## Plataforma

- .NET 8
- ASP.NET Core Web API

## Persistência

- Entity Framework Core
- SQL Server

## Documentação

- Swagger / OpenAPI

## Arquitetura

- Clean Architecture
- Domain Driven Design (DDD)
- SOLID
- Repository Pattern

## Boas práticas

- Value Objects
- DTOs
- Dependency Injection
- Middleware Global para Exceptions

---

# 📋 Funcionalidades

## Clientes

- Cadastro
- Consulta
- Atualização
- Remoção
- Ativação
- Desativação

Regras:

- CPF/CNPJ válido
- Email válido
- Documento único

---

## Produtos

- Cadastro
- Consulta
- Atualização
- Remoção

Regras:

- Controle de estoque
- Controle de preço
- Produto ativo/inativo

---

## Pedidos

- Criar pedido
- Consultar pedido
- Consultar todos
- Marcar como Pago
- Marcar como Enviado
- Cancelar pedido

Regras implementadas:

- Cliente deve estar ativo
- Produto deve possuir estoque
- Pedido deve possuir itens
- Controle automático do estoque
- Histórico de alterações de status
- Validação das transições de status

---

# 📈 Fluxo do Pedido

```
Criado
   │
   ▼
 Pago
   │
   ▼
Enviado
```

Também é permitido:

```
Criado
   │
   ▼
Cancelado
```

Qualquer outra transição gera uma **DomainException**.

---

# 📦 Modelo de Domínio

O domínio foi construído utilizando entidades ricas.

## Entidades

- Cliente
- Produto
- Pedido
- PedidoItem
- HistoricoStatusPedido

## Value Objects

- Email
- Documento

Toda validação permanece encapsulada nos próprios objetos.

---

# 💾 Estratégia de Persistência

Foi utilizada a abordagem **Code First** com Entity Framework Core.

Características:

- Migrations
- Fluent API
- Repository Pattern
- SQL Server

Os Value Objects foram persistidos utilizando **Owned Types**, mantendo o domínio rico sem criar tabelas desnecessárias.

---

# 📦 Controle de Estoque

Durante a criação do pedido:

- verifica disponibilidade
- debita estoque

Durante o cancelamento:

- repõe automaticamente o estoque

Toda a lógica permanece dentro do domínio.

---

# 💰 Valores Monetários

Todos os valores monetários utilizam:

```
decimal
```

Mapeados para:

```
decimal(18,2)
```

Essa estratégia evita problemas de precisão encontrados com `float` e `double`.

---

# 🌎 Datas e Fuso Horário

Todas as datas são armazenadas em:

```
DateTime.UtcNow
```

Utilizar UTC garante consistência entre diferentes fusos horários.

A conversão para horário local deve ocorrer apenas na camada de apresentação.

---

# 🧪 Estratégia de Testes

A arquitetura foi preparada para facilitar testes unitários.

As principais regras candidatas a testes são:

- Cliente
- Produto
- Pedido
- Value Objects
- Services

Exemplos:

- CPF inválido
- Email inválido
- Estoque insuficiente
- Pedido sem itens
- Mudança de status
- Cancelamento
- Reposição de estoque

---

# 📚 Documentação

A pasta **docs** contém documentação complementar:

```
docs/

01-domain-model.md
02-solid.md
03-architecture.md
04-business-rules.md
05-persistence.md
06-api.md
07-decisions.md
08-future-improvements.md
```

---

# ▶ Como Executar

## Pré-requisitos

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 ou superior

---

## Clone o projeto

```bash
git clone https://github.com/SEU-USUARIO/OrderManagement.Api.git
```

---

## Configure a Connection String

No arquivo:

```
appsettings.json
```

Configure:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=OrderManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## Execute as Migrations

```bash
Update-Database
```

ou

```bash
dotnet ef database update
```

---

## Execute o projeto

```bash
dotnet run
```

---

## Swagger

Após iniciar a aplicação:

```
https://localhost:7078/swagger
```

---

# 📌 Decisões Técnicas

As principais decisões adotadas foram:

- Clean Architecture
- DDD
- SOLID
- Repository Pattern
- Entity Framework Core
- SQL Server
- Value Objects
- Fluent API
- DTOs
- Dependency Injection

Essas escolhas tornam a aplicação mais organizada, desacoplada e preparada para evolução.

---

# ⚖ Trade-offs

## Vantagens

- Código desacoplado
- Fácil manutenção
- Fácil evolução
- Regras concentradas no domínio
- Alta legibilidade

## Desvantagens

- Maior quantidade de arquivos
- Curva de aprendizado inicial
- Estrutura mais robusta para projetos pequenos

---

# 🚀 Melhorias Futuras

Caso houvesse mais tempo, seriam implementados:

- Testes unitários (xUnit + FluentAssertions + Moq)
- JWT Authentication
- Authorization
- Refresh Token
- Redis
- RabbitMQ
- Docker
- GitHub Actions (CI/CD)
- Health Checks
- OpenTelemetry
- Serilog
- Paginação
- Filtros
- Versionamento da API
- Coleção Postman
- Docker Compose

---

# 👨‍💻 Autor

**Nicollas Guimarães**

Desenvolvedor FullStack .NET

LinkedIn:

> https://www.linkedin.com/in/nicollas-guimarães

---

# ⭐ Considerações Finais

Este projeto foi desenvolvido com foco em boas práticas de desenvolvimento de software, buscando representar uma arquitetura utilizada em aplicações corporativas.

Além da implementação dos requisitos propostos, foram adotados princípios de design que favorecem escalabilidade, manutenção e evolução do sistema, mantendo as regras de negócio centralizadas no domínio e minimizando o acoplamento entre as camadas.
