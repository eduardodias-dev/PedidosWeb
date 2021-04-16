

<h1>Pedidos Web - API</h1>


<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Sumário</h2></summary>
  <ol>
    <li><a href="#sobre-o-projeto">Sobre o Projeto</a></li>
    <li><a href="#desenvolvido-com">Desenvolvido com</a></li>
     <li><a href="#pré-requisitos">Pré-requisitos</a></li>
    <li><a href="#instalação">Instalação</a></li>
    <li><a href="#execute-o-projeto">Execute o projeto</a></li>
    <li><a href="#contato">Contato</a></li>
  </ol>
</details>



<!-- SOBRE O PROJETO -->
## Sobre o Projeto

Esse projeto contém a API de um pequeno CRUD de pedidos, incluindo cadastro de pedidos, produtos, categorias e clientes.


### Desenvolvido com

* .Net Core 5.0 (.NET 5)
* Entity Framework Core 5.0.5
* Entity Framework Core Identity


### Pré-requisitos

Esse projeto foi desenvolvido com Visual Studio 2019, então é recomendado utilizá-lo para compilar o projeto.

* .Net 5
  ```
  https://dotnet.microsoft.com/download/dotnet/5.0
  ```
* Visual Studio 2019 (Recomendado: Community)
```
  https://visualstudio.microsoft.com/pt-br/downloads/
```
* SQL Server 2017 Express ou superior
```
  https://www.microsoft.com/pt-br/download/details.aspx?id=55994
```
* PostMan RestClient (Recomendado para consumir a API)
```
https://www.postman.com/product/rest-client/
```

Obs.: Para utilizar o projeto no Visual Studio Code, alterar as portas da api para 5001 ou a porta de sua preferência.

### Instalação

1. Clone o repositório
   ```sh
   git clone https://github.com/eduardodias-dev/PedidosWeb.git
   ```
2. Abra o projeto no Visual Studio
   
3. Altere a string de conexão com o banco de dados, na seção "ConnectionStrings" do arquivo appsetings.json que está na raiz do projeto, apontando o servidor do sql server que deseja instalar o banco de dados.
4. Execute o comando para criação do banco de dados a partir das migrações:
  * No Packet Manager do Nuget:
    ```
      Update-Database
    ```
  * No .NET Cli (Necessária a ferramenta dotnet-ef -> https://docs.microsoft.com/pt-br/ef/core/cli/dotnet):
    ```
      dotnet-ef database update
    ```
    
### Execute o projeto

1.Após instalado, execute o projeto:
* No Visual Studio:
    ```
      Depurar -> Executar Depuração
    ```
    
* No prompt de comando
  ```sh
    dotnet run
  ```
2.Abra o Projeto no Postman e import as configurações do arquivo "PedidosWeb.postman_collection.json"
3.Execute uma das requisições salvas, lembrando que é necessário fazer o registro de usuário no endpoint api/authorize/register
 

## Contato

Eduardo Dias - [linkedin](https://www.linkedin.com/in/eduardo-jos%C3%A9-de-oliveira-dias-5963ba57/) - eduardo.dias092@gmail.com

Project Link: [https://github.com/eduardodias-dev/PedidosWeb](https://github.com/eduardodias-dev/PedidosWeb)

