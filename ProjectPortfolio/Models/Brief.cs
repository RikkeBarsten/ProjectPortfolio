using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPortfolio.Models
{
    public class Brief : AProject
    {
        public int Hours { get; set; }
        public SteeringCommittee? SteeringCommittee { get; set; }
    }
}


namespace ProjectPortfolio.Models
{
    public enum SteeringCommittee
    {
        Chefgruppe,
        Stabsmøde,
        DriftsmødeEUD,
        DriftsmødeHTX
    }
}