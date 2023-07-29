using DataAccess.Dtos.RankDto;
using DataAccess.Dtos.RoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.RoleService
{
    public interface IRoleService
    {
        Task<ServiceResponse<IEnumerable<GetRoleDto>>> GetRole();
        Task<ServiceResponse<RoleDto>> GetRoleById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewRole(CreateRoleDto createRoleDto);
        Task<ServiceResponse<string>> UpdateRole(Guid id, UpdateRoleDto roleDto);
    }
}
