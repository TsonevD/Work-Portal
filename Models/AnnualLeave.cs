using System;

namespace Models
{
    public class AnnualLeave
    {
        public int Id { get; init; }
        public DateTime DateFrom { get; init; }
        public DateTime DateTo { get; init; }
        public decimal TakenDays { get; init; }
        public decimal RemainingDays { get; init; }
    }
}