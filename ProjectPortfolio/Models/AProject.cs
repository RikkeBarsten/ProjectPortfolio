
using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectPortfolio.Models
{
    public abstract class AProject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProjectId { get; set; }

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

        [Display(Name = "Kort resume")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Bemærkning")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

        //[ForeignKey("Person")]
        //public int? PersonId { get; set; }

        [Display(Name = "Kontaktperson")]
        public string Person { get; set; }

        [Display(Name = "Ansvarlig leder/chef")]
        public string Responsible { get; set; }

        [Display(Name = "Ansvarsnummer")]
        public string RespNo { get; set; }

        [ForeignKey("Program")]
        [Required]
        public string ProgramId { get; set; }


        //Navigation properties
        //public virtual Person Person { get; set; }
        public virtual Program Program { get; set; }

        public virtual List<File> Files { get; set; }


       
    }

    
}

