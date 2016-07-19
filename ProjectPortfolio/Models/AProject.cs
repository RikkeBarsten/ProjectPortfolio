
using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectPortfolio.Models
{
    public abstract class AProject : IComparable<AProject>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Navn")]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Projektstart")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Projektslut")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }
        
        [ForeignKey("Person")]
        public int? PersonId { get; set; }

        [ForeignKey("Program")]
        public string ProgramId { get; set; }


        //Navigation properties
        public virtual Person Person { get; set; }
        public virtual Program Program { get; set; }


        // Implement IComparable, to be able to use my sorting algorithms
        public int CompareTo(AProject other)
        {
            return this.StartDate.CompareTo(other.StartDate);
        }
    }

    
}

