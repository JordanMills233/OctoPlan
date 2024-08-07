using Microsoft.AspNetCore.Mvc;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;

namespace OctoPlan.Core.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        await _userService.CreateUserAsync(user);

        return Ok();
    }


    [HttpPut]
    public async Task<IActionResult> UpdateUser(User user, CancellationToken ct)
    {
        await _userService.UpdateUserAsync(user, ct);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid userId, CancellationToken ct)
    {
        await _userService.DeleteUserAsync(userId, ct);

        return Ok();
    }
}