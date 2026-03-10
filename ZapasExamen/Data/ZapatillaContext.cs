using Microsoft.EntityFrameworkCore;

namespace ZapasExamen.Data
{
    public class ZapatillaContext : DbContext
    {
        public ZapatillaContext(DbContextOptions<ZapatillaContext> options) : base(options)
        { }
        
        public DbSet<Zapatilla> zapa { get; set; }
        public DbSet<ImagenZapatilla> ImagenesZapatillas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar que las claves primarias NO son IDENTITY
            // porque la base de datos no tiene auto-incremento
            modelBuilder.Entity<Zapatilla>()
                .Property(z => z.IdProducto)
                .ValueGeneratedNever();

            modelBuilder.Entity<ImagenZapatilla>()
                .Property(i => i.IdImagen)
                .ValueGeneratedNever();
        }
    }
}



