using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppDbContext.Model
{
    public class BaseEntity<TPrimary>
    {
        [Key]
        public TPrimary Id { get; set; }
    }
}
