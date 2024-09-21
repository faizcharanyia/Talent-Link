using System;
using System.ComponentModel.DataAnnotations;


namespace sp23Team09FinalProject.Models
{
    public enum Price { Greater, Less}

    public class SearchViewModel
    {
        [Display(Name = "Search by Company:")]
        public String SearchCompany { get; set; }

        [Display(Name = "Search by Industry:")]
        public String SearchIndustry { get; set; }

        [Display(Name = "Search by Position Type:")]
        public PositionType? SearchType { get; set; }

        [Display(Name = "Search by Major:")]
        public String SearchMajor { get; set; }


        [Display(Name = "Search by Location:")]
        public String SearchLocation { get; set; }

        [Display(Name = "Search by Student Name:")]
        public String SearchName { get; set; }

        [Display(Name = "Search by Graduation Date:")]
        public DateTime? SearchDate { get; set; }

        [Display(Name = "Search by GPA:")]
        public Decimal SearchGPA { get; set; }

        [Display(Name = "Search by Major:")]
        public Int32? SearchMajorID { get; set; }

    }
}
