using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services;

namespace TVShowTraker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavouritsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private UserFavouritTVShowService _service;

        public UserFavouritsController(ApplicationDbContext context)
        {
            _context = context;
            _service = new UserFavouritTVShowService(_context);
        }


        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public IActionResult GetUserFavourits(Guid userId)
        {
            try
            {
                var result = _service.GetUserFavourits(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(ex.Message, ExceptionMessages.Fail));
            }
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public IActionResult AddFavourit([FromBody] FavouritRequest model)
        {
            try
            {
                var result = _service.AddFavourit(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(ex.Message, ExceptionMessages.Fail));
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public IActionResult RemoveFavourit([FromBody] FavouritRequest model)
        {
            try
            {
                var result = _service.RemoveFavourit(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(ex.Message, ExceptionMessages.Fail));
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("[action]")]
        public IActionResult RemoveAllFavourits(Guid userId)
        {
            try
            {
                var result = _service.RemoveAllFavourits(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(ex.Message, ExceptionMessages.Fail));
            }
        }
    }
}
