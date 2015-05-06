# LEIA-ME #

Esta é uma aplicação web para demonstração do uso de algumas funcionalidade da API da SpediA.

A aplicação está desenvolvida em .NET (C#) utilizando WebForms para facilitar o aprendizado e entendimento exclusivo das APIs da SpediA.

### Requisitos para executar a aplicação ###

- SQLServer 2012
- Visual Studio 2013
- Credenciais de acesso da API SpediA

### Fazendo setup do projeto ###

Configuração da base de dados

- Executar o script Database/CRIACAO_BANCO_E_USUARIO.sql na base de dados SQLServer que será utilizada
- Habilitar o usuário da aplicação no SQLServer via interface ou com o comando abaixo:

ALTER LOGIN [spediauser] ENABLE

- Configurar o acesso do usuário spediauser na base spedia via interface ou com os comandos abaixo:

~~~~
USE [spedia]
GO
CREATE USER [spediauser] FOR LOGIN [spediauser]
GO
~~~~

- Alterar a senha do usuário spediauser via interface ou com o comando abaixo:

~~~~
ALTER LOGIN [spediauser] WITH PASSWORD=N'NOVA_SENHA'
GO
~~~~

- Alterar o owner da base spedia para o usuário spediauser pela interface ou com o comando abaixo:

~~~~
USE [spedia]
GO
exec sp_addrolemember db_owner,spediauser;
GO
~~~~

Visual Studio 2013

- Abrir a solução
- Configurar o parâmetro connection.connection_string no arquivo SpediaLibrary\hibernate.cfg.xml para apontar para a base de dados configurada previamente. Se o SQLServer estiver em execução na mesma máquina onde será executada a aplicação não será preciso configurar.
- Configurar também no parêmetro connection.connection_string a senha do usuário de banco definida previamente.
- Configurar as mesmas informações de banco no arquivo SpediaWeb\Web.config sessão <connectionStrings><add name="spedia...
- Configurar as credenciais de acesso а plataforma SpediA nas colunas usuario_spedia e senha_spedia da tabela usuario. Se você não possui essas credenciais, solicite pelo nosso site http://spedia.com.br/
- Alterar as propriedades abaixo no arquivo SpediaWeb\Web.config
- TempDirectory = Diretório onde serão gravados os arquivos temporários (O diretório deve ser previamente criado)
- Template = Diretório onde serão gravados os templates de e-mail. Deve ser configurado para o caminho absoluto do diretório SpediaWeb\Templates\ do projeto
- Executar o projeto, acessar o endereço http://localhost:51776/spedia/Pages/Logon e realizar o login com o usuário padrão

### Credenciais padrão ###

Login                 | Senha
--------------------- | ------
usuario@spedia.com.br | 0