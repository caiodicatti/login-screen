using api_login.Repository;
using api_login.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Application
{
    public class AppUser
    {
        //private readonly UserRepository repository;
        public readonly UserRepository repository;

        public AppUser(UserRepository _repository)
        {
            repository = _repository;
        }

        public User Cadastrar(User user)
        {
            return repository.Cadastro(user);
        }

    }
}
