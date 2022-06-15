using Casestudy.DAL;
using Casestudy.DAL.DAO;
using Casestudy.DAL.DomainClasses;
using Casestudy.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Casestudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        readonly AppDbContext _ctx;
        public OrderController(AppDbContext context) // injected here
        {
            _ctx = context;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<string>> Index(OrderHelper helper)
        {
            string retVal;
            try
            {
                CustomerDAO uDao = new(_ctx);
                Customer? cartOwner = await uDao.GetByEmail(helper.Email);
                OrderDAO tDao = new(_ctx);
                int cartId = await tDao.AddOrder(cartOwner!.Id, helper.Items!);
                retVal = cartId > 0
                    ? "Cart " + cartId + " saved!"
                    : "Cart not saved";
            }
            catch (Exception ex)
            {
                retVal = "Cart not saved " + ex.Message;
            }
            return retVal;
        }
    }
}
