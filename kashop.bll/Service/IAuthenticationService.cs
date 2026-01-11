using kashop.dal.DTO.Request;
using kashop.dal.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kashop.bll.Service
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse>RegisterAsync(RegisterRequest registerRequest);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<bool> ConfirmEmailAsync(string token,string userId);
        Task<ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
        Task<LoginResponse> RefreshTokenAsync(TokenApiModel request);
    }
}
