using Microsoft.EntityFrameworkCore;

namespace PracticaProgramada_Grupo3.Models
{
    public class Tarea02Context : DbContext
    {
        public Tarea02Context(DbContextOptions<Tarea02Context> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=WINDEV2401EVAL\SQLEXPRESS;Database=tarea02;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        public DbSet<User> Usuarios { get; set; }

        public DbSet<Game> Games { get; set; }

    }
}
