using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Application.DTOs.AuthDtos
{
    public class RegisterResultDto
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; } = Array.Empty<string>();
    }

}
