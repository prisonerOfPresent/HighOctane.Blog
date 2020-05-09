using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighOctane.Blog.Data
{
    public static class MigrationHelper
    {
        public static void PrePopulate( IApplicationBuilder app ) 
        {
            using (var serviceScope = app.ApplicationServices.CreateScope()) 
            {
                DoMigrate( serviceScope.ServiceProvider.GetService<AppDbContext>() );
            }
        }

        public static void DoMigrate( AppDbContext appDbContext ) 
        {
            Console.WriteLine("Applying Migrations...");
            if ( appDbContext.Database.GetMigrations() == null )
                appDbContext.Database.Migrate();
        }
    }
}
