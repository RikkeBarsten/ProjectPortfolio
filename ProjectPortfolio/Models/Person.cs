using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProjectPortfolio.Models
{
    public class Person : IComparable<Person>
    {
        private string email;

        [Key]
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public string Email
        {
            get { return email; }
            set {
            // Using a email-adress regular expression to test the email. Regex is from:
            //http://stackoverflow.com/questions/16167983/best-regular-expression-for-email-validation-in-c-sharp
                Regex validEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                     @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (validEmail.IsMatch(value))
                    email = value;
                else
                    throw new ValidationException("E-mail adressen er ikke i et korrekt format.");
            }
        }
         
        
        // Property for display only
        [Display(Name = "Kontaktperson")]
        public string Name
        {
            get { return FirstName + " " + LastName; }
           
        }


        // Navigation property for entity framwork
        public virtual List<AProject> Projects { get; set; }

        //IComparable
        public int CompareTo(Person other)
        {
            return this.LastName.CompareTo(other.LastName);
        }

        
    }
}