# Lisport.API
API em **.NET + ASP.NET Core** com **EF Core (SQLite)** para o projeto Lisport.

O Lisport.API é uma API backend em ASP.NET Core que centraliza as regras e os dados do sistema Lisport. Ela expõe endpoints HTTP para autenticação com JWT e para operações do sistema (gestão de usuários em geral), persistindo as informações em um banco SQLite via Entity Framework Core, para dar suporte a um frontend/aplicação cliente.
## Requisitos
- **.NET SDK** (compatível com o projeto)
## Como rodar
```bash
dotnet restore
dotnet run
Banco: SQLite em arquivo lisport.db (local).
Swagger/OpenAPI: habilitado em ambiente Development.
Configuração
As principais configurações ficam em appsettings.json:

ConnectionStrings:DefaultConnection: conexão do SQLite
Jwt:SecretKey, Jwt:Issuer, Jwt:Audience, Jwt:ExpirationMinutes: autenticação JWT
Rotas (principais)
Auth: POST /auth/login, POST /auth/register, POST /auth/bootstrap, POST /auth/definir-senha-inicial
Users: GET /users/{id}, PATCH /users/{id}, DELETE /users/{id} (endpoints protegidos)
Observação
O arquivo lisport.db é local e não deve ser versionado no git.



dotnet run
