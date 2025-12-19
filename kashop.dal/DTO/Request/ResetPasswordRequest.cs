using kashop.dal.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.dal.DTO.Request
{
    public class ResetPasswordRequest
    {
        public string Code {  get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
    }
}
