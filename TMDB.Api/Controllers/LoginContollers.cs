using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDB.Api.Filters;
using TMDB.Data.DTOs;
using TMDB.Data.Entities;
using TMDB.Data.Security;
using TMDB.IService;
using TMDB.Service;

namespace TMDB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginContollers : ControllerBase
    {
        private readonly IBaseService<User> _baseService;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;

        public LoginContollers(IMapper mapper ,IBaseService<User> baseService, ITokenHandler tokenHandler)
        {
            _baseService = baseService;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        [ValidationFilter]
        [Route("/create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDTO userdto)
        {
            var result = new User
            {
                Email = userdto.Email,
                Password = userdto.Password
            };
            //User result = _mapper.Map<UserDTO, User>(userdto);
            await _baseService.AddAsync(result);
            return Ok("oluşturuldu");
        }
        [ValidationFilter]
        [HttpPost]
        [Route("/api/Login")]
        public IActionResult Login([FromBody] UserDTO userdto)
        {
            var result = new User
            {
                Email = userdto.Email,
                Password = userdto.Password
            };

            var GetAccessToken = _tokenHandler.CreateAccessToken(result);

            return Ok(GetAccessToken);
        }

    }
}
