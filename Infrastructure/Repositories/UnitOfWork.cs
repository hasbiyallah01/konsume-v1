using konsume_v1.Core.Application.Interfaces.Repositories;
using konsume_v1.Infrastructure.Context;
namespace konsume_v1.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KonsumeContext _context;

        public UnitOfWork(KonsumeContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
