using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dastone.Extension
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // Brand Policies
                options.AddPolicy("GetBrand", policy =>
                    policy.RequireClaim("Brand", "GetBrand").RequireAuthenticatedUser());
                options.AddPolicy("CreateBrand", policy =>
                    policy.RequireClaim("Brand", "CreateBrand").RequireAuthenticatedUser());
                options.AddPolicy("DeleteBrand", policy =>
                    policy.RequireClaim("Brand", "DeleteBrand").RequireAuthenticatedUser());
                options.AddPolicy("UpdateBrand", policy =>
                    policy.RequireClaim("Brand", "UpdateBrand").RequireAuthenticatedUser());
                // Model Policies
                options.AddPolicy("GetModel", policy =>
                    policy.RequireClaim("Model", "GetModel").RequireAuthenticatedUser());
                options.AddPolicy("CreateModel", policy =>
                    policy.RequireClaim("Model", "CreateModel").RequireAuthenticatedUser());
                options.AddPolicy("DeleteModel", policy =>
                    policy.RequireClaim("Model", "DeleteModel").RequireAuthenticatedUser());
                options.AddPolicy("UpdateModel", policy =>
                    policy.RequireClaim("Model", "UpdateModel").RequireAuthenticatedUser());
                // MeasurementUnit Policies
                options.AddPolicy("GetMeasurementUnit", policy =>
                    policy.RequireClaim("MeasurementUnit", "GetMeasurementUnit").RequireAuthenticatedUser());
                options.AddPolicy("CreateMeasurementUnit", policy =>
                    policy.RequireClaim("MeasurementUnit", "CreateMeasurementUnit").RequireAuthenticatedUser());
                options.AddPolicy("DeleteMeasurementUnit", policy =>
                    policy.RequireClaim("MeasurementUnit", "DeleteMeasurementUnit").RequireAuthenticatedUser());
                options.AddPolicy("UpdateMeasurementUnit", policy =>
                    policy.RequireClaim("MeasurementUnit", "UpdateMeasurementUnit").RequireAuthenticatedUser());
                // Supplier Policies
                options.AddPolicy("GetSupplier", policy =>
                    policy.RequireClaim("Supplier", "GetSupplier").RequireAuthenticatedUser());
                options.AddPolicy("CreateSupplier", policy =>
                    policy.RequireClaim("Supplier", "CreateSupplier").RequireAuthenticatedUser());
                options.AddPolicy("DeleteSupplier", policy =>
                    policy.RequireClaim("Supplier", "DeleteSupplier").RequireAuthenticatedUser());
                options.AddPolicy("UpdateSupplier", policy =>
                    policy.RequireClaim("Supplier", "UpdateSupplier").RequireAuthenticatedUser());
                // Consumable Policies
                options.AddPolicy("GetConsumable", policy =>
                    policy.RequireClaim("Consumable", "GetConsumable").RequireAuthenticatedUser());
                options.AddPolicy("CreateConsumable", policy =>
                    policy.RequireClaim("Consumable", "CreateConsumable").RequireAuthenticatedUser());
                options.AddPolicy("DeleteConsumable", policy =>
                    policy.RequireClaim("Consumable", "DeleteConsumable").RequireAuthenticatedUser());
                options.AddPolicy("UpdateConsumable", policy =>
                    policy.RequireClaim("Consumable", "UpdateConsumable").RequireAuthenticatedUser());
                // Product Policies
                options.AddPolicy("GetProduct", policy =>
                    policy.RequireClaim("Product", "GetProduct").RequireAuthenticatedUser());
                options.AddPolicy("CreateProduct", policy =>
                    policy.RequireClaim("Product", "CreateProduct").RequireAuthenticatedUser());
                options.AddPolicy("DeleteProduct", policy =>
                    policy.RequireClaim("Product", "DeleteProduct").RequireAuthenticatedUser());
                options.AddPolicy("UpdateProduct", policy =>
                    policy.RequireClaim("Product", "UpdateProduct").RequireAuthenticatedUser());
                // ProductType Policies
                options.AddPolicy("GetProductType", policy =>
                   policy.RequireClaim("ProductType", "GetProductType").RequireAuthenticatedUser());
                options.AddPolicy("CreateProductType", policy =>
                   policy.RequireClaim("ProductType", "CreateProductType").RequireAuthenticatedUser());
                options.AddPolicy("UpdateProductType", policy =>
                   policy.RequireClaim("ProductType", "UpdateProductType").RequireAuthenticatedUser());
                options.AddPolicy("DeleteProductType", policy =>
                   policy.RequireClaim("ProductType", "DeleteProductType").RequireAuthenticatedUser());

                // Warehouse Policies
                options.AddPolicy("GetWarehouse", policy =>
                   policy.RequireClaim("Warehouse", "GetWarehouse").RequireAuthenticatedUser());
                options.AddPolicy("CreateWarehouse", policy =>
                   policy.RequireClaim("Warehouse", "CreateWarehouse").RequireAuthenticatedUser());
                options.AddPolicy("UpdateWarehouse", policy =>
                   policy.RequireClaim("Warehouse", "UpdateWarehouse").RequireAuthenticatedUser());
                options.AddPolicy("DeleteWarehouse", policy =>
                   policy.RequireClaim("Warehouse", "DeleteWarehouse").RequireAuthenticatedUser());
                // Inventory Policies
                options.AddPolicy("GetInventoryStock", policy =>
                policy.RequireClaim("InventoryStock", "GetInventoryStock").RequireAuthenticatedUser());
                options.AddPolicy("CreateInventoryStock", policy =>
                   policy.RequireClaim("InventoryStock", "CreateInventoryStock").RequireAuthenticatedUser());
                options.AddPolicy("UpdateInventoryStock", policy =>
                   policy.RequireClaim("InventoryStock", "UpdateInventoryStock").RequireAuthenticatedUser());
                options.AddPolicy("DeleteInventoryStock", policy =>
                   policy.RequireClaim("InventoryStock", "DeleteInventoryStock").RequireAuthenticatedUser());


                // StockChange Policies
                options.AddPolicy("GetStockChange", policy =>
                   policy.RequireClaim("StockChange", "GetStockChange").RequireAuthenticatedUser());
                options.AddPolicy("CreateStockChange", policy =>
                   policy.RequireClaim("StockChange", "CreateStockChange").RequireAuthenticatedUser());
                options.AddPolicy("UpdateStockChange", policy =>
                   policy.RequireClaim("StockChange", "UpdateStockChange").RequireAuthenticatedUser());
                options.AddPolicy("DeleteStockChange", policy =>
                   policy.RequireClaim("StockChange", "DeleteStockChange").RequireAuthenticatedUser());
            });

            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtSettings = configuration.GetSection("JwtSettings"); //appsettingsteki istenilen tagı okumaya yarar

            var decryptedsecretKey =jwtSettings["SecretKey"];
            var decryptedValidateIssue = jwtSettings["ValidateIssue"];
            var decryptedValidateAudience = jwtSettings["ValidateAudience"];

            var secretKey = jwtSettings["SecretKey"];
            //Tokenlarda hassas veriler hiçbir şekilde yer almamalıdır //userName, password, eMail, phoneNumber etc
            services.AddAuthentication("LibraryCookieScheme")
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = decryptedValidateIssue,
                    ValidAudience = decryptedValidateAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(decryptedsecretKey)),
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(jwtSettings.GetSection("Expire").Value))
                }
                ).AddCookie("LibraryCookieScheme", opt =>
                {
                    opt.Cookie.Name = "LibraryCookie";
                    opt.LoginPath = "/Authentication/Login";
                    opt.LogoutPath = "/Authentication/Logout";
                    opt.AccessDeniedPath = "/Authentication/Login";
                });

            return services;
        }
        public static IServiceCollection AddCustomSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            return services;
        }
        

       
        public static IServiceCollection AddCustomHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("MyClient", client =>
            {
                var baseUrl = configuration.GetValue<string>("ClientUrl:BaseUrl");
               
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient(); // Diğer genel HttpClient kaydı

            return services;
        }
    }
}
