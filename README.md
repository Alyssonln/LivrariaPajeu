# 📚 Livraria Pajeú API

API REST em **C# / ASP.NET Core** para gerenciar livros de uma livraria: cadastro, listagem com filtros, busca, edição e exclusão.

## 🚀 Tecnologias

C# · .NET · ASP.NET Core Web API · Swagger

## 🏗️ Estrutura

```
LivrariaPajeu.API            → Controllers, Services, Validators, Filters
LivrariaPajeu.Communication  → Requests e Responses
LivrariaPajeu.Exception      → Exceções personalizadas
```

- Erros tratados por um **filtro global**, que devolve o status code certo num formato padrão
- **Injeção de dependência**, com os dados armazenados em memória

## 📌 Endpoints

| Método | Rota | Descrição |
|---|---|---|
| `POST` | `/api/books` | Cria um livro |
| `GET` | `/api/books` | Lista livros (filtros: `title`, `author`, `genre`) |
| `GET` | `/api/books/{id}` | Busca por ID |
| `PUT` | `/api/books/{id}` | Atualiza um livro |
| `DELETE` | `/api/books/{id}` | Exclui um livro |

## 📋 Regras

- `title` e `author`: entre 2 e 120 caracteres, sem duplicar a combinação
- `genre`: deve estar na lista de gêneros válidos
- `price` e `stock`: maiores ou iguais a 0
- `CreatedAt` preenchido na criação e `UpdatedAt` em cada alteração

## ▶️ Como executar

```bash
git clone https://github.com/SEU-USUARIO/livraria-pajeu-api.git
cd livraria-pajeu-api
dotnet run --project LivrariaPajeu.API
```

Acesse `/swagger` no endereço exibido no terminal.

## 👨‍💻 Autor

**Alysson Nunes**
[LinkedIn](https://www.linkedin.com/in/SEU-LINKEDIN) · [GitHub](https://github.com/SEU-USUARIO)
