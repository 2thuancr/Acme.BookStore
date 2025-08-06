using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Categories
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
