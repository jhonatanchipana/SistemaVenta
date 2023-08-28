using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Dto.Object.Account;
using Venta.Services.Bussiness;
using Venta.Services.Interface;

namespace Venta.Services
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IClothingService, ClothingService>();
            services.AddScoped<IManufacturingService, ManufacturingService>();
            services.AddScoped<IReportInOutService, ReportInOutService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserStore<UserDTO>, UsersStore>();
            services.AddScoped<IClothingCategoryService, ClothingCategoryService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IClothingSizeService, ClothingSizeService>();

            return services;
        }
    }
}
