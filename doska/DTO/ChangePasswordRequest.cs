﻿namespace doska.DTO;

internal sealed class ChangePasswordRequest
{
    public string Password { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
    public string PasswordConfirmation { get; set; } = default!;
}