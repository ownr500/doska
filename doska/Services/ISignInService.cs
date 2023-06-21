using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

public interface ISignInService
{
    Task<ActionResult<SigninResponse>> SignInAsync(SigninRequest signinRequest);
}