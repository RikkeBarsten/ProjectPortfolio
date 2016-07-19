using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ProjectPortfolio.Models

{
    public class Program
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }

        
        [Display(Name = "Strategisk program")]
        public string AggName
        {
            get { return "Program "+ ProgramId + ": " + ProgramName; }
        }


        // Navigation property
        public virtual List<AProject> AProjects { get; set; }
    }
}