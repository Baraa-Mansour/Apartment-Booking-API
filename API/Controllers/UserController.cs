using BLL.Interfaces;
using Contracts.DTOs;
using Contracts.DTOs.user;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<ListItemUser>))]
        public async Task<ActionResult<PagedResponse<ListItemUser>>> GetAll(
            [FromQuery] UserFilterParams filterParams)
        {
            (List<ListItemUser> Items, int Total) items_total =
                await _userService.GetAllWithCountAsync(filterParams);

            return Ok(new PagedResponse<ListItemUser>
            {
                Items = items_total.Items,
                TotalCount = items_total.Total,
                Page = filterParams.Page,
                PageSize = filterParams.PageSize
            });
        }
    }
}
