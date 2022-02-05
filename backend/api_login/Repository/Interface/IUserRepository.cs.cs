using api_login.Model;
using api_login.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Repository.Interface
{
    public interface IUserRepository
    {
        public User Cadastro(User user);
        public User GetUserByEmail(String email);
        public bool AlterPassword(RecoverPasswordLink recoverPasswordLink);
    }
}
