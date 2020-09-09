using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AuthorTest
{ 
    // This class configures the application at startup.
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Creates and configures the WebHost and returns it
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseStartup<Startup>();
                });
    }

}

