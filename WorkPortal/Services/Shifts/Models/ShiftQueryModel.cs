using System;

namespace WorkPortal.Services.Shifts.Models
{
    public class ShiftQueryModel
    {
        public int Id { get; set; }

        public DateTime ShiftDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan FinishTime { get; set; }
        public decimal HoursWorking { get; set; }

        public decimal RatePerHour { get; set; }

        public LocationQueryModel Location { get; set; }
    }
}
