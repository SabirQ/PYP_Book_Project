using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class Basket:BaseEntity
    {
        public decimal Total { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
