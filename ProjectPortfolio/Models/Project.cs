using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectPortfolio.Models
{
    public class Project : AProject
    {

        public Project()
        {
            this.Status = Status.Ny;
        }


        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(0.1, 10000000)]
        [DataType(DataType.Currency)]
        public decimal? Budget { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Egenfinansiering")]
        [Range(0.1, 10000000)]
        [DataType(DataType.Currency)]
        public decimal? SelfFinancing { get; set; }

        [Display(Name = "Flere partnere?")]
        public Multiple? MultiplePartners { get; set; }

        [Display(Name = "Ejer/partner")]
        public Owner? Owner { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Samlet budget (partnere)")]
        [Range(0.1, 10000000)]
        [DataType(DataType.Currency)]
        public decimal? AggregatedBudget { get; set; }

        [DisplayFormat(NullDisplayText = "Ikke tildelt")]
        [Display(Name = "Projektnummer")]
        public int? ProjectNumber { get; set; }

        [Display(Name = "Eksternt projektnummer")]
        public string ExtProjectNumber { get; set; }

        [Required]
        public Status Status { get; set; }

        [Display(Name = "Primært indsatsområde")]
        public String PrimaryFocus { get; set; }

        [Display(Name = "Underindsatsområde")]
        public string SecondaryFocus { get; set; }

        [ForeignKey("Funder")]
        public int? FunderId { get; set; }

        //Navigation property
        [Display(Name = "Bevillingstype")]
        public virtual Funder Funder { get; set; }
    }

    public enum Status
    {
        Ny,
        Ansøgt,
        [Display(Name = "Bevilget - Ikke startet")]
        Bevilget_IS,
        [Display(Name = "Bevilget - Startet")]
        Bevilget_S,
        Lukket,
        Afslag,
        Afbrudt
    }

    public enum Owner
    {
        Ejer,
        Partner
    }

    public enum Multiple
    {
        Nej,
        Ja
    }

    
}