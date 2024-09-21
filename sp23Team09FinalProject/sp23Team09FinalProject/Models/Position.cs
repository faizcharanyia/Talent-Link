using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.ComponentModel.DataAnnotations;


namespace sp23Team09FinalProject.Models
{
	public enum PositionType { [Display(Name = "Internship")]  I,
                               [Display(Name = "Full Time")] FT,
                               [Display(Name = "N/A")] NA
    }


	public class Position
	{

        public Int32 PositionID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public String Title { get; set; }

        [Display(Name = "Description")]
        public String? Description { get; set; }

        [Required]
        [Display(Name = "Position Type")]
        public PositionType PositionType { get; set; }

        [Required]
        [Display(Name = "Location")]
        public String Location { get; set; }

        [Required]
        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        //navigational property
        [Display(Name = "Company")]
        public Company Company { get; set; }

        public Position()
        {
            Majors ??= new List<Major>();
            Applications ??= new List<Application>();
        }

        //navigational property
        [Required]
        [Display(Name = "Applicable Majors")]
        public List<Major> Majors { get; set; }

        //navigational property
        [Display(Name = "AppUsers")]
        public List<Application>? Applications { get; set; }

        [Display(Name = "Interviewer")]
        public AppUser? Interviewer { get; set; }


        [Display(Name = "Date")]
        public static DateTime TimeVal { get; set; }

        [Display(Name = "Current Date")]
        public Global CurrDate { get; set; }


    }

}

