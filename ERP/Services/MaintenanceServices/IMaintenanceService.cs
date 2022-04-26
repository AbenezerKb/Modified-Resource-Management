using ERP.DTOs;
using ERP.Models;

namespace ERP.Services.MaintenanceServices
{
    public interface IMaintenanceService
    {
        Task<Maintenance> ApproveMaintenance(ApproveMaintenanceDTO approveDTO);
        Task<Maintenance> DeclineMaintenance(ApproveMaintenanceDTO declineDTO);
        Task<Maintenance> FixMaintenance(FixMaintenanceDTO fixDTO);
        Task<List<Maintenance>> GetByCondition();
        Task<Maintenance> GetById(int id);
        Task<Maintenance> RequestMaintenance(CreateMaintenanceDTO maintenanceDTO);
    }
}
