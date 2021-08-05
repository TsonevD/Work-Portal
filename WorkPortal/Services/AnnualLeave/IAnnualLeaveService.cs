﻿using WorkPortal.Areas.Admin.Models.Employee;
using WorkPortal.Models.AnnualLeave;
using WorkPortal.Models.Shifts;
using WorkPortal.Services.AnnualLeave.Models;

namespace WorkPortal.Services.AnnualLeave
{
    public interface IAnnualLeaveService
    {

        AllAnnualLeavesServiceModel All(string userId);

        int Add(AnnualLeaveInputModel annualLeave, string userById);

        void Edit(int id, AnnualLeaveInputModel annualLeaveEdit, int userId);

        AnnualLeaveDetailsServiceModel EditDetails(int id, int userId);

        void Delete(int id);

        bool IsByUser(int id, int userId);
        int UserId(string id);

    }
}
