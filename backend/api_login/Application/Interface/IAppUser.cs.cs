using api_login.Model;
using api_login.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Application.Interface
{
    public interface IAppUser
    {
        public Response Cadastrar(User user);
    }
}
