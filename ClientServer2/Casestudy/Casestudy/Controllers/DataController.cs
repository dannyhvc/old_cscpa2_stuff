using Casestudy.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace Casestudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        readonly AppDbContext? _ctx;
        public DataController(AppDbContext context) // injected here
        {
            _ctx = context;
        }

        private async Task<String> GetMenuItemJsonFromWebAsync()
        {
            string url = "https://raw.githubusercontent.com/dannyhvc/sample_cars/main/car_products.json";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<String>> Index()
        {
            DataUtility util = new(_ctx!);
            string payload; 
            var json = await GetMenuItemJsonFromWebAsync();
            try
            {
                payload = (await util.LoadStoreInfoFromWebToDb(json)) ? "tables loaded" : "problem loading tables";
            }
            catch (Exception ex)
            {
                payload = ex.Message;
            }
            return JsonSerializer.Serialize(payload);
        }


    }
}
