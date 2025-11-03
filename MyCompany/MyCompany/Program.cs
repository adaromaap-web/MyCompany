using Microsoft.AspNetCore.Builder;
using MyCompany.Infrastruction;

namespace MyCompany
{
    using MyCompany.Infrastruction;
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
           // WebApplication app = builder.Build();

            // Подключаем в конфигурацию файл appsettings.json и appsettings.{Environment}.json

            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                // .AddJsonFile($"appsettings.{app.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            // Оборачиваем секцию "Project" в объектную форму для удобства

            IConfiguration configuration = configBuilder.Build();
            AppConfig config = configuration.GetSection("Project").Get<AppConfig>()!;

            //Подключаем функционал контроллеров

            builder.Services.AddControllersWithViews();

            //Собираем конфигурацию
            WebApplication app = builder.Build();

            //! Порядок следования middleware очень важен, они будут выполяться согласно нему
            app.UseStaticFiles();

            // Подключаем использование статичных файлов (js, css, любых)
            app.UseStaticFiles();

            // Подключаем систему маршрутизации
            app.UseRouting();

            // Регистрируем нужные нам маршруты

            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");    

           await app.RunAsync();
        }
    }
}
