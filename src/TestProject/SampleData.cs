using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MysqlEntityFarmework;
using Microsoft.Extensions.DependencyInjection;


namespace ConsoleApp2
{
    public class SampleData
    {
        public static void InitDB()
        {
            using (var context = new MyContext())
            {
                bool bl = context.Database.EnsureCreated();
                //var context = service.GetService<MyContext>();

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
            }
        }
    }
}
