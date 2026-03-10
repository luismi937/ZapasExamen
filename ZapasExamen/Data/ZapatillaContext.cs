using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ZapasExamen.Data
{
    public class ZapatillaContext : Controller
    {
        public ZapatillaContext(DbContextOptions<ZapatillaContext> options) : base(options)
        { }
        public DbSet<Zapatilla> zapa { get; set; }
        public DbSet<ImagenZapatilla> ImagenesZapatillas { get; set; }
    }

}


