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
        public decimal Budget { get; set; }

        [DisplayFormat(NullDisplayText = "Ikke tildelt")]
        [Display(Name = "Projektnummer")]
        public int? ProjectNumber { get; set; }

        [Required]
        public Status Status { get; set; }

        [ForeignKey("Funder")]
        public int? FunderId { get; set; }

        //Navigation property
        [Display(Name = "Fond")]
        public virtual Funder Funder { get; set; }
    }

    public enum Status
    {
        Ny,
        Ansøgt,
        Bevilget,
        Afslag,
        Afbrudt
    }
}