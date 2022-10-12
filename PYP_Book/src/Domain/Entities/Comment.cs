using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Domain.Entities
{
    public class Comment: BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Message { get; set; }
        public int? ReplyId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public DateTime CommentedAt { get; set; }
        public DateTime EditedAt { get; set; }
    }
}
