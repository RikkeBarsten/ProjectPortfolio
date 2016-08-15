using System;
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
                    Url = "https://indberet.virk.dk/myndigheder/stat/AUB/Ansoegning_om_tilskud_fra_AER_til_faglige_udvalg_og_lokale_uddannelsesudvalg",
                    Description ="Arbejdsgivernes Uddannelses Bidrag støtter primært praktikpladsopsøgende arbejde." },
                new Funder { Name = "Erasmus+",
                    Url = "http://ufm.dk/uddannelse-og-institutioner/tilskud-til-udveksling-og-internationale-projekter/programoversigt/erasmusplus",
                    Description = "EU Fond, der støtter mobilitetsprojekter på EUD-området."},
                new Funder { Name = "TrygFonden",
                    Url = "https://www.trygfonden.dk/soeg-stoette",
                    Description="Privat fond, der støtter et bredt udvalg af projekter. Ansøgninsfrist for regionale projekter 1. marts og 1. september."},
                new Funder {Name = "Kompetencefonden",
                    Url = "http://www.kompetenceudvikling.dk/kompetencefonden",
                    Description="Støtter kompetenceudviklingsprojekter"},
                new Funder {Name="Knud Højgaard Fond",
                    Url = "http://www.khf.dk/",
                    Description="Knud Højgaards Fond er en almennyttig erhvervsdrivende fond, der bl.a. støtter initiativrige studerende til at realisere drømmen om at dygtiggøre sig ved studier i udlandet."}
            };
            funders.ForEach(f => context.Funders.Add(f));
            context.SaveChanges();

            //var people = new List<Person>
            //{
            //    new Person {FirstName="Troels Royster", LastName="Olsen", Email="tro@tec.dk" },
            //    new Person {FirstName="Rikke", LastName="Barsten", Email="rba@tec.dk" },
            //    new Person {FirstName = "Glenn Møller", LastName="Jensen", Email="gmj@tec.dk" },
            //    new Person {FirstName = "Vibeke Holtum", LastName="Nørgaard", Email="vhn@tec.dk" },
            //    new Person {FirstName = "Karin Staal", LastName="Køhler", Email="ksk@tec.dk" },
            //    new Person {FirstName = "Charlotte", LastName="Lundius", Email="cl@tec.dk" }
            //};

            //people.ForEach(p => context.People.Add(p));
            //context.SaveChanges();

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
                    FunderId = 1, Person = "Lars Larsen", Budget =184344.00m, ProjectNumber = 3004, ProgramId = "B",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam commodo lobortis nisl molestie malesuada. Quisque eleifend quis nisl at tristique. Maecenas suscipit nibh quis elit sagittis, sit amet fermentum enim tincidunt. Phasellus quis metus diam. Fusce id lacus odio. In sagittis in augue eget viverra. Integer tristique turpis et ex pellentesque commodo. Fusce quis venenatis massa. Ut placerat enim at tellus eleifend, non pharetra diam fringilla. Pellentesque eu orci facilisis, pretium magna vel, congue nunc. Curabitur purus arcu, posuere ac blandit sed, pellentesque ut velit. Morbi sed dictum libero. Integer efficitur dolor tortor. Duis dignissim tellus vehicula purus malesuada, eu laoreet quam varius. Pellentesque rhoncus sapien sit amet tellus placerat faucibus. ",
                    Files = new List<File> { }
                },
                new Project { Name="Nyt tekonologifag",
                    StartDate=DateTime.Parse("08-01-2015"),
                    EndDate=DateTime.Parse("05-01-2016"),
                    FunderId = 5, Person = "Rikke Barsten", Budget = 75000.00m, ProjectNumber = 3005, ProgramId = "B",
                    Files = new List<File> { }
                    },
                new Project { Name="Svage elever i stærke virksomheder",
                    StartDate=DateTime.Parse("06-01-2014"),
                    EndDate=DateTime.Parse("31-05-2015"),
                    FunderId = 1, Person = "Troels Troelsen", Budget = 67825.00m, ProjectNumber = 3006, ProgramId = "B",
                    Files = new List<File> { }
                },
                new Project { Name="Kompetencefonden pulje 2015",
                    StartDate=DateTime.Parse("01-01-2015"),
                    EndDate=DateTime.Parse("09-01-2019"),
                    FunderId = 4, Person = "Rikke Barsten", Budget = 30000.00m, ProjectNumber = 3007, Status = Status.Bevilget, ProgramId = "A",
                    Files = new List<File> { }
                },
                new Project { Name="Clean Tech",
                    StartDate=DateTime.Parse("01-09-2014"),
                    EndDate=DateTime.Parse("31-08-2017"),
                    FunderId = 2, Person = "Nina Olsen", Budget = 284515.00m, ProjectNumber = 3008, Status = Status.Bevilget, ProgramId = "B",
                    Files = new List<File> { }
                }
            };

            projects.ForEach(p => context.AProjects.Add(p));
            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                // add action
            }
            
        }
    }
}