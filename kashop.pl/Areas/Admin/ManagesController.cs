using kashop.bll.Service;
using kashop.dal.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace kashop.pl.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
 
    public class ManagesController : ControllerBase
    {
        private readonly IManageUserService _manageUser;

        public ManagesController(IManageUserService manageUser)
        {
            _manageUser = manageUser;
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _manageUser.GetUsersAsync();
            return Ok(result);
        }

        [HttpPatch("block/{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> BlockUser([FromRoute]string id)
        => Ok(await _manageUser.BlockedUserAsync(id));


        [HttpPatch("unblock/{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string id)
        => Ok(await _manageUser.UnBlockedUserAsync(id));

        [HttpGet("user/{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetUserDetails([FromRoute] string id)
        => Ok(await _manageUser.GetUserDetailsAsync(id));


        [HttpPatch("change-role")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangeRole(ChangeUserRoleRequest request)
     => Ok(await _manageUser.ChangeUserRoleAsync(request));


    }
}
