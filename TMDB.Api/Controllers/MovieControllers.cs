using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDB.Api.Filters;
using TMDB.Data.DTOs;
using TMDB.Service;

namespace TMDB.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieControllers : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieControllers(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("/api/Detail/{MovieId}")]
        public IActionResult GetMovieDetail(int MovieId)
        {
            var result = _movieService.GetMovieDetail(MovieId);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/List/{PageNumber}")]
        public IActionResult GetMovieListByPageNumber(int PageNumber)
        {
            var result = _movieService.GetMovieList(PageNumber);
            return Ok(result);
        }

        [HttpPost]
        [ValidationFilter]
        [Route("/api/{MovieId}/Rating")]
        public IActionResult MovieRate(int MovieId, [FromBody] RateDTO Rate)
        {
            var result = _movieService.MovieRate(MovieId, Rate);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/List")]
        public IActionResult GetAllMovieList()
        {
            var result = _movieService.GetAllMovieList();
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/{MovieId}/Recommand")]
        public IActionResult Recommand(int MovieId, [FromBody] RecommandDTO Recipient)
        {
            var isDone = _movieService.SendRecommendMail(MovieId, Recipient);

            return Ok(isDone);
        }




    }
}
