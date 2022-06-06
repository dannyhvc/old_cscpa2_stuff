using Casestudy.DAL;
using Casestudy.DAL.DomainClasses;
using Microsoft.EntityFrameworkCore;


namespace Casestudy.DAL.DAO
{
    public class ProductDAO
    {
        private readonly AppDbContext _db;
        public ProductDAO(AppDbContext ctx) => _db = ctx;

        public async Task<List<Product>> GetAllByBrand(int id) 
            => await _db.Products!.Where(item => item.Brand!.Id == id).ToListAsync();

        public async Task<Product> GetById(string id) 
            => await _db.Products!.Where(item => item.Id!.Equals(id)).FirstAsync();
    }
}
