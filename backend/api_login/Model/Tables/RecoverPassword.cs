using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Model.Tables
{
    [Table("tbl_recuperacao_senha")]
    public class RecoverPassword
    {
        [Key]
        public int idRecuperacaoSenha { get; set; }
        public string email { get; set; }
        public string codigo { get; set; }
        public string validado { get; set; }
        public DateTime dataCadastro { get; set; }
    }
}
