using Common.DTOs;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using RESTCore.Services;

namespace RESTCore.Controllers
{
    [ApiController]
    [Route("controller")]
    public class UserController: ControllerBase
    {
        readonly UserService _userService;

        public UserController (UserService userService)
        {
            _userService = userService;
        }


        //get All users
        [HttpGet("/users")]
        public List<UserDTO> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }


        //get one user
        [HttpGet("/users/get-by-Id/{id}")]
        public UserDTO GetUserById(string id)
        {
            return _userService.GetUserById(id);
        }


        // create new user
        [HttpPost("/users/create-user")]
        public ActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            if(userDTO == null)
            {
                return BadRequest();
            }
            _userService.CreateUser(userDTO);
            return Ok("Added");
        }

        [HttpPut("/users/update/{id}")]
        public ActionResult UpdateUser(string id, [FromBody] UpdateRequest request)
        {
            if (id == null || request == null || string.IsNullOrWhiteSpace(request.AttributeToUpdate) || string.IsNullOrWhiteSpace(request.NewValue))
            {
                return BadRequest();
            }

            var result = _userService.UpdateUser(id, request.AttributeToUpdate, request.NewValue);

            if (result==false)
            {
                return NotFound();
            }
            else
            {
                return Ok("User updated successfully");
            }
        }


        //delete user
        [HttpDelete("/users/delete-by-id/{id}")]
        public ActionResult DeleteUserById(string id)
        {
            if(id== null)
            {
                return BadRequest();
            }
            var result = _userService.DeleteUserById(id);
            if(result == false)
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
