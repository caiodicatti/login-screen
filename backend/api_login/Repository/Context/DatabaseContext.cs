using api_login.Model.Tables;
using api_login.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_login.Repository.Context
{
    public class DatabaseContext: DbContext
    {

        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Usuario { get; set; }
        public DbSet<RecoverPassword> RecoverPassword { get; set; }
    }
}
