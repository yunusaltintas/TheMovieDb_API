using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDB.Data.DTOs;
using TMDB.Service;

namespace TMDB.Api.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
       
        private readonly IMovieService _movieService;

        public NotFoundFilter(IMovieService movieService )
        {
            
            _movieService = movieService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = (int)context.ActionArguments.Values.FirstOrDefault();
            var result = _movieService.GetMovieDetail(id);

            if (result != null)
            {
                await next();
            }
            else
            {
                ErrorDTO errorDto = new ErrorDTO();
                errorDto.Status = 404;
                errorDto.Errors.Add($"id'si {id} olan ürün veritabanında bulunamadı");

                context.Result = new NotFoundObjectResult(errorDto);
            }
        }
    }
}
