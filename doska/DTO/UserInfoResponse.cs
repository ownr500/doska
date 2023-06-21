﻿namespace doska.DTO;

internal sealed class UserInfoResponse
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime CreationDate { get; init; }
    public IEnumerable<UserPostDto>? Posts { get; set; }
}