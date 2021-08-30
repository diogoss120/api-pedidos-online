# api-pedidos-online
Api simples feita com ASP.NET Core Web Api para gerenciamento de pedidos online.
<p>  Este projeto gerencia Clientes, Pedidos, Produtos e o relacionamento entre eles, 
o acesso ao banco de dados é feito com o Entity Framework, e sobre ele implementei uma camada de persistência que aplica os padrões Repository e UnitOfWork.<br> 
Também criei uma camada de Serviço que acessa as camadas de Persistência e Dominio e expõe DTOs para a camada de API <br>
A camada de API acessa somente a camada de Serviços em seus controllers, ela possui a referênia das outras camadas somente para registras na classe 
Startup a classe de contexto e os serviços criados para a injeção de dependência.
</p>


## Para executar esse projeto:
- realize o clone do repositório com o git bash ou faça o download
- com o git bash ou terminal do sistema operacional, navegue até o diretório do projeto e execute:
- dotnet restore  // para restaurar as dependências
- altere a string de conexão no arquivo appsettings.json para a string de conexão do seu SqlServer
- rode a migration com o comando: 'dotnet ef database update' pelo terminal/git bash, ou 'Update-Database' no Visual Studio
- também inclui a estrutura do banco no projeto 'Sales.Persistence', caso opte por importar o banco no seu SGBD
- dotnet watch run   // para rodar 

## 🛠 Tecnologias
As seguintes ferramentas foram usadas na construção do projeto:

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [ASP.NET core Web Api](https://docs.microsoft.com/pt-br/aspnet/core/web-api/?view=aspnetcore-5.0)
- [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/)
- [SQLServer](https://www.microsoft.com/pt-br/sql-server/sql-server-2019)
- [.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
