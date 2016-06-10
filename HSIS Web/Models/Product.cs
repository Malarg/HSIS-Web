using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSIS_Web.Models
{
    public enum ProductType
    {
        CPU, 
        Motherboard,
        Videocard,
        RAM,
        HardDisk,
        PowerSupply,
        Case
    }

    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public ProductType Type { get; set; }

        [Display(Name = "Purchace price")]
        public int PurchacePrice { get; set; }

        [Display(Name = "Selling price")]
        public int SellingPrice { get; set; }
        public int Quantity { get; set; }
        public string Deskription { get; set; }

        [ForeignKey("Shell")]
        [Display(Name = "Shell")]
        public int? ShellId { get; set; }
        public Shell Shell { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}