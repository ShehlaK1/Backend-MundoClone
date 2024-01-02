using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using RESTCore.Services;

namespace RESTCore.Controllers
{
    [ApiController]
    [Route("controller")]
    public class YearController : ControllerBase
    {
        readonly YearService _yearService;

        public YearController(YearService yearService)
        {
           _yearService = yearService;
        }


        //get All years
        [HttpGet("/years/getAll")]
        public List<YearDTO> GetAllYears()
        {
            return _yearService.GetAllYears();
        }


        //get one year
        [HttpGet("/years/get/{id}")]
        public YearDTO GetYear(string id)
        {
            return _yearService.GetYear(id);
        }


        // create new year
        [HttpPost("/years/create")]
        public ActionResult CreateYears([FromBody] YearDTO yearDTO)
        {
            if (yearDTO == null)
            {
                return BadRequest();
            }
            _yearService.CreateYear(yearDTO);
            return Ok("Added");
        }


        [HttpPut("/years/update/{id}")]
        public ActionResult UpdateYear(string id, [FromBody] UpdateRequest request)
        {
            if (id == null || request == null || string.IsNullOrWhiteSpace(request.AttributeToUpdate) || string.IsNullOrWhiteSpace(request.NewValue))
            {
                return BadRequest();
            }

            var result = _yearService.UpdateYear(id, request.AttributeToUpdate, request.NewValue);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok("Year updated successfully");
            }
        }



        //delete year
        [HttpDelete("/years/delete/{id}")]
        public ActionResult DeleteYear(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = _yearService.DeleteYear(id);
            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }

        }
    }
}
