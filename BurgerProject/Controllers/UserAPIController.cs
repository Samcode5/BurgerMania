using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BurgerProject.Repositories;
using BurgerProject.Model;
using BurgerProject.Mappings;
using AutoMapper;
using BurgerProject.DTOs;

namespace BurgerProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {

        private readonly IUserRepo _userRepo;

        private readonly IMapper _mapper;


        public UserAPIController(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            
        }

        [HttpGet]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();
            var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

            return Ok(userDTOs);

        }


        [HttpGet("{number}")]

        public async Task<IActionResult> GetUserById(string number)
        {
            var user = await _userRepo.GetUserByIdAsync(number);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);




        }


        [HttpPost]

        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User Data is required");
            }

           
            var user = _mapper.Map<User>(userDTO);
            await _userRepo.AddUserAsync(user);

            var createdUserDTO = _mapper.Map<UserDTO>(user);

            return CreatedAtAction(nameof(GetUserById), new { number = user.Number }, createdUserDTO);

        }

        [HttpPut("{number}")]

        public async Task<IActionResult> UpdateUser(string number, [FromBody] UserDTO userDTO)
        {
            if (userDTO == null || number == userDTO.Number)
            {
                return BadRequest("User data is Incorrect");
            }

            var existinguser = await _userRepo.GetUserByIdAsync(number);
            if (existinguser == null)
            {
                return NotFound();
            }

            var user = _mapper.Map<User>(userDTO);
            await _userRepo.UpdateUserAsync(user);

            return NoContent();
        }


        [HttpDelete("{number}")]

        public async Task<IActionResult> DeleteUser(string number)
        {
            var existinguser = await _userRepo.GetUserByIdAsync(number);

            if (existinguser == null)
            {
                return NotFound();
            }

            await _userRepo.DeleteUserAsync(number);
            return NoContent();
        }



    }
}
