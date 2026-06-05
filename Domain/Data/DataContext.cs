using Domain.Data.Entities;
using Domain.Data.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class DataContext : IdentityDbContext<User, Role, string>
    {
        #region Overrids
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ReviewMap(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        public DbSet<Product> Products { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<TypeIdentification> TypeIdentifications { get; set; }

        public DbSet<IMPUESTO> IMPUESTOS { get; set; }

        public DbSet<IMPUESTO_VALOR> IMPUESTO_VALOR { get; set; }

        public DbSet<PRODUCTO_IMPUESTO> PRODUCTO_IMPUESTO { get; set; }

        public DbSet<MyCommerce> MyCommerce { get; set; }


        public DbSet<SignatureOption> SignatureOptions { get; set; }

        public DbSet<AutoridadesCertificante> AutoridadesCertificantes { get; set; }

        public DbSet<TypeDocument> TypeDocuments { get; set; }

        public DbSet<FORMAS_PAGO> FORMAS_PAGOS { get; set; }

        public DbSet<MySetting> MySettings { get; set; }

        public DbSet<Bodega> Bodegas { get; set; }


    }
}
