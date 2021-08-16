using System.Collections.Generic;
using System.Threading.Tasks;
using WorkPortal.Models.AnnualLeave;
using WorkPortal.Services.AnnualLeaves.Models;

namespace WorkPortal.Services.AnnualLeaves
{
    public interface IAnnualLeaveService
    {

        ICollection<AllAnnualLeavesServiceModel> All();

        ICollection<AnnualLeaveServiceModel> Mine(int userId);

        int Add(AnnualLeaveInputModel annualLeave, string userById);

        void Edit(int id, AnnualLeaveInputModel annualLeaveEdit, int userId);

        AnnualLeaveDetailsServiceModel EditDetails(int id, int userId);

        Task Approve(int id);

        Task Decline(int id);

        void Delete(int id);

        bool IsByUser(int id, int userId);
    }
}
