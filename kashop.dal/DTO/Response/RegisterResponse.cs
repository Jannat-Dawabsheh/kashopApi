using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.DTO.Response
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public List<string>? Errors { get; set; }
    }
}
