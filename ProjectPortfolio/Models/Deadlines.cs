using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectPortfolio.Models
{
    public class Deadline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DeadlineId { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Funder")]
        public int FunderId { get; set; }

        // Naviagation property
        public virtual Funder Funder { get; set; }
        
    }
}