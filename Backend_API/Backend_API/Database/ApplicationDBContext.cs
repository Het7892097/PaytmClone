using Backend_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_API.Database
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions) {}

        public DbSet<User> Users { get; set; }  
        public DbSet<Account> Accounts { get; set; }
      
    }
}
