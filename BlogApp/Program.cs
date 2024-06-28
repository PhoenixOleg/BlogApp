using AutoMapper;
using Azure;
using BlogApp.BLL;
using BlogApp.BLL.Services;
using BlogApp.BLL.Services.Interfaces;
using BlogApp.DAL;
using BlogApp.DAL.Entities;
using BlogApp.DAL.Extentions;
using BlogApp.DAL.Models;
using BlogApp.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore;
using System;

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

            builder.Services.AddIdentity<UserEntity, RoleEntity>(
                options =>
                { 
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 5;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;
                })
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BlogDBContext>();

            builder.Services.AddUnitOfWork();
            builder.Services.AddCustomRepository<TagEntity, TagRepository>();

            #region Подключаем автомаппинг (заменен на более понятный из модуля 33)
            //Закоменченный код из модуля 34
            //var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            //builder.Services.AddAutoMapper(assembly);

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile()); //Добавляем в профиль конфигурации класс с описанием маппинга объектов
            });

            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
            #endregion Подключаем автомаппинг
            builder.Services
                .AddTransient<ITagService, TagService>()
                .AddTransient<IArticleService, ArticleService>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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

            app.UseAuthentication(); //Добавление аутентификации
            app.UseAuthorization(); //Добавление авторизации
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
