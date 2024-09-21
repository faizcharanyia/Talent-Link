using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace sp23Team09FinalProject.Models
{


    public class Industry
    {
        public Int32 IndustryID { get; set; }

        [Display(Name = "Industry Name")]
        public String IndustryName { get; set; }


        public Industry()
        {
            Companys ??= new List<Company>();
        }

        [Display(Name = "Companys")]
        public List<Company> Companys { get; set; }


    }
}

