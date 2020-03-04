using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDona.TestJMeter.WebApp.Models
{
    [Table("Venda")]
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
    }
}
