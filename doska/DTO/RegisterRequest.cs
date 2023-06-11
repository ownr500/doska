﻿namespace doska.DTO;

public class RegisterRequest
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
    public IFormFile? Picture { get; set; } = default!;
}