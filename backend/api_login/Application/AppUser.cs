﻿using api_login.Application.Interface;
using api_login.Model;
using api_login.Repository;
using api_login.Repository.Interface;
using api_login.Repository.Model;
using api_login.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace api_login.Application
{
    public class AppUser : IAppUser
    {
        //private readonly UserRepository repository;
        public readonly IUserRepository repository;

        public AppUser(IUserRepository _repository)
        {
            repository = _repository;
        }

        public Response Cadastrar(User user)
        {
            Functions f = new Functions();

            EntityVerify verifica = f.VerifyUser(user);

            if (verifica.Verifica)
            {
                Encrypt encript = new Encrypt(SHA512.Create());
                user.senha = encript.CriptografarSenha(user.senha);

                var retorno = f.HideParamsUser(repository.Cadastro(user));

                ServiceMail.Send(retorno.nome, retorno.email);

                Response response = new Response();
                response.success = true;
                response.statusCode = 200;
                response.result = retorno;
                response.message = "Usário cadastrado com sucesso";

                return response;

            }
            else
            {
                Response error = new Response();
                error.success = false;
                error.statusCode = 422;
                error.result = "";
                error.message = verifica.Mensagem;

                return error;
            }
        }

        public Response Login(Authentication authentication)
        {
            Functions f = new Functions();

            Encrypt encript = new Encrypt(SHA512.Create());

            User user = repository.Login(authentication);

            bool verifica = false;

            if (user != null)
            {
                verifica = encript.VerificarSenha(authentication.Senha, user.senha);

                if (verifica)
                {
                    Response response = new Response();
                    response.success = true;
                    response.statusCode = 200;
                    response.result = f.HideParamsUser(user); ;
                    response.message = "Usário autenticado";

                    return response;
                }
                else
                {
                    Response error = new Response();
                    error.success = false;
                    error.statusCode = 422;
                    error.result = "";
                    error.message = "Senha ou e-mail incorreto";

                    return error;
                }
            }
            else
            {
                Response error = new Response();
                error.success = false;
                error.statusCode = 422;
                error.result = "";
                error.message = "Usuário não encontrado";

                return error;
            }
        }
    }
}
