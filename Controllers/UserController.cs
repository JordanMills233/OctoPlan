using System.Diagnostics;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;
using OctoPlan.Core.Models.Requests;
using OctoPlan.Core.Services;

namespace OctoPlan.Core.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly CognitoAuthService _cognitoAuthService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, CognitoAuthService cognitoAuthService,ILogger<UserController> logger)
    {
        _userService = userService;
        _cognitoAuthService = cognitoAuthService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        return Ok(user);
    }

    [HttpGet()]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken ct)
    {

        try
        {
            var stopwatch = Stopwatch.StartNew();
            var signUpResult = await _cognitoAuthService.SignUpAsync(
                email: request.Email,
                password: request.Password,
                firstName: request.FirstName,
                lastName: request.LastName
            );
            stopwatch.Stop();
            _logger.LogInformation($"User {request.Email} has been created in Cognito in {stopwatch.Elapsed}.");

            stopwatch.Restart();
            var newRequest = new CreateCognitoUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                Sub = signUpResult.UserSub
            };
            stopwatch.Stop();
            _logger.LogInformation($"User {request.Email} has been created in local DB in {stopwatch.Elapsed}.");

            var response = _userService.CreateUserAsync(newRequest, ct);

            return Ok(new {success = true, message = "User registered successfully. please check your email for verification.", email = request.Email });
        }
        catch (Exception e)
        {
            return BadRequest(new {message = e.Message});
        }
        
        // var result = await _userService.CreateUserAsync(request, ct);
        // if (result.Success) return Ok(result);
        //
        // return BadRequest(result);
    }

    [HttpPost()]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        try
        {
            await _cognitoAuthService.ConfirmSignUpAsync(request.email, request.verificationCode);
            return Ok(new { success = true, message = "Email verified successfully" });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = "Failed to verify email", e.Message });
        }
    } 

    [HttpPost]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request, CancellationToken ct)
    {
        try
        {
            AuthenticationResultType authResult =
                await _cognitoAuthService.SignInAsync(request.Email, request.Password);

            var user = await _userService.GetUserByEmailAsync(request.Email);

            var response = new
            {
                User = user,
                Token = authResult.AccessToken,
                RefreshToken = authResult.RefreshToken
            };

            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
        
        
        // var response = await _userService.LoginUserAsync(request, ct);
        
        // if (response.Flag) return Ok(response);

        // return BadRequest(response);
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