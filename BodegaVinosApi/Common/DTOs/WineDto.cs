using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class WineDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Variety { get; set; }
        public required int Year { get; set; }
        public required string Region { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
