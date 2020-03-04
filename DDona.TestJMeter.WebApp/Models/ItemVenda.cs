using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDona.TestJMeter.WebApp.Models
{
    [Table("ItemVenda")]
    public class ItemVenda
    {
        [Key]
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public string Item { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
    }
}
