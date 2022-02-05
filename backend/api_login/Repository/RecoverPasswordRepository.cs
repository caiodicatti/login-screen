using api_login.Model.Tables;
using api_login.Repository.Context;
using api_login.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Repository
{
    public class RecoverPasswordRepository : IRecoverPasswordRepository
    {
        private readonly DatabaseContext context;

        public RecoverPasswordRepository(DatabaseContext _context)
        {
            context = _context;
        }

        public RecoverPasswordRepository()
        {
        }

        public void InsertData(RecoverPassword recoverPassword)
        {
            context.RecoverPassword.Add(recoverPassword);
            context.SaveChanges();            
        }

        public bool UpdateValidade(String email)
        {
            RecoverPassword recoverBD = context.RecoverPassword.AsNoTracking().Where(recover => recover.email == email).FirstOrDefault();

            if(recoverBD!= null && recoverBD.validado == "N")
            {
                recoverBD.validado = "S";
                recoverBD.dataCadastro = DateTime.Now;

                context.RecoverPassword.Update(recoverBD);
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
