using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class WishListItem:BaseEntity
    {
        public Book Book { get; set; }
        public int WishListId { get; set; }
        public WishList WishList { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
