using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GlobalConstants;

namespace Models
{
    using static DataConstants;

    public class Shift
    {
        public int Id { get; init; }

        public DateTime ShiftDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan FinishTime { get; set; }

        [Column(TypeName = DefaultDecimalValue)]
        [Range(minimum:ShiftMinHours, maximum:ShiftMaxHours)]
        public decimal HoursWorking { get; set; }

        [Column(TypeName = DefaultDecimalValue)]
        public decimal RatePerHour { get; set; }

        public bool IsAssigned { get; set; }

        public int? EmployeeId { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }

        public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();

    }
}