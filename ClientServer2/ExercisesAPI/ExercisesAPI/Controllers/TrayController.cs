using ExercisesAPI.DAL;
using ExercisesAPI.DAL.DAO;
using ExercisesAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Mvc;
using ExercisesAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
namespace ExercisesAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrayController : ControllerBase
    {
        readonly AppDbContext _ctx;
        public TrayController(AppDbContext context) // injected here
        {
            _ctx = context;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<string>> Index(TrayHelper helper)
        {
            string retVal;
            try
            {
                UserDAO uDao = new(_ctx);
                User? trayOwner = await uDao.GetByEmail(helper.Email);
                TrayDAO tDao = new(_ctx);
                int trayId = await tDao.AddTray(trayOwner!.Id, helper.Selections!);
                retVal = trayId > 0 
                    ? "Tray " + trayId + " saved!"
                    : "Tray not saved";
            }
            catch (Exception ex)
            {
                retVal = "Tray not saved " + ex.Message;
            }
            return retVal;
        }
    }
}