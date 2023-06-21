using doska.DTO;
using Microsoft.AspNetCore.Mvc;

namespace doska.Services;

internal interface ISignInService
{
    Task<ActionResult<SigninResponse>> SignInAsync(SigninRequest signinRequest);
}