﻿using System.Linq;
using System.Threading.Tasks;

namespace Login.WebApi.Models.Response
{
    public class Respuesta
    {
        public int Exito { get; set; }
        public string? Mensaje { get; set; }
        public object? Data { get; set; }

    }
}
