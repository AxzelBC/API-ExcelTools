﻿namespace ExcelToolsApi.Domain.Request;

public class RegisterResquest
{
    public required string FirstName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
