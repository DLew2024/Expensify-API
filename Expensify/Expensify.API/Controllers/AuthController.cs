using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.Filters;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers;

/// <summary>
/// Handles user authentication and authorization.
/// Responsibilities:
/// - Change password - Done (Need to Test)
/// - Email verification - Done (Need to Test)
/// - Forgot Password - Done (Need to Test)
/// - Get User Info - Done (Need to Test)
/// - Logout User - Done (Need to Test)
/// - Password reset - Done (Need to Test)
/// - Refresh tokens -
/// - Upload Image -
/// - User login - Done (Need to Test)
/// - User registration - Done (Need to Test)
/// </summary>
[Route("api/auth")]
public class AuthController : AuthorizationControllerBase
{
    private readonly IService _service;

    public AuthController(IService service)
    {
        _service = service;
    }

    [HttpPost("change-password", Name = "ChangePassword")]
    public async Task<ActionResult> ChangePassword(
        ChangePasswordDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.ChangePassword(
            CurrentUserId,
            request,
            cancellationToken
        );

        return result.Match<ActionResult>(
            success => NoContent(),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    UnauthorizedAccessException ex => Unauthorized(ex.Message),
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    [AllowAnonymous]
    [HttpPost("email-verification", Name = "EmailVerification")]
    public async Task<ActionResult> EmailVerification(
        EmailVerificationDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.EmailVerification(request, cancellationToken);

        return result.Match<ActionResult>(
            success => NoContent(),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    [AllowAnonymous]
    [HttpPost("forgot-password", Name = "ForgotPassword")]
    public async Task<ActionResult<bool>> ForgotPassword(
        ForgotPasswordDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.ForgotPassword(request, cancellationToken);

        return result.Match<ActionResult<bool>>(
            success => NoContent(),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    EntityNotFoundException ex => BadRequest(ex.Message),
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    [HttpGet(Name = "GetUserInfo")]
    public async Task<ActionResult<UserResponseDTO>> GetUserInfo(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.GetUserInfo(id, cancellationToken);

        return result.Match<ActionResult<UserResponseDTO>>(
            success => Ok(success),
            error =>
                error switch
                {
                    EntityNotFoundException ex => BadRequest(ex.Message),
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    // Add unauthroized logs
    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    [ServiceFilter(typeof(ValidationFilter<LoginUserDTO>))]
    public async Task<ActionResult<UserTokenResponseDTO>> LoginUser(
        LoginUserDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.LoginUser(request, cancellationToken);

        return result.Match<ActionResult<UserTokenResponseDTO>>(
            succees => Ok(succees),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    EntityNotFoundException ex => BadRequest(ex.Message),
                    UnauthorizedAccessException ex => Unauthorized(ex.Message),
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    [HttpPost("logout", Name = "LogoutUser")]
    public async Task<ActionResult> LogoutUser(
        LogoutUserDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.LogoutUser(
            CurrentUserId,
            request,
            cancellationToken
        );

        return result.Match<ActionResult>(
            success => NoContent(),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    UnauthorizedAccessException ex => Unauthorized(ex.Message),
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    [HttpPost("refresh-token", Name = "RefreshToken")]
    public async Task<ActionResult<RefreshTokenResponseDTO>> RefreshTokens(
        RefreshTokensDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.RefreshTokens(request, cancellationToken);

        return result.Match<ActionResult<RefreshTokenResponseDTO>>(
            success => Ok(success),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    UnauthorizedAccessException ex => Unauthorized(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, error.Message),
                }
        );
    }

    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    public async Task<ActionResult<UserTokenResponseDTO>> RegisterUser(
        RegisterUserDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.RegisterUser(request, cancellationToken);
        return result.Match<ActionResult>(
            succees => StatusCode(StatusCodes.Status201Created),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    ConflictException ex => Conflict(ex.Message),
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    [AllowAnonymous]
    [HttpPost("reset-password", Name = "ResetPassword")]
    public async Task<ActionResult<bool>> ResetPassword(
        ResetPasswordDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.ResetPassword(request, cancellationToken);

        return result.Match<ActionResult<bool>>(
            success => NoContent(),
            error =>
                error switch
                {
                    ValidationException ex => BadRequest(ex.Message),
                    EntityNotFoundException ex => NotFound(ex.Message),
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    [HttpPost("upload-image", Name = "UploadImage")]
    public void UploadImage(CancellationToken cancellationToken) { }
}
