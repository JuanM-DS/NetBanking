using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetBanking.Core.Dtos;
using NetBanking.Core.EntityFilters;
using NetBanking.Core.Interfaces.Services;

namespace NetBanking.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserServices services, IMapper mapper) : ControllerBase
    {
        private readonly IUserServices _services = services;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public IActionResult GetAll([FromQuery] UserQueryFilters filters)
        {
            var result = _services.GetAll(filters);

            var userDto = _mapper.Map<IEnumerable<UserDto>>(result);

            return Ok(userDto);
        }

        [HttpGet("{idModel}")]
        public async Task<IActionResult> Get(int idModel)
        {
            var result = await  _services.GetByIdAsync(idModel);

            var userDto = _mapper.Map<UserDto>(result);

            return Ok(userDto);
        }
    }
}
