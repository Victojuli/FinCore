using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FinCore.Data;
using FinCore.Models;
using FinCore.Services;

namespace FinCore
{
    public class Startup
    {
        // Конструктор является необязательной частью класса Startup. 
        //В конструкторе, как правило, производится начальная конфигурация приложения
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //устанавливает, как приложение будет обрабатывать запрос
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // если приложение в процессе разработки
            if (env.IsDevelopment()) //позволяет взаимодействовать со средой, в которой запускается приложение
            {       //Конвейер обработки запроса
                    //   app.UseMiddleware<AuthenticationMiddleware>();//++чтобы пользователь был аутентифицирован при обращении к нашему приложению.
                    //   app.UseMiddleware<RoutingMiddleware>();//++компонент в зависимости от строки запроса возвращает либо определенную строку, либо устанавливает код ошибки.

                app.UseDeveloperExceptionPage();// то выводим информацию об ошибке, при наличии ошибки
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            { 
                app.UseExceptionHandler("/Home/Error");    // установка обработчика ошибок
            }

            app.UseStaticFiles(); // установка обработчика статических файлов  http://localhost:55234/index.html,

            app.UseAuthentication();

            // установка GDPR
            app.UseCookiePolicy();

            //устанавливает компоненты MVC для обработки запроса и, в частности, настраивает систему маршрутизации в приложении.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
