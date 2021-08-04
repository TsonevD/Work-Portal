using System.Collections.Generic;

namespace WorkPortal.Models.AnnualLeave
{
    public class AllAnnualLeaveViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public ICollection<AnnualLeaveModel> AnnualLeave { get; set; }
    }
}
