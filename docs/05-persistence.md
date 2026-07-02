# Persistência

## Banco de Dados

Foi utilizado:

- SQL Server
- Entity Framework Core

---

# Estratégia

Foi utilizada a estratégia Code First.

Todo o modelo do banco é gerado através das entidades e configurações do Entity Framework.

---

# Value Objects

Os Value Objects foram persistidos utilizando Owned Types.

Exemplos:

- Documento
- Email

Isso permite que o domínio permaneça rico sem criar tabelas adicionais.

---

# Repository Pattern

Toda comunicação com o banco ocorre através de Repositories.

Exemplo:

```
Controller

↓

Service

↓

Repository

↓

DbContext
```

Nenhuma camada superior conhece o Entity Framework.

---

# Controle de Estoque

Ao criar um pedido:

- verifica estoque
- debita quantidade

Ao cancelar:

- devolve estoque

Toda regra permanece centralizada no domínio.

---

# Valores Monetários

Foi utilizado:

decimal

Configuração:

```
decimal(18,2)
```

Evita problemas de precisão encontrados com float e double.

---

# Datas

Todas as datas são armazenadas em:

UTC

Utilizando:

```
DateTime.UtcNow
```

Isso evita inconsistências entre fusos horários.

A conversão para horário local deve ocorrer apenas na camada de apresentação.