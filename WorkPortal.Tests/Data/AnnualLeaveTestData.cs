
using Models;
using Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace WorkPortal.Tests.Data
{
    public static class AnnualLeaveTestData
    {
        public static IEnumerable<AnnualLeave> AnnualLeave
         => Enumerable.Range(0, 2).Select(i => new AnnualLeave
         {
             Id = i,
             Status = AnnualLeaveStatus.Pending,
         });

    }
}
