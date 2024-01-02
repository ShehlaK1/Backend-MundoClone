using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using RESTCore.Services;

namespace RESTCore.Controllers
{
    [ApiController]
    [Route("controller")]
    public class TargetSkillController : ControllerBase
    {
        readonly TargetSkillService _targetSkillService;

        public TargetSkillController(TargetSkillService targetSkillService)
        {
            _targetSkillService = targetSkillService;
        }


        //get All target skills
        [HttpGet("/target-skills/getAll")]
        public List<TargetSkillDTO> GetAllTargetSkills()
        {
            return _targetSkillService.GetAllTargetSkills();
        }


        //get one target skill
        [HttpGet("/target-skills/get/{id}")]
        public TargetSkillDTO GetTargetSkill(string id)
        {
            return _targetSkillService.GetTargetSkill(id);
        }


        // create new target skill
        [HttpPost("/target-skills/create")]
        public ActionResult CreateTargetSkill([FromBody] TargetSkillDTO targetSkillDTO)
        {
            if (targetSkillDTO == null)
            {
                return BadRequest();
            }
            _targetSkillService.CreateTargetSkill(targetSkillDTO);
            return Ok("Added");
        }


        [HttpPut("/targetSkills/update/{id}")]
        public ActionResult UpdateTargetSkill(string id, [FromBody] UpdateRequest request)
        {
            if (id == null || request == null || string.IsNullOrWhiteSpace(request.AttributeToUpdate) || string.IsNullOrWhiteSpace(request.NewValue))
            {
                return BadRequest();
            }

            var result = _targetSkillService.UpdateTargetSkill(id, request.AttributeToUpdate, request.NewValue);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok("Target skill updated successfully");
            }
        }



        //delete target skill
        [HttpDelete("/target-skills/delete/{id}")]
        public ActionResult DeleteTargetSkill(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = _targetSkillService.DeleteTargetSkill(id);
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
