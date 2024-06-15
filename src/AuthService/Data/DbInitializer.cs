using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.DBContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            Console.WriteLine("Migrating");
            scope.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            //SeedData(scope.ServiceProvider.GetService<DatabaseContext>());
        }
 }
}