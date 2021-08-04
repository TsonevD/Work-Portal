using System.ComponentModel.DataAnnotations;

namespace Models.Enums
{
    public enum AnnualLeaveType
    {
        [Display(Name = "Sick Leave")]
        SickLeave,
        [Display(Name = "Unpaid Leave")]
        UnpaidLeave,
        [Display(Name = "Paid Leave")]
        PaidLeave,
    }
}
