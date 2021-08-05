using System.Collections.Generic;

namespace WorkPortal.Services.AnnualLeave.Models
{
    public class AllAnnualLeavesServiceModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public ICollection<AnnualLeaveServiceModel> AnnualLeave { get; set; }
    }
}
