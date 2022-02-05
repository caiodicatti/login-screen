CREATE DATABASE db_userlogin;

USE db_userlogin;

-- Tabela de usuário
CREATE TABLE tbl_usuario(
idUsuario integer auto_increment not null,
nome varchar(300),
email varchar(300),
senha varchar(500),
dataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
PRIMARY KEY(idUsuario));

-- Tabela de Recuperação Senha
CREATE TABLE tbl_recuperacao_senha(
idRecuperacaoSenha integer auto_increment not null,
email varchar(300),
codigo varchar(500),
validado varchar(1),
dataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
PRIMARY KEY(idRecuperacaoSenha));