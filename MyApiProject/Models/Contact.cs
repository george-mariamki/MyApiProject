using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Logging;
using MyApiProject.Controllers;

namespace MyApiProject.Models
{

    [Index(nameof(Contact.Email), IsUnique = true)]  // Email is Uniq
    public class Contact
    {
        
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Salutation { get; set; }

        [Required]
        [MinLength(3)]
        public string Firstname { get; set; }

        [Required]
        [MinLength(3)]
        public string Lastname { get; set; }
       
        public string Displayname { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthdate { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public DateTime LastChangeTimestamp { get; set; }
        // readonnnly
        [NotMapped] // Exclude from model binding
        public bool NotifyHasBirthdaySoon
        {
            get
            {
                // Calculate NotifyHasBirthdaySoon based on   Birthdate
                if (Birthdate.HasValue)
                {
                    var today = DateTime.Today;
                    var nextBirthday = Birthdate.Value.AddYears(today.Year - Birthdate.Value.Year);
                    if (nextBirthday < today)
                        nextBirthday = nextBirthday.AddYears(1);
                    
                    return (nextBirthday - today).TotalDays <= 14;
                }
                return false;
            }
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phonenumber { get; set; }
    }


}
