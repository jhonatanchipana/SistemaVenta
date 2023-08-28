using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Data.Connection;
using Venta.Data.Interfaces;
using Venta.Data.Repository;

namespace Venta.Data
{
    /// <summary>
    /// Clase que inyecta en la configuración lo requerido para el startup de la aplicación
    /// </summary>
    public static class DependencyContainer
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")) );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPurchaseMaterialRepository, PurchaseMaterialRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IReportInOutRepository, ReportInOutRepository>();
            services.AddScoped<IManufacturingRepository, ManufacturingRepository>();
            services.AddScoped<IManufacturingClothingSizeRepository, ManufacturingClothingSizeRepository>();
            services.AddScoped<IClothingRepository, ClothingRepository>();
            services.AddScoped<IClothingMaterialRepository, ClothingMaterialRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<ISalesClothingRepository, SalesClothingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClothingCategoryRepository, ClothingCategoryRepository>();
            services.AddScoped<IClothingSizeRepository, ClothingSizeRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();

            return services;
        }
    }
}
