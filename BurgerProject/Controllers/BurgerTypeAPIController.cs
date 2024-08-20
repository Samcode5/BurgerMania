using AutoMapper;
using BurgerProject.DTOs;
using BurgerProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BurgerProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerTypeAPIController : ControllerBase
    {
        private readonly IBurgerTypeRepo _burgerTypeRepo;
        private readonly IMapper _mapper;


        public BurgerTypeAPIController(IBurgerTypeRepo burgerTypeRepo, IMapper mapper)
        {
            _burgerTypeRepo = burgerTypeRepo; 
            _mapper = mapper;
            
        }


        [HttpGet]

        public async Task<IActionResult> GetAllBurgerTypes()
        {
            var burgers = await _burgerTypeRepo.GetAllBurgerTypeAsync();
            var burgerDTOs = _mapper.Map<IEnumerable<BurgerTypeDTO>>(burgers);

            return Ok(burgerDTOs);

        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetBurgerTypeById(int id)
        {
            var burger = await _burgerTypeRepo.GetBurgerTypeByIdAsync(id);
            if (burger == null)
            {
                return NotFound();
            }

            var burgerDTO = _mapper.Map<BurgerTypeDTO>(burger);
            return Ok(burgerDTO);




        }

    }
}
