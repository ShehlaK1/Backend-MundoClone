using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using RESTCore.Services;

namespace RESTCore.Controllers
{
    [ApiController]
    [Route("controller")]
    public class LessonController : ControllerBase
    {
        readonly LessonService _lessonService;

        public LessonController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }


        //get All lessons
        [HttpGet("/lessons/getAll")]
        public List<LessonDTO> GetAllLessons()
        {
            return _lessonService.GetAllLessons();
        }


        //get one lesson
        [HttpGet("/lessons/get/{id}")]
        public LessonDTO GetLesson(string id)
        {
            return _lessonService.GetLesson(id);
        }


        // create new lesson
        [HttpPost("/lessons/create")]
        public ActionResult CreateLesson([FromBody] LessonDTO lessonDTO)
        {
            if (lessonDTO == null)
            {
                return BadRequest();
            }
            _lessonService.CreateLesson(lessonDTO);
            return Ok("Added");
        }


        [HttpPut("/lessons/update/{id}")]
        public ActionResult UpdateLesson(string id, [FromBody] UpdateRequest request)
        {
            if (id == null || request == null || string.IsNullOrWhiteSpace(request.AttributeToUpdate) || string.IsNullOrWhiteSpace(request.NewValue))
            {
                return BadRequest();
            }

            var result = _lessonService.UpdateLesson(id, request.AttributeToUpdate, request.NewValue);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok("Lesson updated successfully");
            }
        }



        //delete lesson
        [HttpDelete("/lessons/delete/{id}")]
        public ActionResult DeleteLesson(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = _lessonService.DeleteLesson(id);
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
