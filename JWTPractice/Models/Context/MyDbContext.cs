using JWTPractice.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace JWTPractice.Models.Context
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=BIYIKLI\\BIYIKLI;Database=JwtLoginPractice;Trusted_Connection=true;");
        }




        public DbSet<Role> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
