using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using RESTCore.Services;

namespace RESTCore.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CompositeLevelController : ControllerBase
    {
        readonly CompositeLevelService _compositeLevelService;

        public CompositeLevelController(CompositeLevelService compositeLevelService)
        {
            _compositeLevelService = compositeLevelService; 
        }


        //get All composite levels
        [HttpGet("/compositeLevels/getAll")]
        public List<CompositeLevelDTO> GetAllCompositeLevels()
        {
            return _compositeLevelService.GetAllCompositeLevels();
        }


        //get one composite level
        [HttpGet("/compositeLevels/get/{id}")]
        public CompositeLevelDTO GetCompositeLevel(string id)
        {
            return _compositeLevelService.GetCompositeLevel(id);
        }


        // create new composite level
        [HttpPost("/compositeLevels/create")]
        public ActionResult CreateCompositeLevel([FromBody] CompositeLevelDTO compositeLevelDTO)
        {
            if (compositeLevelDTO == null)
            {
                return BadRequest();
            }
            _compositeLevelService.CreateCompositeLevel(compositeLevelDTO);
            return Ok("Added");
        }


        [HttpPut("/compositeLevels/update/{id}")]
        public ActionResult UpdateCompositeLevel(string id, [FromBody] UpdateRequest request)
        {
            if (id == null || request == null || string.IsNullOrWhiteSpace(request.AttributeToUpdate) || string.IsNullOrWhiteSpace(request.NewValue))
            {
                return BadRequest();
            }

            var result = _compositeLevelService.UpdateCompositeLevel(id, request.AttributeToUpdate, request.NewValue);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok("Composite Level updated successfully");
            }
        }



        //delete composite Level
        [HttpDelete("/compositeLevels/delete/{id}")]
        public ActionResult DeleteCompositeLevel(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = _compositeLevelService.DeleteCompositeLevel(id);
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
