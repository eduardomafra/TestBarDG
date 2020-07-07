# Projeto

Projeto desenvolvido para avaliação técnica de Dev .NET Jr - ClearSale  
  
As ferramentas utilizadas foram .NET Core 3.1 para o desenvolvimento da API, Angular 10 para as páginas web e SQLite para armazenar os dados

## Requisitos

[.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)  
  
[NodeJS 12 LTS](https://nodejs.org/en/download/)  

## Instruções API

Navegar até a pasta `TestBarDg` através do terminal e executar o comando `dotnet build` para compilar o projeto e todas suas dependências  
  
Em seguida navegar até `TestBarDg/TestBarDg` e executar o comando `dotnet run` para iniciar a API

## Instruções Web

Abrir o terminal e digitar `npm install -g @angular/cli` para baixar a última versão do Angular e reiniciar o terminal após o término  
  
Navegar até a pasta `TestBarDg-Front` através do terminal e executar o comando `npm install` para instalar todas as dependências do projeto   
  
Executar o comando `ng serve` para iniciar o servidor web, por padrão ele estará rodando em http://localhost:4200/

## Importante

No arquivo `TestBarDg-Front/src/environments/environments.ts` é possível alterar a url da API consumida pelo front-end, por padrão está definido http://localhost:5000/ mas em alguns casos a porta pode ser diferente

## Documentação da API

A documentação da API foi gerada pela biblioteca Swagger e pode ser encontrada em http://localhost:5000/swagger/index.html