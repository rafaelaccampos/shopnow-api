<p align="center">
  <h1>ShopNow Api</h1>
</p>

## 💻 Projeto

  ShopNow é uma API de sistema de vendas online baseado no curso Clean Code e Clean Architecture do Rodrigo Branas. 
  Neste projeto é possível realizar o pedido com múltiplos itens, cancelamento de pedido, cálculo de frete e interação com o estoque.  

## 🧪 Tecnologias

Esse projeto foi desenvolvido com as seguintes tecnologias:

- [.NET 6 WebApi](https://learn.microsoft.com/pt-br/dotnet/core/whats-new/dotnet-6)
- [Entity Framework](https://learn.microsoft.com/pt-br/ef/)
- [NUnit](https://nunit.org/)
- [Fluent Migrator](https://fluentmigrator.github.io/)
- [Fluent Assertions](https://fluentassertions.com/)
- [NSubstitute](https://nsubstitute.github.io/)
- [AutoBogus](https://github.com/nickdodd79/AutoBogus)
- [Respawn](https://github.com/jbogard/Respawn)

## Como usar?

  Você precisará da última versão do Visual Studio 2022 e .NET Core SDK 6.
  SDKs e ferramentas podem ser baixadas em .Net Core: https://dot.net/core.
  Você também pode executar no Visual Studio Code (Windows, Linux ou MacOS).

## 🚀 Como executar?

Clone o projeto e acesse a pasta do mesmo.

```bash
$ git clone https://github.com/rafaelaccampos/shopnow-api.git
$ cd ShopNow
```
Para iniciar o projeto, siga os passos abaixo:
```bash
$ cd src/ShopNow
# Instalar as dependências
$ dotnet restore
# Iniciar o projeto
$ dotnet run 
```
O app estará disponível no seu browser pelo endereço https://localhost:5001.

Para iniciar o testes, siga os passos abaixo:
```bash
# Para rodar os testes
$ dotnet test
```

Feito com 💜 by Rafaela Cassiano Campos 👋🏻