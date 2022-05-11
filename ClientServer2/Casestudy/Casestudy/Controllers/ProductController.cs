using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExercisesAPI.DAL.DAO;
using Casestudy.DAL;
using Casestudy.DAL.DomainClasses;


namespace ExercisesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly AppDbContext _db;
        public ProductController(AppDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        [Route("{brand_id}")]
        public async Task<ActionResult<List<Product>>> Index(int brand_id)
        {
            ProductDAO dao = new(_db);
            List<Product> productsForBrands = await dao.GetAllByBrand(brand_id);
            return productsForBrands;
        }
    }
}
