using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Application.Common.Exceptions
{
    public class PrimaryDeleteException:Exception
    {
        public PrimaryDeleteException(string message) : base(message) { }
    }
}
