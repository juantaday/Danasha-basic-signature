using Domain.Data.Entities;
using Domain.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Data
{
    public  class ReviewMap
    {
        public ReviewMap(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FORMAS_PAGO>(pr =>
            {
                pr.HasIndex(x => x.CODIGO_FORMA_PAGO).IsUnique().HasName("UQ_CODIGO_FORMA_PAGO");
            });

            modelBuilder.Entity<IMPUESTO>(pr => 
            {
                pr.HasIndex(x => x.CODIGO).IsUnique().HasName("UQ_CODIGO_IMPUESTO");
            });

            modelBuilder.Entity<IMPUESTO_VALOR>(pr =>
            {
                pr.HasOne(x => x.IMPUESTO).WithMany(x => x.IMPUESTO_VALORES)
              .HasForeignKey(x => x.CODIGO_IMPUESTO)
              .OnDelete(DeleteBehavior.Restrict);

            });


            modelBuilder.Entity<PRODUCTO_IMPUESTO>(pr =>
            {
                pr.HasOne(x => x.Product).WithMany(x => x.PRODUCTO_IMPUESTO)
              .HasForeignKey(x => x.ProductId)
              .OnDelete(DeleteBehavior.Cascade);

                pr.HasOne(x => x.IMPUESTO_VALOR).WithMany(x => x.PRODUCTO_IMPUESTOS)
                  .HasForeignKey(x => x.CODIGO_IMPUESTO)
                  .OnDelete(DeleteBehavior.Restrict);


            });

            modelBuilder.Entity<INFO_ADICIONAL >(pr =>
            {
                pr.HasOne(x => x.Product).WithMany(x => x.INFO_ADICIONALS)
              .HasForeignKey(x => x.ProductId)
              .OnDelete(DeleteBehavior.Cascade);

            });
       
            modelBuilder.Entity<TypeIdentification>(pr =>
            {
                pr.HasIndex(x => x.Codigo).IsUnique().HasName("UQ_Codigo_TypeIdentification");
                pr.HasIndex(x => x.Descrip).IsUnique().HasName("UQ_Descr_TypeIdentification");
                pr.HasIndex(x => x.Codigo_SRI).IsUnique().HasName("UQ_Codigo_Codigo_SRI");
            });

            modelBuilder.Entity<Cliente>(pr =>
            {
                pr.HasIndex(x => x.Num_Identity).IsUnique().HasName ("UQ_Num_Identity_Client");

                pr.HasOne(x => x.TypeIdentification)
                .WithMany(x => x.Clientes)
                .HasForeignKey(x => x.TypyIdentificationId)
                .OnDelete(DeleteBehavior.Restrict);

            });


            #region ECommerce
            modelBuilder.Entity<MyCommerce>(pr =>
            {
                pr.HasIndex(x => x.Ruc).IsUnique().HasName("UQ_MyCommerce_Ruc");

                pr.Property(x => x.DateRegister).HasDefaultValueSql("GetDate()");

            });

            modelBuilder.Entity<SignatureOption>(pr =>
            {
                pr.HasOne(x => x.MyCommerce).WithMany(x => x.SignatureOptions)
                .HasForeignKey(x => x.MyCommerceId).HasConstraintName("FK_SignatureOption_MyCommerce")
                .OnDelete(DeleteBehavior.Cascade);

            });


            modelBuilder.Entity<MySetting>(pr =>
            {
                pr.HasOne(x => x.MyCommerce)
                .WithOne(x => x.MySetting)
                .HasForeignKey<MySetting>(b => b.MyCommerceId)
                .HasConstraintName("FK_MySetting_MyCommerce_Id")
                .OnDelete(DeleteBehavior.Cascade);
            });


            #endregion

            #region System

            modelBuilder.Entity<TypeDocument>(pr =>
            {
                pr.HasIndex(x => x.NameDocument).IsUnique().HasName("UQ_TypeDocument_NameDocument");

            });

            #endregion
        }

    }
   

}
