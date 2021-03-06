using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SteveTalksCode.MiddlewareStylesDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseDefaultServiceProvider((_, options) =>
                {
                    // deliberately turning this off to prove captured dependency!
                    options.ValidateScopes = false;
                    options.ValidateOnBuild = true;
                });
    }
}
