using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace sp23Team09FinalProject.Models
{
   
   

    public class Company
    {

        //navigational property
        

        [Display(Name = "Company ID")]
        public Int32 CompanyID { get; set; }

        [Display(Name="Company Name")]

        public String CompanyName { get; set; }

        [Display(Name = "Company Description")]
        public String CompanyDescription { get; set; }

        [Display(Name = "Company Email")]
        public String CompanyEmail { get; set; }



        public Company()
        {
            AppUsers ??= new List<AppUser>();
            Positions ??= new List<Position>();
            Industrys ??= new List<Industry>();
        }

        [Display(Name = "AppUsers")]
        public List<AppUser> AppUsers { get; set; }

        public List<Position> Positions { get; set; }
    
        [Display(Name = "Industries")]
        public List<Industry> Industrys { get; set; }

        [Display(Name = "Current Date")]
        public Global CurrDate { get; set; }


    }
}
