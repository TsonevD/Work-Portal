using System;

namespace GlobalConstants
{
    public abstract class DataConstants
    {
        public const string DefaultDecimalValue = "decimal(18,2)";

        public const int DefaultNameMinLength = 3;
        public const int DefaultNameMaxLength = 30;

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 50;

        public class AnnualLeave
        {
            public const int ReasonMinLength = 10;
            public const int ReasonMaxLength = 500;

            public DateTime TodayDate = DateTime.UtcNow;
            public const int DaysMinValue = 1;
            public const int DaysMaxValue = 28;
        }

        public const int AddressNameMinLength = 5;
        public const int AddressNameMaxLength = 50;
        public const int AddressPostCodeMinLength = 3;
        public const int AddressPostCodeMaxLength = 20;







        public const int PhoneMinLength = 10;
        public const int PhoneMaxLength = 36;


        public const double ShiftMinHours = 0;
        public const double ShiftMaxHours = 16;

        public const int JobTitleMinLength = 2;
        public const int JobTitleMaxLength = 25;


        public const int TownNameMinLength = 3;

        public const int TownNameMaxLength = 30;
    }
}
