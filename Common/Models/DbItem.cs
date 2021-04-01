using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains.Common.Models
{
    public class DbItem
    {
        [Key]
        public int Id { get; set; }
    }
}
