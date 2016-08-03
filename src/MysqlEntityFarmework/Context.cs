using Microsoft.EntityFrameworkCore;

namespace MysqlEntityFarmework
{
    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseMySql(
            "Server=xianggang.cloudapp.net;database=mydb;uid=root;pwd=zheng123!;");

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<User> Users { get; set; }
    }


}
