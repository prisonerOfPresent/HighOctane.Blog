using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighOctane.Blog.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            string dbUrl = configuration["DATABASE_URL"];
            if (string.IsNullOrEmpty(dbUrl))
                dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (string.IsNullOrEmpty(dbUrl))
                dbUrl = "postgres://postgres:root123@localhost:5432/HighOctaneBlog";


            var builder = new PostgresqlConnectionStringBuilder(dbUrl)
            {
                Pooling = true,
                TrustServerCertificate = true,
                SslMode = SslMode.Require
            };


            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql( builder.ConnectionString );
            return new AppDbContext(optionsBuilder.Options);
        }

    }
}
