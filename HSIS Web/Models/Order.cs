using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace HSIS_Web.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Profit { get; set; }

        public int Income { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [ForeignKey("Product")]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Vendor")]
        [Display(Name = "Vendor")]
        public int? VendorId { get; set; }
        public Vendor Vendor { get; set; }

        [ForeignKey("Client")]
        [Display(Name = "Client")]
        public int? ClientId { get; set; }
        public Client Client { get; set; }
    }
}