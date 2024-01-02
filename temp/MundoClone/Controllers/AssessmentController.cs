using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using RESTCore.Services;

namespace RESTCore.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AssessmentController : ControllerBase
    {
        readonly AssessmentService _assessmentService;

        public AssessmentController(AssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }


        //get All assessments
        [HttpGet("/assessments/getAll")]
        public List<AssessmentDTO> GetAllAssessments()
        {
            return _assessmentService.GetAllAssessments();
        }


        //get one assessment
        [HttpGet("/assessments/get/{id}")]
        public AssessmentDTO GetAssessment(string id)
        {
            return _assessmentService.GetAssessment(id);
        }


        // create new assessment
        [HttpPost("/assessments/create")]
        public ActionResult CreateAssessments([FromBody] AssessmentDTO assessmentDTO)
        {
            if (assessmentDTO == null)
            {
                return BadRequest();
            }
            _assessmentService.CreateAssessment(assessmentDTO);
            return Ok("Added");
        }


        [HttpPut("/assessments/update/{id}")]
        public ActionResult UpdateAssessment(string id, [FromBody] UpdateRequest request)
        {
            if (id == null || request == null || string.IsNullOrWhiteSpace(request.AttributeToUpdate) || string.IsNullOrWhiteSpace(request.NewValue))
            {
                return BadRequest();
            }

            var result = _assessmentService.UpdateAssessment(id, request.AttributeToUpdate, request.NewValue);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok("Assessment updated successfully");
            }
        }



        //delete assessment
        [HttpDelete("/assessments/delete/{id}")]
        public ActionResult DeleteAssessment(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = _assessmentService.DeleteAssessment(id);
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
