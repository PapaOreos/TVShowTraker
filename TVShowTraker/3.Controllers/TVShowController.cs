using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.Filters;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services;

namespace TVShowTraker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowController : ControllerBase
    {
        private readonly TVShowService _service;

        public TVShowController(
            ApplicationDbContext context,
            IMapper mapper,
            IMemoryCache cache
            )
        {
            _service = new TVShowService(context, mapper, cache);
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _service.GetAllVM();
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
        public IActionResult GetAllWithFilter([FromBody] TVShowFilter filter)
        {
            try
            {
                var result = _service.GetAllWithFilter(filter);
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
        public IActionResult Get(int id)
        {
            try
            {
                var result = _service.GetVM(id);
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
        public IActionResult Create([FromBody] TVShowVM model)
        {
            try
            {
                var result = _service.CreateVM(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(ex.Message, ExceptionMessages.Fail));
            }
        }

        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public IActionResult Update([FromBody] TVShowVM model)
        {
            try
            {
                var result = _service.UpdateVM(model);
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

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public IActionResult ExportToCSV(int id)
        {
            try
            {
                _service.ExportTVShowToCSV(id);
                return Ok(new ResponseModel(
                    "CSV created into CSV folder",
                    ExceptionMessages.Success
                    ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel(ex.Message, ExceptionMessages.Fail));
            }
        }
    }
}
