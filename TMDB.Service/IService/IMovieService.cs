using System;
using System.Collections.Generic;
using System.Text;
using TMDB.Data.DTOs;

namespace TMDB.Service
{
    public interface IMovieService
    {
        MovieDetailDTO GetMovieDetail(int id);
        PopulerMovieListDTO GetMovieList(int PageNumber);
        RateDTO MovieRate(int MovieId, RateDTO Rate);
        List<MovieResultDTO> GetAllMovieList();
        bool SendRecommendMail(int MovieId, RecommandDTO Recipient);
    }
}
