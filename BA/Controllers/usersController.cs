using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using BA.DAL;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace BA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AutoValidateAntiforgeryToken]
    public class usersController : ControllerBase
    {
        public IRepository Items { get; set; }

        public usersController(IRepository items)
        {
            Items = items;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var res = await Items.GetUsers().ToListAsync();
                var json = JsonConvert.SerializeObject(
                    res.Select(x => new
                    {
                        id = x.Id,
                        UserName = x.UserName,
                        DepartmentId = x.DepartmentId

                    })
                 );
                return Content(json);
            }
            catch (Exception e)
            {
                return BadRequest();
            }           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetItem(int id)
        {
            try
            {
                var user = await Items.GetUser(id);

                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostItem(User item)
        {
            try
            {
                var department = await Items.GetDepartment(item.DepartmentId);
                if (department == null)
                {
                    return NotFound();
                }
                var res = await Items.SaveUser(item);

                return CreatedAtAction(nameof(GetItem), new { id = res }, item);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, User item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            try
            {
                var department = await Items.GetDepartment(item.DepartmentId);
                if (department == null)
                {
                    return NotFound();
                }
                var res = await Items.SaveUser(item);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var res = await Items.DeleteUser(id);

                if (!res)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
