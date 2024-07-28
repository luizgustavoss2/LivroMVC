using Dapper.Contrib.Extensions;
using System;

namespace Livros.Infra.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Computed]
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
