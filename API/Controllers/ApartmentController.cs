using API.Common;
using BLL.Interfaces;
using Contracts.DTOs;
using Contracts.DTOs.apartment;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/apartments")]
    [ApiController]
    [Tags("Apartments")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        /// <summary>Gets a paginated list of apartments.</summary>
        /// <param name="filterParams">Pagination and apartment filtering options.</param>
        /// <response code="200">A paginated list of apartments was returned.</response>
        /// <response code="400">The query parameters are invalid.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResponse<ListItemApartment>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResponse<ListItemApartment>>>> GetAll(
            [FromQuery] ApartmentFilterParams filterParams)
        {
            (List<ListItemApartment> Items, int Total) items_total =
                await _apartmentService.GetAllWithCountAsync(filterParams);

            var response = new PagedResponse<ListItemApartment>
            {
                Items = items_total.Items,
                TotalCount = items_total.Total,
                Page = filterParams.Page,
                PageSize = filterParams.PageSize
            };

            return Ok(ApiResponse<PagedResponse<ListItemApartment>>.Ok(response));
        }

        /// <summary>Gets detailed information for an apartment.</summary>
        /// <param name="apartmentId">The ID of the apartment.</param>
        /// <response code="200">The apartment details were returned.</response>
        /// <response code="404">The apartment was not found.</response>
        [HttpGet("{apartmentId:int}")]
        [ProducesResponseType(typeof(ApiResponse<ApartmentDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<ApartmentDetails>>> GetById([FromRoute] int apartmentId)
        {
            ApartmentDetails details = await _apartmentService.GetDetailsById(apartmentId);

            return Ok(ApiResponse<ApartmentDetails>.Ok(details));
        }
    }
}
