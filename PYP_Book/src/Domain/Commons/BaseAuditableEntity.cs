using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commons
{
    public class BaseAuditableEntity:BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        //public string? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        //public string? ModifiedBy { get; set; }
        public BaseAuditableEntity()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }
    }
}
