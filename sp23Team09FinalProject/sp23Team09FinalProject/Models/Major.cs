using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace sp23Team09FinalProject.Models
{
   

    public class Major
    {
    
        public Int32 MajorID { get; set; }

        [Display(Name = "Major Name")]
        public String MajorName { get; set; }


        public Major()
        {
            Positions ??= new List<Position>();
            AppUsers ??= new List<AppUser>();
        }

        [Display(Name = "Applicable Majors")]
        public List<Position> Positions { get; set; }

        [Display(Name = "AppUsers")]
        public List<AppUser> AppUsers { get; set; }

    }
}

