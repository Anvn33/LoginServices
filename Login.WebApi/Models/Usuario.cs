using System;
using System.Collections.Generic;

namespace Login.WebApi.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime FechaCreacion { get; set; }
}
