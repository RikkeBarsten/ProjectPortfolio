﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProjectPortfolio.Models;

namespace ProjectPortfolio.DAL
{
    public class PortfolioInitializer : DropCreateDatabaseAlways<PortfolioContext>
    {
        protected override void Seed(PortfolioContext context)
        {
            var funders = new List<Funder>
            {
                new Funder { Name= "AUB",
                    Description ="Arbejdsgivernes Uddannelses Bidrag støtter primært praktikpladsopsøgende arbejde." },
                new Funder { Name = "Erasmus+",
                    Description = "EU Fond, der støtter mobilitetsprojekter på EUD-området."},
                new Funder { Name = "TrygFonden",
                    Description="Privat fond, der støtter et bredt udvalg af projekter. Ansøgninsfrist for regionale projekter 1. marts og 1. september."},
                new Funder {Name = "Kompetencefonden",
                    Description="Støtter kompetenceudviklingsprojekter"},
                new Funder {Name="Knud Højgaard Fond",
                    Description="Knud Højgaards Fond er en almennyttig erhvervsdrivende fond, der bl.a. støtter initiativrige studerende til at realisere drømmen om at dygtiggøre sig ved studier i udlandet."}
            };
            funders.ForEach(f => context.Funders.Add(f));
            context.SaveChanges();

            var people = new List<Person>
            {
                new Person {FirstName="Troels Royster", LastName="Olsen", Email="tro@tec.dk" },
                new Person {FirstName="Rikke", LastName="Barsten", Email="rba@tec.dk" },
                new Person {FirstName = "Glenn Møller", LastName="Jensen", Email="gmj@tec.dk" },
                new Person {FirstName = "Vibeke Holtum", LastName="Nørgaard", Email="vhn@tec.dk" },
                new Person {FirstName = "Karin Staal", LastName="Køhler", Email="ksk@tec.dk" },
                new Person {FirstName = "Charlotte", LastName="Lundius", Email="cl@tec.dk" }
            };

            people.ForEach(p => context.People.Add(p));
            context.SaveChanges();

            var programs = new List<Program>()
            {
                new Program {ProgramId = "A", ProgramName = "Vi udvikler TECs leder og medarbejdere" },
                new Program {ProgramId = "B", ProgramName = "Vi udvikler TECs kerneydelse" },
                new Program {ProgramId = "C", ProgramName = "Vi udvikler TECs infrastruktur" },
                new Program {ProgramId = "D", ProgramName = "Vi udvikler TECs forretningsmæssige position" }
            };
            programs.ForEach(p => context.Programs.Add(p));
            context.SaveChanges();

           var projects = new List<Project>
           {
                new Project { Name="Elbranchens fagfolk i dialog med virksomheder med praktikpladser - 2015",
                    StartDate=DateTime.Parse("06-01-2014"),
                    EndDate=DateTime.Parse("03-01-2015"),
                    FunderId = 1, PersonId = 6, Budget =184344.00m, ProjectNumber = 3004, ProgramId = "B",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam commodo lobortis nisl molestie malesuada. Quisque eleifend quis nisl at tristique. Maecenas suscipit nibh quis elit sagittis, sit amet fermentum enim tincidunt. Phasellus quis metus diam. Fusce id lacus odio. In sagittis in augue eget viverra. Integer tristique turpis et ex pellentesque commodo. Fusce quis venenatis massa. Ut placerat enim at tellus eleifend, non pharetra diam fringilla. Pellentesque eu orci facilisis, pretium magna vel, congue nunc. Curabitur purus arcu, posuere ac blandit sed, pellentesque ut velit. Morbi sed dictum libero. Integer efficitur dolor tortor. Duis dignissim tellus vehicula purus malesuada, eu laoreet quam varius. Pellentesque rhoncus sapien sit amet tellus placerat faucibus. "
                    },
                new Project { Name="Nyt tekonologifag",
                    StartDate=DateTime.Parse("08-01-2015"),
                    EndDate=DateTime.Parse("05-01-2016"),
                    FunderId = 5, PersonId = 1, Budget = 75000.00m, ProjectNumber = 3005, ProgramId = "B"
                    },
                new Project { Name="Svage elever i stærke virksomheder",
                    StartDate=DateTime.Parse("06-01-2014"),
                    EndDate=DateTime.Parse("31-05-2015"),
                    FunderId = 1, PersonId = 5, Budget = 67825.00m, ProjectNumber = 3006, ProgramId = "B"
                    },
                new Project { Name="Kompetencefonden pulje 2015",
                    StartDate=DateTime.Parse("01-01-2015"),
                    EndDate=DateTime.Parse("09-01-2019"),
                    FunderId = 4, PersonId = 2, Budget = 30000.00m, ProjectNumber = 3007, Status = Status.Bevilget, ProgramId = "A"
                    },
                new Project { Name="Clean Tech",
                    StartDate=DateTime.Parse("01-09-2014"),
                    EndDate=DateTime.Parse("31-08-2017"),
                    FunderId = 2, PersonId = 4, Budget = 284515.00m, ProjectNumber = 3008, Status = Status.Bevilget, ProgramId = "B"
                    }
            };

            projects.ForEach(p => context.AProjects.Add(p));
            context.SaveChanges();
        }
    }
}