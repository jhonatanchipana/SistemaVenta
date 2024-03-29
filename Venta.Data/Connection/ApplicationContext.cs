﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Identity.Client;
using SistemaVenta.Entities;
using Venta.Entities;

namespace Venta.Data.Connection
{
    /// <summary>
    /// Contexto asociado a la base de datos (utilizando EF Core)
    /// </summary>
    public class ApplicationContext : DbContext
    {
        //add-Migration -Context Venta.Data.Connection.ApplicationContext -name 001
        //update-database

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="options">Opciones para la configuración del contexto</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        /// <summary>
        /// BdSet asociado a la compra de material
        /// </summary>
        public DbSet<Purchase> Purchase { get; set; }

        /// <summary>
        /// BdSet asociado a compra de material detalle
        /// </summary>
        public DbSet<PurchaseMaterial> PurchaseMaterial { get; set; }

        /// <summary>
        /// BdSet asociado a la campaña
        /// </summary>
        public DbSet<ReportInOut> ReportInOut { get; set; }

        /// <summary>
        /// BdSet asociado a la ropa
        /// </summary>
        public DbSet<Clothing> Clothing { get; set; }

        /// <summary>
        /// BdSet asociado al material de la ropa
        /// </summary>
        public DbSet<ClothingMaterial> ClothingMaterial { get; set; }

        /// <summary>
        /// BdSet asociado a la categoria de ropa
        /// </summary>
        public DbSet<ClothingCategory> ClothingCategory { get; set; }

        /// <summary>
        /// BdSet asociado a la fabricacion de ropa
        /// </summary>
        public DbSet<Manufacturing> Manufacturing { get; set; }

        /// <summary>
        /// BdSet asociado a la prenda de fabricacion
        /// </summary>
        public DbSet<ManufacturingClothingSize> ManufacturingClothingSize { get; set; }

        /// <summary>
        /// BdSet asociado al modelo de la ropa
        /// </summary>
        //public DbSet<ClothingModel> ClothingModel { get; set; }

        /// <summary>
        /// BdSet asociado al material
        /// </summary>
        public DbSet<Material> Material { get; set; }

        /// <summary>
        /// BdSet asociado a la venta de ropa
        /// </summary>
        public DbSet<Sales> Sales { get; set; }

        /// <summary>
        /// BdSet asociado la venta de ropa detalle
        /// </summary>
        public DbSet<SalesClothingSize> SalesClothingSize { get; set; }

        /// <summary>
        /// BdSet asociado al usuario
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// BdSet asociado a la prenda talla
        /// </summary>
        public DbSet<ClothingSize> ClothingSize { get; set; }

        /// <summary>
        /// BdSet asociado a la talla
        /// </summary>
        public DbSet<Size> Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuraciones Generales
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                //configuraciones asociadas a la baseEntity
                if (typeof(BaseEntity).IsAssignableFrom(item.ClrType))
                {
                    modelBuilder.Entity(item.ClrType).Property<string>(nameof(BaseEntity.CreateBy)).IsRequired().HasMaxLength(80);
                    modelBuilder.Entity(item.ClrType).Property<DateTime>(nameof(BaseEntity.CreationDate)).IsRequired();
                    modelBuilder.Entity(item.ClrType).Property<string>(nameof(BaseEntity.ModifiedBy)).HasMaxLength(80);
                }
            }
            #endregion

            #region Configuración para la entidad Purchase
            modelBuilder.Entity<Purchase>()
                .Property(a => a.BuyDate)
                .IsRequired()
                .HasColumnType("date");

            modelBuilder.Entity<Purchase>()
                .Property(a => a.NameBuyer)
                .HasMaxLength(120);

            modelBuilder.Entity<Purchase>()
               .Property(a => a.CostTotal)
               .HasPrecision(16, 2);
            #endregion

            #region Configuración para la entidad PurchaseMaterial
            modelBuilder.Entity<PurchaseMaterial>()
                .Property(a => a.PriceUnit)
                .IsRequired()
                .HasPrecision(16, 2);
            #endregion

