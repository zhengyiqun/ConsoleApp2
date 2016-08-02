using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MysqlEntityFarmework
{
    public class Blog
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }

        public virtual User user { get; set; }

        public string Content { get; set; }

        public JsonObject<List<string>> Tags { get; set; }
    }
}
