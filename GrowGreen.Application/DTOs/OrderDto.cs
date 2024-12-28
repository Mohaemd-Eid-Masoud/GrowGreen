using GrowGreen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowGreen.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Product Products { get; set; }
        public int UserId { get; set; }
    
    }

}
