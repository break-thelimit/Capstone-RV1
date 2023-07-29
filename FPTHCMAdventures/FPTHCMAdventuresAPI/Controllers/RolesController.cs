using AutoMapper;
using DataAccess.Dtos.RankDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.RankService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.RoleService;
using DataAccess.Dtos.RoleDto;

namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IMapper mapper, IRoleService roleService)
        {
            this._mapper = mapper;
            _roleService = roleService;
        }


        [HttpGet(Name = "GetRole")]

        public async Task<ActionResult<ServiceResponse<GetRoleDto>>> GetRankList()
        {
            try
            {
                var res = await _roleService.GetRole();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(Guid id)
        {
            var eventDetail = await _roleService.GetRoleById(id);
            return Ok(eventDetail);
        }

        [HttpPost("role", Name = "CreateNewRole")]

        public async Task<ActionResult<ServiceResponse<RoleDto>>> CreateNewRole(CreateRoleDto answerDto)
        {
            try
            {
                var res = await _roleService.CreateNewRole(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<RoleDto>>> UpdateRole(Guid id, [FromBody] UpdateRoleDto eventDto)
        {
            try
            {
                var res = await _roleService.UpdateRole(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
