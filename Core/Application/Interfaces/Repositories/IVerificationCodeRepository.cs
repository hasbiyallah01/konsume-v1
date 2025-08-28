using konsume_v1.Core.Domain.Entities;
using System.Linq.Expressions;

namespace konsume_v1.Core.Application.Interfaces.Repositories
{
    public interface IVerificationCodeRepository 
    {
        Task<int> Save();
        Task<VerificationCode> Create(VerificationCode entity);
        VerificationCode Update(VerificationCode entity);
        Task<VerificationCode> Get(int id);
        Task<VerificationCode> Get(string email);
        Task<VerificationCode> Get(Expression<Func<VerificationCode, bool>> expression);
    }
}
