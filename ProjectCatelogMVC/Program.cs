
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCatDAL;
using ProductCatDAL.Context;
using ProductCateBBL.Repositories.interfaces;
using ProductCateBBL.Repositories.RepoClasses;

namespace ProjectCatelogMVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddDbContext<ProductContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
            });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ProductContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();






            var app = builder.Build();

            #region HostService

            
                using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    //var loggerFactory = services.GetRequiredService<LoggerFactory>();

                    var dbContext = services.GetRequiredService<ProductContext>();


                    dbContext.Database.Migrate();
                    await StoreContextSeed.SeedCategoriesAsync(dbContext);
                    
                }
                catch (Exception ex)
                {
                    // var logger=  LoggerFactory.CreateLogger<Program>();
                    throw ex;
                }

            }
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
