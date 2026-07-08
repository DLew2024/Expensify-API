using System.ComponentModel.DataAnnotations;
using Expensify.API.DTOs.AuthDTOs;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.GlobalExceptionHandling.CustomExceptions;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expensify.API.Controllers;

/// <summary>
/// Handles user authentication and authorization.
/// Responsibilities:
/// - User registration - Done (Need to Test)
/// - User login - Done (Need to Test)
/// - Password reset
/// - Refresh tokens
/// - Email verification
/// - Change password
/// - User logout
/// </summary>
public class AuthController : AuthorizationController
{
    private readonly IService _service;

    public AuthController(IService service)
    {
        _service = service;
    }

    [HttpPost(Name = "Forgot-Password")]
    public async Task<ActionResult<bool>> ForgotPassword(
        ForgotPasswordDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.ForgotPassword(request, cancellationToken);

        return result.Match<ActionResult<bool>>(
            success => Ok(),
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
            success => Ok(),
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
    [HttpPost(Name = "Login")]
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

    [AllowAnonymous]
    [HttpPost(Name = "Register")]
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

    [HttpPost(Name = "Reset-Password")]
    public async Task<ActionResult<bool>> ResetPassword(
        ResetDTO request,
        CancellationToken cancellationToken
    )
    {
        var result = await _service.AuthService.ResetPassword(request, cancellationToken);

        return result.Match<ActionResult<bool>>(
            success => Ok(),
            error =>
                error switch
                {
                    _ => StatusCode(500, error.Message),
                }
        );
    }

    [HttpPost(Name = "Upload-Image")]
    public void UploadImage(CancellationToken cancellationToken) { }
}
