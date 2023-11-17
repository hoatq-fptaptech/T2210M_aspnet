using System;
using System.Collections.Generic;

namespace T2210M_API.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Tel { get; set; }

    public int? Age { get; set; }
}
