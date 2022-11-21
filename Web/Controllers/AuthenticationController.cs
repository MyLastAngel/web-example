using Microsoft.AspNetCore.Mvc;
using Web.Authentication;
using Web.Authentication.Interfaces;
using Web.Models;

namespace Web.Controllers;

[ApiController]
[Route("auth/api")]
public class AuthenticationController : ControllerBase
{
    #region Поля

    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authService;
    private readonly ITokenGeneratorService _tokenGenerator;

    #endregion

    public AuthenticationController(
       ILogger<AuthenticationController> logger,
       IAuthenticationService authService,
       ITokenGeneratorService tokenGenerator
       )
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        var user = await _authService.Authenticate(request.Login, request.Password);

        if (user == null)
        {
            _logger.LogError("Неудачная попытка регистрации {request.Login}", request.Login);

            return Unauthorized(new { message = $"Пользователь '{request.Login}' не найден" });
        }

        return Ok(new
        {
            token = _tokenGenerator.Generate(user)
        });
    }
}
