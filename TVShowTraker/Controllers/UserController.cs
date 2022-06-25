using Microsoft.AspNetCore.Mvc;
using TVShowTraker.Helpers;
using TVShowTraker.Models;
using TVShowTraker.Models.Mappers;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        IBaseService<User, UserVM> userService;
        public UserController(IBaseService<User, UserVM> userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll()
        {
            try
            {
                var users = userService.GetAll();
                if (users == null) return NotFound();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Get(int id)
        {
            try
            {
                var employees = userService.Get(id);
                if (employees == null) return NotFound();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(UserVM user)
        {
            try
            {
                var model = userService.Create(UserMapper.ParseVMToModel(user));
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Update(UserVM user)
        {
            try
            {
                var model = userService.Update(UserMapper.ParseVMToModel(user));
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = userService.Delete(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
