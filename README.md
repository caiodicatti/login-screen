# login-screen

## Descrição do Projeto

<h1 align="center">
    <a href="https://www.mysql.com/">🔗 MySQL</a>
</h1>
<p align="center">O banco de dados utilizado no projeto.</p>


<h1 align="center">
    <a href="https://dotnet.microsoft.com/en-us/">🔗 .Net Core 5</a>
</h1>
<p align="center">No backend, a API criada foi desenvolvida utilizando essa tecnologia.</p>

<h1 align="center">
    <a href="https://angular.io/">🔗 Angular 13</a>
</h1>
<p align="center">Framework utilizado no projeto para construir interfaces do usuário com componentes reutilizáveis.</p>

## Como utilizar

- O projeto está dividido entre backend, frontend e scrips sql's para a criação do banco.

- Primeiramente rode os scripts sql em seu BD.

- Após, entre na pasta backend e abra o arquivo appsettings.json

- Neste arquivo, altere a string "DefaultConnection" com suas credenciais, além disso, no nó "EmailConfig", coloque o SMTP, e-mail e senha conforme desejado, essa configuração é de extrema importância, pois sem ela, os e-mails não serão disparados após a conclusão do cadastro e na página de "Esqueci a senha".

- Assim que terminar a configuração do arquivo appsettings, rodar a API e após isso, iniciar a loja através do comando "ng s"

### Features

- [x] Cadastro de usuário
- [x] Autenticação de usuário
- [x] Recuperação de Senha
