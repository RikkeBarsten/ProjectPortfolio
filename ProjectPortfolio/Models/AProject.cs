
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
        [Range(1000, 9999, ErrorMessage = "Ansvarsnummer skal være et tal mellem 1000 og 9999")]
        public int? RespNo { get; set; }

        [Display(Name = "Afdeling")]
        public string Section { get; set; }

        [Display(Name = "Link til projekt")]
        [DataType(DataType.Url)]
        public string ProjectLink { get; set; }

        [ForeignKey("Program")]
        [Required]
        public string ProgramId { get; set; }


        //Navigation properties
        //public virtual Person Person { get; set; }
        public virtual Program Program { get; set; }

        public virtual List<File> Files { get; set; }



        //Set section based on RespNo

        public void SetSection(int? RespNo)
        {
            if (RespNo >= 3000 && RespNo < 4000 )
            {
                this.Section = "Stab";
            }
            else if (RespNo >= 5100 && RespNo < 5200)
            {
                this.Section = "Unge, vejledning og erhverv";
            }
            else if (RespNo >= 5300 && RespNo < 5400)
            {
                this.Section = "El, Automation og Data";
            }
            else if (RespNo >= 5400 && RespNo < 5500)
            {
                this.Section = "Byggeri, Produktion og Service";
            }
            else if (RespNo >= 5600 && RespNo < 5700)
            {
                this.Section = "H.C. Ørsted Gymnasiet";
            }
            else if (RespNo >= 5700 && RespNo < 5800)
            {
                this.Section = "Praktikcenteret";
            }
            else if (RespNo >= 5900 && RespNo < 6000)
            {
                this.Section = "Mekanik, Transport og Aviation";
            }
            else if (RespNo >= 2200 && RespNo < 2300 )
            {
                this.Section = "Eniga";
            }
            else
            {
                this.Section = "Ukendt afdeling";
            }
        }
       
    }

    
}

