using BLL.Interfaces;
using Contracts.DTOs;
using Contracts.DTOs.apartment;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/apartments")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<ListItemApartment>))]
        public async Task<ActionResult<PagedResponse<ListItemApartment>>> GetAll(
            [FromQuery] ApartmentFilterParams filterParams)
        {
            (List<ListItemApartment>Items, int Total) items_total = 
                await _apartmentService.GetAllWithCountAsync(filterParams);

            return Ok(new PagedResponse<ListItemApartment>
            {
                Items = items_total.Items,
                TotalCount = items_total.Total,
                Page = filterParams.Page,
                PageSize = filterParams.PageSize
            });
        }

        [HttpGet("{apartmentId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<ApartmentDetails>))]
        public async Task<ActionResult<ApartmentDetails>> GetById([FromRoute]int apartmentId)
        {
            return Ok(await _apartmentService.GetDetailsById(apartmentId));
        }
    }
}
