using AutoMapper;
using DataAccess.Dtos.ItemInventoryDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.ItemInventoryService;
using Service;
using System.Threading.Tasks;
using System;
using Service.Services.ItemService;
using DataAccess.Dtos.ItemDto;


namespace FPTHCMAdventuresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemsController(IMapper mapper, IItemService itemService)
        {
            this._mapper = mapper;
            _itemService = itemService;
        }


        [HttpGet(Name = "GetItem")]

        public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetItemList()
        {
            try
            {
                var res = await _itemService.GetItem();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(Guid id)
        {
            var eventDetail = await _itemService.GetItemById(id);
            return Ok(eventDetail);
        }

        [HttpPost("item", Name = "CreateNewItem")]

        public async Task<ActionResult<ServiceResponse<ItemDto>>> CreateNewItem(CreateItemDto answerDto)
        {
            try
            {
                var res = await _itemService.CreateNewItem(answerDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("{id}")]

        public async Task<ActionResult<ServiceResponse<ItemDto>>> UpdateItem(Guid id, [FromBody] UpdateItemDto eventDto)
        {
            try
            {
                var res = await _itemService.UpdateItem(id, eventDto);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
      
            }
        }
    }
}
