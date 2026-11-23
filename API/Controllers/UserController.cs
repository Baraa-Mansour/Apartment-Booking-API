using API.Common;
using BLL.Interfaces;
using Contracts.DTOs;
using Contracts.DTOs.user;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Tags("Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>Gets a paginated list of users.</summary>
        /// <param name="filterParams">Pagination and user filtering options.</param>
        /// <response code="200">A paginated list of users was returned.</response>
        /// <response code="400">The query parameters are invalid.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResponse<ListItemUser>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<PagedResponse<ListItemUser>>>> GetAll(
            [FromQuery] UserFilterParams filterParams)
        {
            (List<ListItemUser> Items, int Total) items_total =
                await _userService.GetAllWithCountAsync(filterParams);

            var response = new PagedResponse<ListItemUser>
            {
                Items = items_total.Items,
                TotalCount = items_total.Total,
                Page = filterParams.Page,
                PageSize = filterParams.PageSize
            };

            return Ok(ApiResponse<PagedResponse<ListItemUser>>.Ok(response));
        }

        /// <summary>Approves a pending user account.</summary>
        /// <param name="userId">The ID of the user to approve.</param>
        /// <response code="204">The user was approved successfully.</response>
        /// <response code="404">The user was not found.</response>
        /// <response code="409">The user cannot be approved in the current state.</response>
        [HttpPatch("{userId:int}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> ApproveUser([FromRoute] int userId)
        {
            int adminId = 4; // TEST!! : need to be deleted

            await _userService.ApproveUserAsync(userId, adminId);

            return NoContent();
        }

        /// <summary>Rejects a pending user account.</summary>
        /// <param name="userId">The ID of the user to reject.</param>
        /// <response code="204">The user was rejected successfully.</response>
        /// <response code="404">The user was not found.</response>
        /// <response code="409">The user cannot be rejected in the current state.</response>
        [HttpPatch("{userId:int}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> RejectUser([FromRoute] int userId)
        {
            int adminId = 4; // TEST!! : need to be deleted

            await _userService.RejectUserAsync(userId, adminId);

            return NoContent();
        }
    }
}
