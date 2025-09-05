using konsume_v1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using konsume_v1.Core.Application.Interfaces.Repositories;
using konsume_v1.Core.Domain.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.ObjectPool;
using System.Reflection.Metadata.Ecma335;

namespace konsume_v1.Infrastructure.Repositories
{

    public class ProductRepository : IProductRepository
    {
        private readonly KonsumeContext _context;

        public ProductRepository(KonsumeContext context)
        {
            _context = context;
        }


        public async Task<Product> GenerateProductAsync(Product mealPlan)
        {
            if (_context == null) throw new ObjectDisposedException(nameof(KonsumeContext));

            await _context.Set<Product>().AddAsync(mealPlan);
            await _context.SaveChangesAsync();

            return mealPlan;
        }

        public async Task<ICollection<Product>> GetProductByProfileIdAsync(int profileId)
        {
            return await _context.Products
                .Where(mp => mp.ProfileId == profileId)
                .Include(mp => mp.Alternatives)
                .Include(mp => mp.Ingredients)
                .ToListAsync();
        }



        public async Task<Product> GetProductByIdAsync(int mealId)
        {
            return await _context.Products
                .Include(mp => mp.Alternatives)
                .Include(mp => mp.Ingredients)
                .FirstOrDefaultAsync(mp => mp.Id == mealId);
        }
    }
}
