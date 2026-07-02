# API REST

## Endpoints

### Clientes

| Método | Endpoint |
|---------|----------|
| GET | /clientes |
| GET | /clientes/{id} |
| POST | /clientes |
| PUT | /clientes/{id} |
| DELETE | /clientes/{id} |

---

### Produtos

| Método | Endpoint |
|---------|----------|
| GET | /produtos |
| GET | /produtos/{id} |
| POST | /produtos |
| PUT | /produtos/{id} |
| DELETE | /produtos/{id} |

---

### Pedidos

| Método | Endpoint |
|---------|----------|
| GET | /pedidos |
| GET | /pedidos/{id} |
| POST | /pedidos |
| PATCH | /pedidos/{id}/pagar |
| PATCH | /pedidos/{id}/enviar |
| PATCH | /pedidos/{id}/cancelar |

---

# Documentação

Toda a API encontra-se documentada utilizando Swagger.

Após executar a aplicação:

```
https://localhost:7078/swagger
```