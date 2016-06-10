using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HSIS_Web.Models
{
    public class Storage
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Capasity { get; set; }
        public virtual ICollection<Shell> Shells { get; set; } = new List<Shell>();
    }
}