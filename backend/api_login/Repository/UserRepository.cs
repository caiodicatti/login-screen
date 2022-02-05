using api_login.Model;
using api_login.Repository.Context;
using api_login.Repository.Interface;
using api_login.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext context;

        public UserRepository(DatabaseContext _context)
        {
            context = _context;
        }

        public UserRepository()
        {
        }

        public User Cadastro(User user)
        {
            context.Usuario.Add(user);
            context.SaveChanges();
            return user;
        }

        public User GetUserByEmail(String email)
        {
            User user = context.Usuario.Where(u => u.email == email).FirstOrDefault();
            return user;
        }

        public bool AlterPassword(RecoverPasswordLink recoverPasswordLink)
        {
            User userBD = context.Usuario.AsNoTracking().Where(user => user.email == recoverPasswordLink.Email).FirstOrDefault();

            if(userBD != null)
            {
                userBD.senha = recoverPasswordLink.Senha;

                context.Usuario.Update(userBD);
                context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
