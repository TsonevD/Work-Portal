using System;
using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace WorkPortal.Services.AnnualLeaves.Models
{
    public class AnnualLeaveServiceModel
    {
        public int Id { get; set; }

        public AnnualLeaveType Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string Reason { get; set; }

        public int DaysToBeTaken { get; set; }

        public AnnualLeaveStatus Status { get; set; }
    }
}