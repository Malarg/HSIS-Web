using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web;

namespace HSIS_Web.Models
{
    public class Shell
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Capasity { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [ForeignKey("Storage")]
        [Display(Name = "Storage")]
        public int? StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}