            #region Configuración para la entidad Activity
            modelBuilder.Entity<ReportInOut>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<ReportInOut>()
                .Property(a => a.InitialDate)
                .IsRequired()
                .HasColumnType("date");

            modelBuilder.Entity<ReportInOut>()
               .Property(a => a.EndDate)
               .HasColumnType("date");

            modelBuilder.Entity<ReportInOut>()
               .Property(a => a.StatusActivityType)
               .IsRequired();
            #endregion

            #region Configuración para la entidad Clothing
            modelBuilder.Entity<Clothing>()
               .Property(a => a.Name)
               .IsRequired()
               .HasMaxLength(250);

            modelBuilder.Entity<Clothing>()
               .Property(a => a.Description)
               .HasMaxLength(500);

            modelBuilder.Entity<Clothing>()
               .Property(a => a.PriceSuggested)
               .HasPrecision(8, 2);

            modelBuilder.Entity<Clothing>()
               .Property(a => a.InvestmentUnit)
               .HasPrecision(8, 2);
            #endregion

            #region Configuración para la entidad ClothingMaterial

            #endregion

            #region Configuración para la entidad ClothingSize

            #endregion

            #region Configuración para la entidad ClothingCategory
            modelBuilder.Entity<ClothingCategory>()
              .Property(a => a.Name)
              .IsRequired()
              .HasMaxLength(120);

            modelBuilder.Entity<ClothingCategory>()
             .Property(a => a.Description)
             .HasMaxLength(250);
            #endregion

            #region Configuración para la entidad Manufacturing
            modelBuilder.Entity<Manufacturing>()
              .Property(a => a.ManufacturingDate)
              .IsRequired();
            #endregion

            #region Configuración para la entidad ManufacturingClothing

            #endregion

            #region Configuración para la entidad ManufacturingClothingSize
            
            #endregion

            #region Configuración para la entidad ClothingModel
            //modelBuilder.Entity<ClothingModel>()
            //  .Property(a => a.Name)
            //  .IsRequired()
            //  .HasMaxLength(120);

            //modelBuilder.Entity<ClothingModel>()
            //  .Property(a => a.Description)
            //  .HasMaxLength(250);
            #endregion

            #region Configuración para la entidad Material
            modelBuilder.Entity<Material>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<Material>()
                .Property(a => a.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Material>()
                .Property(a => a.Cost)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Material>()
                .Property(a => a.UnitMeasurement)
                .IsRequired();
            #endregion

            #region Configuración para la entidad Sales
            modelBuilder.Entity<Sales>()
                .Property(a => a.SaleDate)
                .IsRequired();

            modelBuilder.Entity<Sales>()
                .Property(a => a.PriceTotal)
                .HasPrecision(16,2);

            modelBuilder.Entity<Sales>()
                .Property(a => a.Investment)
                .HasPrecision(16, 2);
            #endregion

            #region Configuración para la entidad SalesClothingSize

            #endregion

            #region Configuración para la entidad ClothingMaterial
            modelBuilder.Entity<SalesClothingSize>()
               .Property(a => a.PriceUnit)
               .HasPrecision(8, 2);
            #endregion

            #region Configuración para la entida Size

            #endregion

            #region Configuración para la entidad user
            modelBuilder.Entity<User>()
               .Property(a => a.UserName)
               .IsRequired()
               .HasMaxLength(250);

            modelBuilder.Entity<User>()
               .Property(a => a.NormalizedName)
               .IsRequired()
               .HasMaxLength(250);

            modelBuilder.Entity<User>()
               .Property(a => a.Email)
               .IsRequired()
               .HasMaxLength(250);

            modelBuilder.Entity<User>()
               .Property(a => a.NormalizedEmail)
               .IsRequired()
               .HasMaxLength(250);

            modelBuilder.Entity<User>()
             .Property(a => a.PasswordHash)
             .IsRequired();

            modelBuilder.Entity<User>()
             .Property(a => a.UserType)
             .IsRequired();
            #endregion
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{            
        //    IConfiguration config = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .AddEnvironmentVariables()
        //        .Build();
        //   // .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //   //.AddJsonFile(fileName)
        //   //.Build();
        //    optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        //}
    }
}
