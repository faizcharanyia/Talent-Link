using System.ComponentModel.DataAnnotations;
using sp23Team09FinalProject.Models;
using System;

//TODO: Update this namespace to match your project
namespace sp23Team09FinalProject.Models
{
    public enum Status { Active,
                         [Display(Name = "Accepted for Interview")] AcceptedforInterview,
                         Declined,
                         [Display(Name = "Accepted for Position")] AcceptedforPosition
                       }

    public enum Rooms { A, B, C, D, NoRoom }

    public class WeekdayInterviewTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime interviewDate)
            {
                if (interviewDate.DayOfWeek == DayOfWeek.Saturday || interviewDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    // Interviews are not held on weekends
                    return false;
                }

                var startTime = new TimeSpan(8, 0, 0);
                var endTime = new TimeSpan(17, 0, 0);
                var excludedStartTime = new TimeSpan(11, 1, 0);
                var excludedEndTime = new TimeSpan(12, 59, 0);
                var interviewTime = interviewDate.TimeOfDay;

                if ((interviewTime < startTime || interviewTime > endTime) || (interviewTime >= excludedStartTime && interviewTime <= excludedEndTime))
                {
                    // Interview time is outside of the allowed range
                    return false;
                }

                return true;
            }

            return false;
        }
    }

    public class Application
    {


        //Primary key
        [Display(Name = "Application ID")]
        public Int32 ApplicationID { get; set; }


        //Scalar properties
        [Display(Name ="Student")]
        public AppUser Student { get; set; }


        [Display(Name = "Interview Date:")]
        [WeekdayInterviewTime(ErrorMessage = "Interview date must be a weekday between 8:00 AM-11:00 AM. and 1:00PM-5:00PM")]
        public DateTime? Date { get; set; }

        [Display(Name = "Room:")]
        public Rooms? Room { get; set; }

        [Display(Name = "Application Status")]
        public Status Status { get; set; }


        [Display(Name = "Date Submitted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM d, yyyy}")]
        public DateTime SubmissionDate { get; set; }


        [Display(Name = "Position under consideration")]
        public Position Position { get; set; }

        [Display(Name = "Current Date")]
        public Global CurrDate { get; set; }
    }
}
