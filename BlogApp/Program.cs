using BlogApp.DAL;
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
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");


            #region Добавление сервисов в контейнер
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Добавляем контекст BlogDBContext в качестве сервиса в приложение
            builder.Services.AddDbContext<BlogDBContext>(options => options.UseSqlServer(connection));

            #endregion
            
            // Создаем WebApplication
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization(); //Добавление авторизации

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
