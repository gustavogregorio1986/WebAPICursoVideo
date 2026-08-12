using Microsoft.EntityFrameworkCore;
using WebAPICursoVideo.Models;

namespace WebAPICursoVideo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
