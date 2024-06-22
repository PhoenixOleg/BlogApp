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
            // ������� WebApplicationBuilder
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // �������� ������ ����������� �� ����� ������������ appsettings.json
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            #region ���������� �������� � ���������
            // ��������� �������� BlogDBContext � �������� ������� � ����������
            builder.Services.AddDbContext<BlogDBContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddUnitOfWork();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BlogDBContext>();
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization(); //���������� �����������

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
