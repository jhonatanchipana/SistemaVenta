using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Identity.Client;
using SistemaVenta.Entities;

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
        public DbSet<BuyMaterial> BuyMaterial { get; set; }

        /// <summary>
        /// BdSet asociado a compra de material detalle
        /// </summary>
        public DbSet<BuyMaterialDetail> BuyMaterialDetail { get; set; }

        /// <summary>
        /// BdSet asociado a la campaña
        /// </summary>
        public DbSet<Campaign> Campaign { get; set; }

        /// <summary>
        /// BdSet asociado a la ropa
        /// </summary>
        public DbSet<Clothing> Clothing { get; set; }

        /// <summary>
        /// BdSet asociado a la categoria de ropa
        /// </summary>
        public DbSet<ClothingCategory> ClothingCategory { get; set; }

        /// <summary>
        /// BdSet asociado a la fabricacion de ropa
        /// </summary>
        public DbSet<ClothingManufacturing> ClothingManufacturing { get; set; }

        /// <summary>
        /// BdSet asociado al modelo de la ropa
        /// </summary>
        public DbSet<ClothingModel> ClothingModel { get; set; }

        /// <summary>
        /// BdSet asociado al material
        /// </summary>
        public DbSet<Material> Material { get; set; }

        /// <summary>
        /// BdSet asociado a la venta de ropa
        /// </summary>
        public DbSet<SalesClothing> SalesClothing { get; set; }

        /// <summary>
        /// BdSet asociado la venta de ropa detalle
        /// </summary>
        public DbSet<SalesClothingDetail> SalesClothingDetail { get; set; }

        /// <summary>
        /// BdSet asociado al usuario
        /// </summary>
        public DbSet<User> User { get; set; }

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

            #region Configuración para la entidad BuyMaterial
            modelBuilder.Entity<BuyMaterial>()
                .Property(a => a.BuyDate)
                .IsRequired()
                .HasColumnType("date");

            modelBuilder.Entity<BuyMaterial>()
                .Property(a => a.NameBuyer)
                .HasMaxLength(120);

            modelBuilder.Entity<BuyMaterial>()
               .Property(a => a.CostTotal)
               .HasPrecision(16, 2);
            #endregion

            #region Configuración para la entidad BuyMaterialDetail
            modelBuilder.Entity<BuyMaterialDetail>()
                .Property(a => a.Price)
                .IsRequired()
                .HasPrecision(16, 2);
            #endregion

            #region Configuración para la entidad Campaign
            modelBuilder.Entity<Campaign>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(120);

            modelBuilder.Entity<Campaign>()
                .Property(a => a.InitialDate)
                .IsRequired()
                .HasColumnType("date");

            modelBuilder.Entity<Campaign>()
               .Property(a => a.EndDate)
               .HasColumnType("date");

            modelBuilder.Entity<Campaign>()
               .Property(a => a.StatusCampaignType)
               .IsRequired();
            #endregion

            #region Configuración para la entidad Clothing
            modelBuilder.Entity<Clothing>()
               .Property(a => a.Name)
               .IsRequired()
               .HasMaxLength(150);

            modelBuilder.Entity<Clothing>()
               .Property(a => a.Descripcion)
               .HasMaxLength(250);

            modelBuilder.Entity<Clothing>()
               .Property(a => a.PriceSuggested)
               .HasPrecision(8, 2);

            modelBuilder.Entity<Clothing>()
               .Property(a => a.Size)
               .IsRequired();

            modelBuilder.Entity<Clothing>()
               .Property(a => a.InvestmentUnit)
               .HasPrecision(8, 2);
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

            #region Configuración para la entidad ClothingManufacturing
            modelBuilder.Entity<ClothingManufacturing>()
              .Property(a => a.ManufacturingDate)
              .IsRequired();
            #endregion

            #region Configuración para la entidad ClothingManufacturingDetail

            #endregion

            #region Configuración para la entidad ClothingModel
            modelBuilder.Entity<ClothingModel>()
              .Property(a => a.Name)
              .IsRequired()
              .HasMaxLength(120);

            modelBuilder.Entity<ClothingModel>()
              .Property(a => a.Descripcion)
              .HasMaxLength(250);
            #endregion

            #region Configuración para la entidad Material
            modelBuilder.Entity<Material>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(120);

            modelBuilder.Entity<Material>()
                .Property(a => a.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Material>()
                .Property(a => a.Cost)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Material>()
                .Property(a => a.UnitMeasurement)
                .IsRequired();
            #endregion

            #region Configuración para la entidad SalesClothing
            modelBuilder.Entity<SalesClothing>()
                .Property(a => a.SaleDate)
                .IsRequired();

            modelBuilder.Entity<SalesClothing>()
                .Property(a => a.PriceTotal)
                .HasPrecision(16,2);

            modelBuilder.Entity<SalesClothing>()
                .Property(a => a.Investment)
                .HasPrecision(16, 2);
            #endregion

            #region Configuración para la entidad SalesClothingDetail
            modelBuilder.Entity<SalesClothingDetail>()
               .Property(a => a.PriceSold)
               .HasPrecision(8, 2);

            modelBuilder.Entity<SalesClothingDetail>()
               .Property(a => a.InvestmentUnit)
               .HasPrecision(8, 2);
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
