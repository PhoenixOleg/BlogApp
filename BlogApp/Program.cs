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
            // ������� WebApplicationBuilder
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // �������� ������ ����������� �� ����� ������������ appsettings.json
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            #region ���������� �������� � ���������
            // ��������� �������� BlogDBContext � �������� ������� � ����������
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

            #region ���������� ����������� (������� �� ����� �������� �� ������ 33)
            //������������� ��� �� ������ 34
            //var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            //builder.Services.AddAutoMapper(assembly);

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile()); //��������� � ������� ������������ ����� � ��������� �������� ��������
            });

            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
            #endregion ���������� �����������
            builder.Services
                .AddTransient<ITagService, TagService>()
                .AddTransient<IArticleService, ArticleService>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            #endregion

            // ������� WebApplication
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

            app.UseAuthentication(); //���������� ��������������
            app.UseAuthorization(); //���������� �����������
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
