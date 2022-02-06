using api_login.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Repository.Interface
{
    public interface IRecoverPasswordRepository
    {
        public void InsertData(RecoverPassword recoverPassword);
        public string UpdateValidade(String email);
    }
}
