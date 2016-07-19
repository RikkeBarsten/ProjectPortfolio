using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProjectPortfolio.Models
{
    public class Funder
    {
        [Key]
        public int FunderId { get; set; }

        [Display(Name = "Fond")]
        public string Name { get; set; }

        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }

        // Navigation property
        public virtual List<Project> Projects { get; set; }
    }

    
}