using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using QueryExample.EntityFramework;
using System;

namespace QueryExample
{
    public class QueryExampleContextFactory : IDesignTimeDbContextFactory<QueryExampleContext>
    {
        public QueryExampleContext CreateDbContext(string[] args)
        {
            // build configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory);
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configBuilder.Build();

            // build options
            var optionsBuilder = new DbContextOptionsBuilder<QueryExampleContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new QueryExampleContext(optionsBuilder.Options);
        }
    }
}
