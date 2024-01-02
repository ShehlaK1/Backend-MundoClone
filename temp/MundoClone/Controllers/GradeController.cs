using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using RESTCore.Services;

namespace RESTCore.Controllers
{
    [ApiController]
    [Route("controller")]
    public class GradeController : ControllerBase
    {
        readonly GradeService _gradeService;

        public GradeController(GradeService gradeService)
        {
            _gradeService = gradeService;
        }


        //get All grades
        [HttpGet("/grades/getAll")]
        public List<GradeDTO> GetAllGrades()
        {
            return _gradeService.GetAllGrades();
        }


        //get one grade
        [HttpGet("/grades/get/{id}")]
        public GradeDTO GetGrade(string id)
        {
            return _gradeService.GetGrade(id);
        }


        // create new grade
        [HttpPost("/grades/create")]
        public ActionResult CreateGrade([FromBody] GradeDTO gradeDTO)
        {
            if (gradeDTO == null)
            {
                return BadRequest();
            }
            _gradeService.CreateGrade(gradeDTO);
            return Ok("Added");
        }


        [HttpPut("/grades/update/{id}")]
        public ActionResult UpdateGrade(string id, [FromBody] UpdateRequest request)
        {
            if (id == null || request == null || string.IsNullOrWhiteSpace(request.AttributeToUpdate) || string.IsNullOrWhiteSpace(request.NewValue))
            {
                return BadRequest();
            }

            var result = _gradeService.UpdateGrade(id, request.AttributeToUpdate, request.NewValue);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok("Grade updated successfully");
            }
        }



        //delete grade
        [HttpDelete("/grades/delete/{id}")]
        public ActionResult DeleteGrade(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = _gradeService.DeleteGrade(id);
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
