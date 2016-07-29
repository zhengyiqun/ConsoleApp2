using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace MysqlEntityFarmework
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) {

        }

        
    }
}
