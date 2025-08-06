using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Books
{
    public enum BookStatus
    {
        Available = 0,
        Unavailable = 1,
        Reserved = 2,
        Lost = 3,
        Damaged = 4
    }
}
