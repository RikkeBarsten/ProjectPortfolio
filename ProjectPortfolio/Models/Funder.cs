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

        [Display(Name = "Formål")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Hjemmeside")]
        public string Url { get; set; }

        // Navigation property
        public virtual List<Project> Projects { get; set; }

        // Navigation property
        public virtual List<Deadline> Deadlines { get; set; }
    }

    
}