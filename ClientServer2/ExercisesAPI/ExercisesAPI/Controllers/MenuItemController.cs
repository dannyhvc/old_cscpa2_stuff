using ExercisesAPI.DAL;
using ExercisesAPI.DAL.DAO;
using ExercisesAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ExercisesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuItemController : ControllerBase
    {
        readonly AppDbContext _db;
        public MenuItemController(AppDbContext context)
        {
            _db = context;
        }
        [HttpGet]
        [Route("{catid}")]
        public async Task<ActionResult<List<MenuItem>>> Index(int catid)
        {
            MenuItemDAO dao = new(_db);
            List<MenuItem> itemsForCategory = await dao.GetAllByCategory(catid);
            return itemsForCategory;
        }
    }
}