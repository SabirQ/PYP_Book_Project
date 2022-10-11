using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class BasketItem:BaseEntity
    {
        public Book Book { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public Basket Basket { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime SaleDate { get; set; }
        public bool Applied { get; set; }
    }
}
