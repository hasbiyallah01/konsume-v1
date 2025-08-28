using konsume_v1.Models;

namespace konsume_v1.Core.Application.Services
{
    public interface IVerificationCodeService
    {

        Task<BaseResponse<VerificationCodeDto>> UpdateVerificationCode(int id);
        Task<BaseResponse<VerificationCodeDto>> VerifyCode(int id, int verificationcode);
        Task<BaseResponse<VerificationCodeDto>> SendForgetPasswordVerificationCode(string email);
        Task<BaseResponse<bool>> IsOtpVerified(int userId);
    }
}
