using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MysqlEntityFarmework;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;


namespace ConsoleApp2
{
    public class SampleData
    {
        public async static Task InitDB(IServiceProvider service)
        {
            var context = service.GetService<MyContext>();

            if (context.Database != null && context.Database.EnsureCreated())
            {
                var user = new User { Name = "Yuuko" };
                context.Add(user);

                var blog1 = new Blog
                {
                    Title = "",
                    UserId = user.UserId,
                    Tags = new List<string>() { "ASP.NET Core", "MySQL", "Pomelo" }
                };
                context.Add(blog1);

                var blog2 = new Blog
                {
                    Title = "Title #2",
                    UserId = user.UserId,
                    Tags = new List<string>() { "ASP.NET Core", "MySQL" }
                };
                context.Add(blog2);
                context.SaveChanges();

                blog1.Tags.Object.Clear();
                context.SaveChanges();

                blog1.Tags.Object.Add("Pomelo");
                context.SaveChanges();
            }
            await context.SaveChangesAsync();
        }
    }
}
