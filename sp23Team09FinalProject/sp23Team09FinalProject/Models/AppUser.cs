using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

//TODO: Make this namespace match your project name
namespace sp23Team09FinalProject.Models
{
    

    public class AppUser : IdentityUser
    {
      
        
        [Display(Name="First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Display(Name = "Middle Initial")]
        public String? MiddleInitial { get; set; }

        [Display(Name = "GPA")]
        public Decimal? GPA { get; set; }

        [Display(Name = "City")]
        public String? City { get; set; }

        [Display(Name = "Street")]
        public String? Street { get; set; }

        [Display(Name = "State")]
        public String? State { get; set; }

        [Display(Name = "Zip Code")]
        public Int32? Zip { get; set; }
       

        [Display(Name = "Position Type")]
        public PositionType? PositionType { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Grad Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public DateTime? GradDate { get; set; }


        [Display(Name = "Major")]
        public Major? Major { get; set; }

        [Display(Name = "Company")]
        public Company? Company { get; set; }

        public AppUser()
        {
            Applications ??= new List<Application>();
        }

        [Display(Name = "Applications")]
        public List<Application>? Applications { get; set; }



        //[InverseProperty("Interviewer")]
        //public List<Interview>? InterviewsWhereIAmTheInterviewer { get; set; }

        //[InverseProperty("Applicant")]
        //public List<Interview>? InterviewsWhereIAmTheApplicant { get; set; }


    }
}
