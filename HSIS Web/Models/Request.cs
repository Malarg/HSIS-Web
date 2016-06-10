using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HSIS_Web.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        [Required]
        public string Problem { get; set; }

        public string Solution { get; set; }

        [Required]
        public int Cost { get; set; }

        [ForeignKey("Client")]
        [Display(Name = "Client")]
        public int? ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Assistant")]
        [Display(Name = "Assistant")]
        public int? AssistantId { get; set; }
        public Assistant Assistant { get; set; }
    }
}