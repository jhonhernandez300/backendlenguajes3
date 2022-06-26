using Microsoft.EntityFrameworkCore;


namespace LenguajesIII.Models
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        public Microsoft.EntityFrameworkCore.DbSet<Pedidos> Pedido { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Detalles> Detalle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Pedidos>().HasKey(m => m.IdPedido);

            modelBuilder.Entity<Detalles>().HasKey(m => m.IdDetalle);
            
            modelBuilder.Entity<Detalles>()
            .HasOne(p => p.Pedidos)
            .WithMany(b => b.Detalle)
            .HasForeignKey(t => t.IdPedido);

            base.OnModelCreating(modelBuilder);
        }
    }
}
