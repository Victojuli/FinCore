using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FinCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run(); // запускаем приложение
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()               // настраиваем веб-сервер Kestrel 
                .UseContentRoot(Directory.GetCurrentDirectory())    // настраиваем корневой каталог приложения
                .UseIISIntegration()
                .UseStartup<Startup>() // устанавливаем главный файл приложения
                .Build(); // создаем хост
    }
}
