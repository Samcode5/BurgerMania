using AutoMapper;
using BurgerProject.DTOs;
using BurgerProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BurgerProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerAPIController : ControllerBase
    {
        private readonly IBurgerRepo _burgerRepo;
        private readonly IMapper _mapper;


        public BurgerAPIController(IBurgerRepo burgerRepo, IMapper mapper)
        {
            _burgerRepo = burgerRepo;
            _mapper = mapper;
            
        }


        [HttpGet]

        public async Task<IActionResult> GetAllBurgers()
        {
            var burgers = await _burgerRepo.GetAllBurgerAsync();
            var burgerDTOs = _mapper.Map<IEnumerable<BurgerDTO>>(burgers);

            return Ok(burgerDTOs);

        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetBurgerById(int id)
        {
            var burger = await _burgerRepo.GetBurgerByIdAsync(id);
            if (burger == null)
            {
                return NotFound();
            }

            var burgerDTO = _mapper.Map<BurgerDTO>(burger);
            return Ok(burgerDTO);




        }


    }
}
