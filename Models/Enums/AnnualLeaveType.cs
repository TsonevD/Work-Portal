using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

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
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue) =>
            enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
    }

}

