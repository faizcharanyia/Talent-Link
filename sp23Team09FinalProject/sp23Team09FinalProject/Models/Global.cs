using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace sp23Team09FinalProject.Models
{


    public class Global
    {
        public Int32 ID { get; set; }

        [Display(Name = "Date and Time")]
        public DateTime CurrDate { get; set; }

        public Global()
        {
            Applications ??= new List<Application>();
            Companys ??= new List<Company>();
            Positions ??= new List<Position>();
        }

        [Display(Name = "Applications")]
        public List<Application>? Applications { get; set; }

        [Display(Name = "Positions")]
        public List<Position>? Positions { get; set; }

        [Display(Name = "Companys")]
        public List<Company>? Companys { get; set; }
    }
}
