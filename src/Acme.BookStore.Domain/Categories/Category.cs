using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Categories
{
    [Table("AppCategories")]
    public class Category : AuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string? Description { get; set; }

        // Quan hệ 1 Category có thể chứa nhiều Book (navigation property)
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
