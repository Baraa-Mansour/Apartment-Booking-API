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

        [HttpPatch("{userId:int}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ApproveUser([FromRoute]int userId)
        {
            int adminId = 4; // TEST!! : need to be deleted

            await _userService.ApproveUserAsync(userId, adminId);

            return NoContent();
        }

        [HttpPatch("{userId:int}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RejectUser([FromRoute] int userId)
        {
            int adminId = 4; // TEST!! : need to be deleted

            await _userService.RejectUserAsync(userId, adminId);

            return NoContent();
        }
    }
}
