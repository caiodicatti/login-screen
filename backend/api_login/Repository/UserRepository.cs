using api_login.Model;
using api_login.Repository.Context;
using api_login.Repository.Interface;
using api_login.Repository.Model;
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

        public User Login(Authentication authentication)
        {
            User user = context.Usuario.Where(u => u.email == authentication.Email).FirstOrDefault();
            return user;
        }
    }

}
