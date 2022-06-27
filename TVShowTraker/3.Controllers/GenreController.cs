using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Services;

namespace TVShowTraker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreService _service;

        public GenreController(
            ApplicationDbContext context,
            IMapper mapper
            )
        {
            _service = new GenreService(context, mapper);
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public IActionResult Create([FromBody] Genre model)
        {
            try
            {
                var result = _service.Create(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(ex.Message, ExceptionMessages.Fail));
            }
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _service.GetAll();
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
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _service.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(ex.Message, ExceptionMessages.Fail));
            }
        }
    }
}
