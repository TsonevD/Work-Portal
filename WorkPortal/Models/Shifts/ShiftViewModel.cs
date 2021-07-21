using System;

namespace WorkPortal.Models.Shifts
{
    public class ShiftViewModel
    {
        public int Id { get; set; }

        public DateTime ShiftDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan FinishTime { get; set; }
        public decimal HoursWorking { get; set; }

        public decimal RatePerHour { get; set; }

        public LocationViewModel Location { get; set; }

    }
}
