using BlogApp.DAL;
using BlogApp.DAL.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создаем WebApplicationBuilder
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Получаем строку подключения из файла конфигурации appsettings.json
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            #region Добавление сервисов в контейнер
            // Добавляем контекст BlogDBContext в качестве сервиса в приложение
            builder.Services.AddDbContext<BlogDBContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddUnitOfWork();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BlogDBContext>();
            builder.Services.AddControllersWithViews();

            #endregion

            // Создаем WebApplication
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization(); //Добавление авторизации

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
