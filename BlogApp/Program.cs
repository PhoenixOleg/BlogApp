using BlogApp.DAL;
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
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");


            #region ���������� �������� � ���������
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // ��������� �������� BlogDBContext � �������� ������� � ����������
            builder.Services.AddDbContext<BlogDBContext>(options => options.UseSqlServer(connection));

            #endregion
            
            // ������� WebApplication
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

            app.UseAuthorization(); //���������� �����������

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
