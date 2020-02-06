using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BA.DAL;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class departmentsController : ControllerBase
    {
        public IRepository Items { get; set; }

        public departmentsController(IRepository items)
        {
            Items = items;
        }
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            //return await Items.GetUsers().ToListAsync();
            var res = await Items.GetDepartments().ToListAsync();
            var json = JsonConvert.SerializeObject(
                res.Select(x => new
                {
                    id = x.Id,
                    Title = x.Title
                })
                    );
            return Content(json);
        }
    }
}
