using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QueryExample.EntityFramework;
using System;

namespace QueryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // build configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory);
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configBuilder.Build();

            // build DI container
            var services = new ServiceCollection();
            services.AddLogging((builder) =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            services.AddDbContextPool<QueryExampleContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IRepository, Repository>();
            var serviceProvider = services.BuildServiceProvider();

            // example code
            var repository = serviceProvider.GetService<IRepository>();
            var userId = 1;
            var competitions = repository.GetFriendsPlayedCompetitiionsAsync(userId).GetAwaiter().GetResult();

            foreach (var competition in competitions)
                Console.WriteLine(string.Format("My friend passed quest '{0}' which has {1} likes", competition.Quest.Name, competition.LikeCount));
        }
    }
}
